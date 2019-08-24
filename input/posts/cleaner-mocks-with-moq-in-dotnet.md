Title: Cleaner mocks with Moq in .NET
Published: 5/25/2019
Tags: 
- dotnet
- Moq
- csharp
---
# Intro

Making use of the (Fluent) builder pattern we can effectively make our unit tests more cleaner and easier to understand. It is often the case when writing unit tests with [Moq](https://github.com/moq) you end up setting up alot of mocks. Like the unit test below we are having to call `Setup` to return a fake list we setup earlier. The below example is a simple example with just one Setup call, but more often than not most unit tests have multiple setups. To make it more easier on ourselves we can refactor this code.

Before:

```csharp
[TestMethod]
public void Should_Return_Students_When_User_Navigated_To_Home_View()
{
    //Arrange
    var fakeStudentsList = new List<StudentModel>
    {
        new StudentModel
        {
            Id = 321,
            FullName = "Joe Bloggs",
            PhoneNumber = "03245 435345",
            DateOfBirth = DateTime.Parse("1993-01-01")
        },
        new StudentModel
        {
            Id = 123,
            FullName = "John Smith",
            PhoneNumber = "01345 3456778",
            DateOfBirth = DateTime.Parse("1990-01-01")
        }
    };
    var mockLogger = new Mock<ILogger>();
    var mockStudentService = new Mock<IStudentsService>();
    mockStudentService.Setup(m => m.GetStudents())
        .Returns(fakeStudentsList);

    var controller = new HomeController(mockStudentService.Object, mockLogger.Object);

    //Act
    var result = controller.Index() as ViewResult;
    var model = result.Model as HomeViewModel;

    //Assert
    Assert.IsNotNull(result);
    Assert.AreEqual(fakeStudentsList, model.Students);
    mockStudentService.Verify(c => c.GetStudents(), Times.Once);
}
```

After:

```csharp
 [TestMethod]
public void Should_Return_Students_When_User_Navigated_To_Home_View()
{
    //Arrange
    var fakeStudentsList = FakeData.FakeStudentModelList();
    var mockLogger = new MockLogger();
    var mockStudentService = new MockStudentService()
        .MockGetStudents(output: fakeStudentsList);

    var controller = new HomeController(mockStudentService.Object, mockLogger.Object);

    //Act
    var result = controller.Index() as ViewResult;
    var model = result.Model as HomeViewModel;

    //Assert
    Assert.IsNotNull(result);
    Assert.AreEqual(fakeStudentsList, model.Students);
    mockStudentService.Verify(c => c.GetStudents(), Times.Once);
}
```

# Explanation

One if the first things we could easily refactor is moving the list into a static method elsewhere. Then the bit we can do next is create a mock class that inherits from `Mock<IStudentsService>` instead of calling `new Mock<IStudentsService>()` it now simply becomes `new MockStudentService()`.

To then make it more easier to wire up mocks, we can create methods that mock each call within the `MockStudentService` class. Like the example below. You pass the output you want returned and then return the instantce of MockStudentService. So now you can simple chain mocks, which is in my opinion is more easy to read.


```csharp
public class MockStudentService : Mock<IStudentsService>
{
    public MockStudentService MockGetStudents(IList<StudentModel> output)
    {
        Setup(m => m.GetStudents())
            .Returns(output);
        return this;
    }
}
```

This code is has only one mock method, but you can easily see the benefit if a call was like this.

```csharp
var mockStudentService = new MockStudentService()
        .MockGetStudents(output: fakeStudentsList)
        .MockSomething(output: fakeModel)
        .MockSomethingElse(output: fakeObject);
```

You can simply chain each mock and easily see what it mocked without scanning through a block of code.


