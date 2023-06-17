Title: Dotnet Health Checks
Published: 6/8/2023
Tags: 
- dotnet
- health checks
- docker
- .http file

---

# What are health checks in dotnet?

Health checks in .NET are a way to monitor the health of your application and its dependencies. They help you periodically check the state of critical components, such as databases or external services, to ensure they are working properly.

Health checks are typically implemented as endpoints or middleware in your application that respond to HTTP requests and return a status indicating the health of the application or its dependencies. The status can be one of the following:

- Healthy: Indicates that the component or dependency being checked is functioning correctly.
- Degraded: Indicates that the component or dependency is experiencing some issues or degradation in performance but is still operational.
- Unhealthy: Indicates that the component or dependency is not functioning correctly and requires attention.

By implementing health checks, you can improve the reliability and availability of your application.

# Example

This [github repository](https://github.com/reggieray/dotnet-health-checks) show cases how health checks can be implemented, it covers some of the following:   

- Library health check
- Custom health check
- Health check filters
- Custom output

The github repo was created to show how health checks work when they succeed or fail by simply updating some environment variables. A .http file has also been added for ease of endpoint exportation and manual testing.

## Basic

Firstly I'll cover the basics of setting up health checks.

This example would be the setup in a `Program.cs` file for a minimal API project. This is all that is need to get started.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks() //Adds health checks

var app = builder.Build();

app.MapHealthChecks("/healthz"); //Registers endpoint for health checks

app.Run();
```

## Library Health Check

There are already many health checks available via Nuget. To get a idea of what is available, have a look at this [Nuget search result](https://www.nuget.org/packages?q=AspNetCore.HealthChecks&frameworks=&tfms=&packagetype=&prerel=true&sortby=relevance).

In this example I will make use of a heath check that checks a URL for a successful response.

Add this nuget reference

```xml
<PackageReference Include="AspNetCore.HealthChecks.Uris" Version="6.0.3" />
```

Then you can add the health check

```csharp
services.AddHealthChecks()
                .AddUrlGroup(new Uri("https://matthewregis.dev"), name: "UriCheck");
```

## Custom Health Check

Although there are many health check libraries available, they might not fit your needs, in which case creating your own health check is probably your best option.

Create a health check class that implements the `IHealthCheck` interface. In the example below you can see you can make use of dependency injection. This specific example is a health check that checks that a config value has been populated, this is a more realistic example that might be used in a real world application.

```csharp
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Healthz.Api
{
    public class MyCustomStartUpHealthCheck : IHealthCheck
    {
        private readonly IConfiguration configuration;

        public MyCustomStartUpHealthCheck(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(this.configuration[$"Db:ConnectionString"]))
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("'Db:ConnectionString' empty!"));
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
```

Then you can register the health check like the following:

```csharp
services.AddHealthChecks()
                .AddCheck<MyCustomStartUpHealthCheck>(nameof(MyCustomStartUpHealthCheck));
```


> **_NOTE:_**  The [MyCustomStartUpHealthCheck.cs](https://github.com/reggieray/dotnet-health-checks/blob/main/src/Healthz.Api/MyCustomStartUpHealthCheck.cs) example in the github repo returns a Healthy or Unhealthy result based of what a configuration value is set too. This is not a health check you would use in the real world, it was created for demonstrating purposes of what a Healthy or Unhealthy result looks like by easily updating environment variables and seeing the results.

## Health check filter

You can register health checks with tags and filter on those tags so you only run a subset of health checks matching those tags. One use case in having start up checks and liveness checks separate, so you only run start up checks when a application starts and then run liveness checks at a certain timed interval.

An example of this is in the [HealthCheckExtensions.cs](https://github.com/reggieray/dotnet-health-checks/blob/main/src/Healthz.Api/HealthCheckExtensions.cs) in the github repo mentioned previously. 

First register with tags.

```csharp
public static void RegisterHealthChecks(this IServiceCollection services, string? healthCheckUri)
{
    var url = string.IsNullOrEmpty(healthCheckUri) ? "http://localhost" : healthCheckUri;

    services.AddHealthChecks()
        .AddUrlGroup(new Uri(url), name: "UriCheck", tags: new[] { "live", "all" })
        .AddCheck<MyCustomStartUpHealthCheck>(nameof(MyCustomStartUpHealthCheck), tags: new[] { "ready", "all" });
}
```

Secondly filter on those tags. Here three separate endpoints were created and only runs health checks with those tags associated with it.

```csharp
public static void MapHealthChecks(this WebApplication app)
{
    app.MapHealthChecks("/healthz/live", new HealthCheckOptions
    {
        ResponseWriter = WriteResponse,
        Predicate = healthCheck => healthCheck.Tags.Contains("live")
    });

    app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
    {
        ResponseWriter = WriteResponse,
        Predicate = healthCheck => healthCheck.Tags.Contains("ready")
    });

    app.MapHealthChecks("/healthz/all", new HealthCheckOptions
    {
        ResponseWriter = WriteResponse,
        Predicate = healthCheck => healthCheck.Tags.Contains("all")
    });
}
```

If you are familiar with with Kubernetes, you can configure your deployment with readiness and liveness probes which is a prefect use case for separating your health checks.

## Custom output

This was taken from the [Health checks : Customize output](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-7.0#customize-output) documentation as an example.

This highlights how you can customise the output how you see fit, you don't necessarily have to implement it like the following. Also worth noting that there are existing health check frameworks that would expect a response to be in a certain structure, this might help drive your decision on what structure to use. 

```csharp
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = WriteResponse
});
```

```csharp
private static Task WriteResponse(HttpContext context, HealthReport healthReport)
{
    context.Response.ContentType = "application/json; charset=utf-8";

    var options = new JsonWriterOptions { Indented = true };

    using var memoryStream = new MemoryStream();
    using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
    {
        jsonWriter.WriteStartObject();
        jsonWriter.WriteString("status", healthReport.Status.ToString());
        jsonWriter.WriteStartObject("results");

        foreach (var healthReportEntry in healthReport.Entries)
        {
            jsonWriter.WriteStartObject(healthReportEntry.Key);
            jsonWriter.WriteString("status",
                healthReportEntry.Value.Status.ToString());
            jsonWriter.WriteString("description",
                healthReportEntry.Value.Description);
            jsonWriter.WriteStartObject("data");

            foreach (var item in healthReportEntry.Value.Data)
            {
                jsonWriter.WritePropertyName(item.Key);

                JsonSerializer.Serialize(jsonWriter, item.Value,
                    item.Value?.GetType() ?? typeof(object));
            }

            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndObject();
        }

        jsonWriter.WriteEndObject();
        jsonWriter.WriteEndObject();
    }

    return context.Response.WriteAsync(
        Encoding.UTF8.GetString(memoryStream.ToArray()));
}
```

# References

- [Health checks in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks)
- [Dotnet health checks example](https://github.com/reggieray/dotnet-health-checks)