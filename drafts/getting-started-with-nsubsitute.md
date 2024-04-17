Title: Intro to nsubstitute
Published: 12/12/2022
Tags: 
- dotnet
- nsubstitute
- dotnet 6
- csharp

---

NSubstitute is a mocking framework for .NET that makes it easy to create mock objects for use in unit tests. Mocking allows you to replace the real dependencies of your code with controlled versions that you can use to test your code in isolation.

To get started with NSubstitute, the first thing you need to do is install the NuGet package. Open the Package Manager Console and run the following command:

```
Install-Package NSubstitute
```

Once the package is installed, you can start using NSubstitute in your unit tests. To create a mock object, use the Substitute.For method and pass in the type of the object you want to mock. For example, if you wanted to create a mock IFoo object, you could do so like this:

```csharp
var foo = Substitute.For<IFoo>();
```

With the mock object in hand, you can set up return values for methods that will be called during the test. For example, if the IFoo.Bar method is called during the test, you can specify what value it should return like this:

```csharp
foo.Bar().Returns("hello world");
```

Additionally, NSubstitute allows you to verify that certain methods were called on the mock object during the test. For example, if you wanted to verify that the IFoo.Bar method was called at least once during the test, you could do so like this:

```csharp
foo.Received().Bar();
```

In conclusion, NSubstitute is a useful tool for writing unit tests for your .NET code. It makes it easy to create mock objects and set up return values and verify method calls. Give it a try and see how it can help improve your testing workflow.