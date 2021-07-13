Title: Sociable unit tests with BDDfy
Published: 7/7/2021
Tags: 
- dotnet
- csharp
- xamarin
- unit tests
- sociable
- bdd
- tdd

---
# Overview

In this blog post, I will be covering two things, one is sociable unit tests and the second is using a BDD (behavior-driven development) framework called BDDfy to pardon the pun, BDDfy your unit tests. The idea is that combined together you should have fewer unit tests that are easier to follow, maintain and give you the same level of test coverage compared with lots of low level unit tests.

If you want to jump straight into real examples like I do sometimes then you can have a look at this [example Xamarin Github repository](https://github.com/reggieray/LottoNumbers). It's an app that was built with Xamarin Forms with unit tests following the concepts I will mention here.

# Sociable Tests

Sociable unit testing is a term coined by Jay Fields in his book *Working Effectively with Unit Tests*. I got introduced to the concept from this [UniTest article by Martin Fowler](https://martinfowler.com/bliki/UnitTest.html). In it, he goes on to cover what is a definition of a unit test, because depending on who you speak this can vary. The blog then goes on to discuss what sociable and solitary unit tests are.

When learning to write unit tests you would probably quickly come across test doubles (mocking) and in many docs, for testing frameworks, they will try to give simple examples which are usually solitary unit tests. It's because of this without realizing it, my tendency was to write unit tests in this fashion. I did not know the difference between solitary and sociable unit tests. I also used to have this misconception that if a unit test was covering more than one class then it was more of an integration test. One of the main things I took away from writing sociable unit tests is my definition of a unit test. The following quote from the Martin's blog is something I agree with and now think about when writing unit tests.

> Although I start with the notion of the unit being a class, I often take a bunch of closely related classes and treat them as a single unit. Rarely I might take a subset of methods in a class as a unit. However, you define it doesn't really matter

Some other key points that are worth noting are that solitary and sociable unit tests both have their place, it really depends on your circumstances of what unit test would be best to write. One thing that I keep in mind when writing solitary unit tests is that will this grow over time to become unwieldy and difficult to manage. For example, you want to make a change that passes a parameter that gets passed down multiple classes. If there were many solitary unit tests in place, then this would mean many unit tests would fail and require many updates to get your unit tests working. On the other hand, if it had been a sociable unit test then hopefully only one or a few unit tests would fail. You still get the same coverage, but with less maintenance and this is where the advantages of sociable unit tests come into play.

# BDDfy

> BDDfy is the simplest BDD framework to use, customize and extend!

That's the introduction from the [BDDfy docs website](http://bddfy.teststack.net/docs/foo). There are other BDD frameworks out there, namely [Specflow](https://specflow.org/) comes to mind. This isn't going to be a comparison between the two, but if you are familiar with Specflow then you'll probably find BDDfy simpler to setup and use. Test's are written in a Given/When/Then style.

To use BDDfy install TestStack.BDDfy nuget package:

```
Install-Package TestStack.BDDfy
```
I'll mainly be showing examples using the BDDfy's Fluent API. The following example is taken from the example Xamarin app mentioned earlier. [Link to code](https://github.com/reggieray/LottoNumbers/blob/acf9f9499cc01312a00c1b4014d63e7349258aa4/LottoNumbers.UnitTests/ViewModels/MainPageViewModelTests.cs#L94).

``` csharp
[Fact]
public void UserNavigatesToSettings()
{
    this.Given(_ => _.AUserIsOnTheMainPage())
    .When(_ => _.TheUserClicksToNavigateToSettingsPage())
    .Then(_ => _.TheUserIsNavigatedToSettingsPage())
    .BDDfy();
}
```

This is what a BBDfy unit test will look like using BDDfy's Fluent API. You don't have to use XUnit though as other testing frameworks are supported such as NUnit and MSTest. This means you can also use those specific testing frameworks features like NUnit's `[TestCase]` attribute. An alternative built into BDDfy though is example tables. They can look like the following code snippet. [Link to code](https://github.com/reggieray/LottoNumbers/blob/acf9f9499cc01312a00c1b4014d63e7349258aa4/LottoNumbers.UnitTests/ViewModels/MainPageViewModelTests.cs#L54).

``` csharp
[Fact]
public void UserGeneratesLottoNumbers()
{
    var gameKey = default(string);
    var useSeed = default(bool);

    this.Given(_ => _.AUserIsOnTheMainPage())
    .And(_ => _.TheUserIsShownGamesToPick())
    .And(_ => _.TheUserSelectsALottoGame(gameKey))
    .And(_ => _.TheUserHasASeedForRandom(useSeed))
    .When(_ => _.TheUserClicksGenerateNumbers())
    .Then(_ => _.TheLottoNumbersAreShown())
    .And(_ => _.TheGameHeaderIs(gameKey))
    .And(_ => _.TheNumbersAreCorrectForGame(gameKey))
    .WithExamples(new ExampleTable("gameKey", "useSeed")
    {
        { "LOTTO", false },
        { "EURO", false },
        { "REGIS", false },
        { "REGIS", true },
        { "EURO", true }
    })
    .BDDfy();
}
```

Another thing worth mentioning is it can produce reports. If you run tests locally with Visual Studio then you'll find it in the bin folder. These are useful and can be built as part of continues integration. Here's a report I produced from the example Xamarin app linked earlier [BDDfy HTML Report](/posts/html/BDDfy.html)

<img src="/posts/images/bddfy-html-report.PNG" height="600">
<br/>

# Final thoughts

It's worth mentioning that sociable unit tests doesn't mean using actual implementations for everything, like in the Xamarin app's unit tests you'll notice it uses actual implementations where is can and mocks when it's not so easy to do so.

In my experience I've found sociable unit tests written with BDDfy to greatly improve the speed with which you can write tests while giving you the coverage of many low level unit tests. Leveraging the tools we are already working with such as IntlliSense contributes to this as-well as is having the flexibility to choose your preferred testing framework. 

# Links

- [Example Xamarin Github repository](https://github.com/reggieray/LottoNumbers)
- [UnitTest article by Martin Fowler](https://martinfowler.com/bliki/UnitTest.html)
- [BDDfy](https://github.com/TestStack/TestStack.BDDfy)
- [BDDfy docs website](http://bddfy.teststack.net/docs/foo)
- [Specflow](https://specflow.org/)
- [Example BDDfy HTML Report](/posts/html/BDDfy.html)