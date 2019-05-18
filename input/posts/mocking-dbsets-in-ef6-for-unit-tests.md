Title: Mocking DbSet in EF6 for unit tests
Published: 12/5/2019
Tags: 
- .NET
- Moq
- Entity Framework
- C#
---
# Intro

Mocking DbSets with [Moq](https://www.nuget.org/packages/moq/) makes it easy to test Entity Framework without having to connect to a real database. This is usful when you want to test the logic of your code without having to use a database.  

# Code

The end result of your test could look like below. In the example below there is a fake list of students which you can see the code to further on. The first student is taken from that list and used to test the service call as it is expected a student with that id will exsit when calling Students from StudentContext. 

```csharp
[TestMethod]
public void Should_Get_Student_When_Get_With_Valid_Id()
{
    //Arrange
    var fakeStudentsList = FakeData.FakeStudents();
    var firstStudent = fakeStudentsList.FirstOrDefault();
    var mockStudentsList = fakeStudentsList.ToMockDbSet();

    StudentContext.Setup(c => c.Students).Returns(mockStudentsList.Object);
    var service = new StudentsService(ContextFactory.Object, Logger.Object);

    //Act
    var student = service.Get(firstStudent.StudentId);

    //Assert
    Assert.IsNotNull(student);
    Assert.AreEqual(firstStudent.StudentId, student.Id);
    Assert.AreEqual(firstStudent.PhoneNumber, student.PhoneNumber);
    Assert.AreEqual(firstStudent.DateOfBirth, student.DateOfBirth);
}
```

The extension function `.ToMockDbSet()` is where it turns any list into a mock DbSet, which is then used to set Students within the Student Context. This means that when any call made to Students will be using the fake list created earliar.

# More Code

To Mock Db Set

```cshrap
public static class MockHelper
{
    public static Mock<DbSet<T>> ToMockDbSet <T>(this IQueryable<T> data) where T : class
    {
        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        return dbSet;
    }
}
```

Fake Data

```csharp
public static class FakeData
{
    public static IQueryable<Student> FakeStudents()
    {
        return new List<Student>
        {
            new Student
            {
                StudentId = 123,
                FullName = "John Smith",
                PhoneNumber = "01345 3456778",
                DateOfBirth = DateTime.Parse("1990-01-01")
            },
            new Student
            {
                StudentId = 321,
                FullName = "Joe Bloggs",
                PhoneNumber = "03245 435345",
                DateOfBirth = DateTime.Parse("1993-01-01")
            }
        }.AsQueryable();
    }
}
```

Service Get Method

```csharp
public StudentModel Get(int id)
{
    try
    {
        using (var context = _contextFactory.StudentsContext)
        {
            var student = context.Students.FirstOrDefault(x => x.StudentId == id);
            if (student != null)
            {
                return new StudentModel
                {
                    Id = student.StudentId,
                    FullName = student.FullName,
                    PhoneNumber = student.PhoneNumber,
                    DateOfBirth = student.DateOfBirth,
                };
            }
            return default;
        }
    }
    catch (Exception ex)
    {
        _logger.LogException(ex);
        throw;
    }
}
```