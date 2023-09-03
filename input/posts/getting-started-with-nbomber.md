Title: Getting started with NBomber
Published: 9/03/2023
Tags: 
- dotnet
- csharp
- nbomber

---

# Introduction

Load testing is an essential practice for ensuring the performance and scalability of your web applications. It helps you identify bottlenecks, optimize your system, and ensure that it can handle real-world traffic without breaking a sweat. However, setting up and running load tests can be a complex and time-consuming task. This is where NBomber makes it easy!

NBomber is an open-source, extensible, and highly configurable load testing framework for .NET applications. In this blog post, I'll create a bare bones example, demonstrating how easy it is to get started. 

# Example

To break it down, I will be doing the following in order:

- Create example web API
- Create performance test for example web API
- Run performance test

## Prerequisites:

Make sure you have the following prerequisites installed:

1. **.NET SDK**: You'll need the .NET SDK installed on your machine. You can download it from the [official .NET website](https://dotnet.microsoft.com/en-us/download).
2. **Visual Studio or Visual Studio Code**: You can use either of these development environments for coding and running NBomber tests.

## Implementation:

1. **Create webapi** to write the performance tests for.

```ps
> mkdir nbomber
> cd .\nbomber\
> dotnet new webapi -n DemoAPI
The template "ASP.NET Core Web API" was created successfully.
> dotnet run --project .\DemoAPI\DemoAPI.csproj
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5195
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Repos\nbomber\DemoAPI
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...
```

Take note of the port, as we'll use this later. Keep the webapi running and open another terminal and navigate to 'nbomber' location to proceed.

2. **Create console** app to be used for the performance test. Navigate to folder.

```ps
> dotnet new console -n PerformanceTests
The template "Console App" was created successfully.
> cd .\PerformanceTests\
```

3. **Add NBomber nuget** to console app

```ps
> dotnet add package NBomber
Determining projects to restore...
  Writing C:\Users\regis\AppData\Local\Temp\tmp58C5.tmp
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adding PackageReference for package 'NBomber' into project 'C:\Repos\nbomber\PerformanceTests\PerformanceTests.csproj'.
### output shortened for brevity ### 
```

4. **Update `program.cs`** with the following code below. 

```csharp
using NBomber.CSharp;

internal class Program
{
    private static void Main(string[] args)
    {
        using var httpClient = new HttpClient();

        var scenario = Scenario.Create("load_test", async context =>
            {
                var response = await httpClient.GetAsync("http://localhost:5195/weatherforecast");

                return response.IsSuccessStatusCode
                    ? Response.Ok()
                    : Response.Fail();
            })
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.Inject(rate: 30, 
                                  interval: TimeSpan.FromSeconds(1),
                                  during: TimeSpan.FromSeconds(30))
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .Run();
    }
}
```

Notice the port shown in pervious step added in the Get request. 

5. **Run performance test** 

```csharp
> dotnet run --project .\PerformanceTests\PerformanceTests.csproj -c Release
  _   _   ____                        _                       ____
 | \ | | | __ )    ___    _ __ ___   | |__     ___   _ __    | ___|
 |  \| | |  _ \   / _ \  | '_ ` _ \  | '_ \   / _ \ | '__|   |___ \
 | |\  | | |_) | | (_) | | | | | | | | |_) | |  __/ | |       ___) |
 |_| \_| |____/   \___/  |_| |_| |_| |_.__/   \___| |_|      |____/

23:16:48 [INF] NBomber "5.2.1" started a new session: "2023-09-03_22.16.60_session_ed8557fc"
23:16:48 [INF] NBomber started as single node
### output shortened for brevity ### 
```

6. **View results**

You should be given a folder location to the results. Navigate to that location and open the index.html file. Here's an example I generated from running the test [report file](/posts/html/nbomber/nbomber_report_2023-09-03--22-17-23).

# Supporting links

- [Example github repo](https://github.com/reggieray/nbomber-example)
- [NBomber github repo](https://github.com/PragmaticFlow/NBomber)