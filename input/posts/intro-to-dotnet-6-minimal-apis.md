Title: Intro to .NET6 Minimal APIs
Published: 4/4/2022
Tags: 
- dotnet
- minimal api
- dotnet 6
- csharp

---
# Overview

Minimal APIs was introduced with the [release of .NET6](https://devblogs.microsoft.com/dotnet/announcing-net-6/) as a alternative approach for building API's compared to the MVC/Controller approach that .NET developers are probably used too. Minimal APIs allow you to create HTTP APIs with minimal dependencies and minimal number of files.

A minimal api hello world would look like the following:

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello, World!");

app.Run();
```

A seasoned dotnet engineer would probably raise a bunch of questions as to how this works and how a `Program.cs` file came to have so few lines of code which I will explore later on. 

Alternatively for a beginner this reduces the barrier to entry, you might notice some similarities in simplicity compared with other frameworks such as hello world examples from Node.js/Express.js or Python/Flask. 


```javascript
// Node.js & Express.js
const express = require('express')
const app = express()
const port = 3000

app.get('/', (req, res) => {
  res.send('Hello World!')
})

app.listen(port, () => {
  console.log(`Example app listening on port ${port}`)
})
```

```python
# Python & Flask 
from flask import Flask

app = Flask(__name__)

@app.route("/")
def hello():
    return "Hello World!"

if __name__ == '__main__':
    app.run(debug=True)
```

Somehow dotnet managed to reduce it's hello world to four lines, reducing the ceremony normally associated with the MVC/Controller approach. This is one of the benefits of building with minimal apis which also as a side effect has improved performance, but don't be under the impression minimal equals simple. Minimal API's should be able cover the majority of scenarios you might want out of an API.

There are some [missing features](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio#differences-between-minimal-apis-and-apis-with-controllers), but there are plans to introduce them later on. At time of this writing one important features looks to be [introduced with .NET7](https://devblogs.microsoft.com/dotnet/announcing-dotnet-7-preview-3/) with the introduction of Filters.

# How did we get here

So how did the `Program.cs` file come to have so few lines of code. Some features that have enabled this are: 

- [Top-level statements](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements): Introduced with C# 9 you don't have to explicitly include a `Main` method. The `Main` method is implied, it is implicitly there.

```csharp
// Before C# 9
class TestClass
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
    }
}
```
```csharp
// Introduced in C# 9
Console.WriteLine("Hello World!");
```
- [Global and implicit usings](https://devblogs.microsoft.com/dotnet/welcome-to-csharp-10/#global-and-implicit-usings): Introduced in C# 10, implicit usings feature automatically adds common global using directives.
```csharp
// format: global using <fully-qualified-namespace>;
// applies to the entire project

global using System;

global using static System.Console;
global using Env = System.Environment;
```
- [Improvements for lambda expressions](https://devblogs.microsoft.com/dotnet/welcome-to-csharp-10/#improvements-for-lambda-expressions-and-method-groups): Another feature introduced in C# 10 implicit lambda expressions.
```csharp
// Before C# 10
Func<string, int> parse = (string s) => int.Parse(s);
```
```csharp
// Introduced in C# 10
var parse = (string s) => int.Parse(s);
```
- [Attributes on lambdas](https://devblogs.microsoft.com/dotnet/welcome-to-csharp-10/#attributes-on-lambdas): In the same way you could put attributes to methods or local functions you can you put them on lambdas, Also introduced in C# 10.
```csharp
Func<string, int> parse = [Example(1)] (s) => int.Parse(s);
var choose = [Example(2)][Example(3)] object (bool b) => b ? 1 : "two";
```

# Misconceptions

Following the release of Minimal API's there have been a few misconceptions, namely the following fallacies pop to mind:

- Minimal API's will replace the MVC approach -  it is just another approach to building API's and it won't be replacing the MVC approach.
- Minimal API's mean you can't use controllers - you can use controllers if you want to or a mixture, but be aware doing so might incur the performance increase introduced by removing controllers from setup.
- All the code should go in a single Program.cs file - you can put the code where ever you like, think of it giving you more options for you choosing how you would like to structure your code. You will no longer have to conform to the MVC approach.

# Further reading

- [Creating a minimal web API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio) - Tutorial documentation from Microsoft for creating a minimal API. Also contains a instructions for creating a Todo app.
 - [Minimal APIs overview](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0) - Microsoft documentation
- [Integration testing minimal apis](https://github.com/martincostello/dotnet-minimal-api-integration-testing) - Github repo exploring integration testing

