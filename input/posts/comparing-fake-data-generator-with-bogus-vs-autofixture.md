Title: A comparison of fake data generators Bogus vs AutoFixture
Published: 10/08/2023
Tags: 
- dotnet
- csharp
- bogus
- auto fixture

---

# Introduction

When it comes to testing in C#, creating fake or mock data is a common requirement. Two popular libraries for achieving this are Bogus and AutoFixture. In this blog post, we'll explore both libraries, compare their features, and provide code examples to showcase their capabilities. 

Before I do that here is a quick view on the statistics of both libraries.

| Comparison 8/10/2023      | Bogus                                         | AutoFixture                                          |
| ------------------------- | --------------------------------------------- | ---------------------------------------------------- |
| Source Link               | [Github](https://github.com/bchavez/Bogus)    | [Github](https://github.com/AutoFixture/AutoFixture) |
| NuGet Link                | [Nuget](https://www.nuget.org/packages/Bogus) | [Nuget](https://www.nuget.org/packages/AutoFixture)  |
| NuGet Downloads (Total)   | 55.8M                                         | 116.6M                                               |
| NuGet Downloads (Per Day) | 18.3K                                         | 25.1K                                                |
| Github Stars              | 7.7k                                          | 3.1k                                                 |
| Github Watchers           | 125                                           | 94                                                   |
| Github Forks              | 450                                           | 339                                                  |

It must be noted that numbers alone don't give you the full picture of a library and if one is better than the other. In most case you'll find that each library handles certain use cases better than others. So if you're looking to use one of these nuget libraries then it would depend on your use case given you understand what each library can offer. 

That said with the numbers alone it's safe to say these are two very popular libraries. I find the type of statistics above useful when I want to use a library, but I am not sure if it's ok to use based off the following:
- Security - Could this lib present a potential security concern. If it is widely adopted then this indicates that the community think it's secure enough. Even better if you know a well known company has adopted it. 
- Usage - Has it been battle tested. If it's not been widely used then there could be potential bugs lurking in the code. If widely used then this would indicate that someone would of possibly come across your use case, found the issue and had it fixed. 

# Examples

In both examples I used the following classes to generate fake data for. You can find the code from all the examples on this [github repo](https://github.com/reggieray/fake-data-examples). 

```csharp
public record Address(string Line1, string Line2, string Town, string PostCode, string Country);

public record Person(string FirstName, string LastName, string Email, Address Address);
```

## Bogus

[Bogus](https://github.com/bchavez/Bogus) allows you to create realistic-looking test data with minimal effort by using predefined fake data generators. 

Highlights:

- Record support - Record support doesn't come out the box, I had to create an extension to get it to play nice. 
- Setup - Requires more setup as you need to specify what type of data you'd like it to generate.
- Realistic data - Generates more realistic data.

```csharp
using Bogus;
using System.Runtime.Serialization;
using System.Text.Json;
using Xunit.Abstractions;

namespace FakeData.Examples.Tests
{
    public class BogusTests
    {
        private readonly ITestOutputHelper outputHelper;

        public BogusTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Fact]
        public void Person()
        {
            // Create a Faker instance for generating fake data
            var faker = new FakeDataGenerator();

            // Generate a fake person
            var fakePerson = faker.Person.Generate();

            outputHelper.WriteLine(JsonSerializer.Serialize(fakePerson));
        }
    }

    public class FakeDataGenerator
    {
        public Faker<Person> Person;
        public Faker<Address> Address;

        public FakeDataGenerator() 
        {
            Address = new Faker<Address>()
                .WithRecord()
                .StrictMode(true)
                .RuleFor(o => o.Line1, f => f.Address.StreetAddress())
                .RuleFor(o => o.Line2, f => f.Address.SecondaryAddress())
                .RuleFor(o => o.Town, f => f.Address.City())
                .RuleFor(o => o.PostCode, f => f.Address.ZipCode())
                .RuleFor(o => o.Country, f => f.Address.Country());

            Person = new Faker<Person>()
                .WithRecord()
                .StrictMode(true)
                .RuleFor(o => o.FirstName, f => f.Person.FirstName)
                .RuleFor(o => o.LastName, f => f.Person.LastName)
                .RuleFor(o => o.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(o => o.Address, Address.Generate());
        }
    }

    public static class ExtensionsForBogus
    {
        public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class
        {
            faker.CustomInstantiator(_ => FormatterServices.GetUninitializedObject(typeof(T)) as T);
            return faker;
        }
    }
}
```
Example output:
```json
{
  "FirstName": "Carole",
  "LastName": "Fay",
  "Email": "Carole.Fay@hotmail.com",
  "Address": {
    "Line1": "678 Geovany Forest",
    "Line2": "Suite 413",
    "Town": "Jeremychester",
    "PostCode": "20819",
    "Country": "Venezuela"
  }
}
```

## AutoFixture

[AutoFixture](https://github.com/AutoFixture/AutoFixture) takes a different approach by focusing on automating the process of creating test data without much manual configuration. 

Highlights:

- Customization support - By default it will populate properties with random data, usually if the property is a string it will use property name as a prefix and guid to fill in the rest, but if you want to define what data it should generate, then you can set this up yourself, like in the example for email.  
- Minimal setup - Easily generate classes with fake data without much setup.


```csharp
using AutoFixture;
using System.Text.Json;
using Xunit.Abstractions;

namespace FakeData.Examples.Tests
{
    public class AutoFixtureTests
    {
        private readonly ITestOutputHelper outputHelper;

        public AutoFixtureTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }


        [Fact]
        public void Person()
        {
            // Create a Fixture instance for generating auto-mocked data
            var fixture = new Fixture();

            // Generate a person with auto-mocked data
            var person = fixture.Build<Person>()
                .With(x => x.Email, "test@email.com")
                .Create();

            outputHelper.WriteLine(JsonSerializer.Serialize(person));
        }
    }
}
```
Example output:
```json
{
  "FirstName": "FirstNamef1b2b680-0a79-4015-96bf-8b36a39ec79d",
  "LastName": "LastName2af92601-cc8e-4bf2-b458-21abf995fb0a",
  "Email": "test@email.com",
  "Address": {
    "Line1": "Line16a0323fa-4db8-4e70-adc3-8091e5408618",
    "Line2": "Line2527e5086-140c-4201-b361-2ac2952acf4d",
    "Town": "Towndfd93b2f-1728-47e8-86a0-d0bb55a1b1ae",
    "PostCode": "PostCodeadf7d835-9249-42f0-b82a-0b80002084f5",
    "Country": "Countrye942137a-8641-4f1c-a362-b2761903fb3e"
  }
}
```

# Summary

Choosing between Bogus and AutoFixture depends on your specific needs and preferences. Bogus excels in providing a rich set of data generators with explicit customization, while AutoFixture offers a more automated, convention-based approach for quickly generating test data.

Ultimately, the choice between these two libraries comes down to the level of control and customization you desire in your test data generation process. Each has its strengths, and the best fit depends on the nature of your testing scenarios.