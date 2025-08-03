Title: Testing with .NET Aspire
Published: 08/03/2025
Tags: 
- microservice architecture 
- dotnet
- dotnet 8
- dotnet aspire
- aspire

---


# 🧪 Testing with .NET Aspire

Testing distributed applications isn’t just about checking HTTP endpoints. It involves coordinating APIs, background workers, messaging systems, and databases. .NET Aspire provides a way to build and test cloud-native .NET applications as a unified experience.

In this post, I’ll cover updating a project to use Aspire’s testing framework to validate full distributed applications workflows — from HTTP APIs to message queues — with minimal infrastructure setup. All examples used are taken from this project [Regis.Pay](https://github.com/reggieray/regis-pay), an fictional payment processor created as an example of a event-driven microservice architecture project built with dotnet.

##  🔍 How it Works

.NET Aspire's testing framework designed specifically for **distributed applications**. It helps you **orchestrate, run, and verify** your entire system in automated tests, going far beyond traditional unit or integration tests by focusing on the **system as a whole**.

### Core Concepts

- **DistributedApplication**  
  Aspire represents your entire distributed app (microservices, API gateways, background workers, messaging infrastructure) as a single testable unit called a `DistributedApplication`. This abstraction lets you start, stop, and interact with the real running system from your test code.
- **DistributedApplicationTestingBuilder**  
  This builder class helps you compose the full app for testing by bootstrapping the actual application host (your production `AppHost`), configuring dependencies, and preparing resource health checks.
- **Resource Notifications & Health Checks**  
  Aspire tracks the health and readiness of individual services and resources (like APIs, message consumers, databases) in your distributed app. Tests can wait for these signals before proceeding, avoiding race conditions and flaky tests.

## 🛠️ Setting Up with Aspire

The first you'll need to do is add this nuget package to your test project.

```bash
dotnet add package Aspire.Hosting.Testing
```

Then you can use `DistributedApplicationTestingBuilder` to start your `Aspire` project. Once the distributed application is running you can create a HttpClient to call it, below is a simplified example so you can focus on the core concepts.

```csharp
namespace AspireApp.Tests;

public class Test
{
    [Fact]
    public async Task RootEndpoint_ReturnsOk()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.AspireApp_AppHost>();

        await using var app = await builder.BuildAsync();
        await app.StartAsync();

        var client = app.CreateHttpClient("web");
        await app.ResourceNotifications.WaitForResourceHealthyAsync("web");

        var response = await client.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
```

## 👀 Looking at the Regis.Pay example

At the center of the test suite is `RegisPayFixture`, a class that launches the entire application using Aspire's test builder, configures HTTP clients, and exposes essential resources for test execution.

🧩 `RegisPayFixture.cs`

```csharp
﻿using Aspire.Hosting;
using Aspire.Hosting.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Regis.Pay.EndToEndTests;

public class RegisPayFixture : IAsyncLifetime
{
    private DistributedApplication App { get; set; } = null!;

    public HttpClient? ApiClient { get; private set; }

    public string? RabbitMqConnString { get; private set; }
    
    public async Task InitializeAsync()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Regis_Pay_AppHost>();
        
        builder.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });
        
        App = await builder.BuildAsync();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        
        await App.ResourceNotifications.WaitForResourceHealthyAsync("regis-pay-eventconsumer", cts.Token);
        await App.ResourceNotifications.WaitForResourceHealthyAsync("regis-pay-api", cts.Token);
        await App.ResourceNotifications.WaitForResourceHealthyAsync("regis-pay-changefeed", cts.Token);
        await App.ResourceNotifications.WaitForResourceHealthyAsync("regis-pay-messaging", cts.Token);
        
        await App.StartAsync(cts.Token);
        
        ApiClient = App.CreateHttpClient("regis-pay-api");
        
        RabbitMqConnString = await App.GetConnectionStringAsync("regis-pay-messaging", cancellationToken: cts.Token);
    }

    public async Task DisposeAsync()
    {
        await App.StopAsync();
        await App.DisposeAsync();
    }
}
```

✅ What This Provides
- Launches the complete distributed application for testing
- Ensures all critical services are healthy before test execution
- Provides an HttpClient and RabbitMQ connection string for use in tests

This setup enables comprehensive system validation, not just isolated units.

🧪 `TestSteps.cs`

```csharp
namespace Regis.Pay.EndToEndTests;
using System.Net.Http.Json;
using FluentAssertions;
using Domain.IntegrationEvents;
using Tests.Shared.ApiClient;
using Tests.Shared.EventTestConsumer.EventTestConsumer;

public class TestSteps
{
    private readonly RegisPayFixture _fixture;
    private CreatePaymentRequest _createPaymentRequest;
    private readonly PaymentCompletedEventTestConsumer _testConsumer;
    private PaymentCompleted _paymentCompleted;
    private CreatePaymentResponse? _createPaymentResponse;

    public TestSteps(RegisPayFixture fixture)
    {
        _fixture = fixture;
        _testConsumer = new PaymentCompletedEventTestConsumer();
    }

    internal void ACreatePaymentRequest()
    {
        _createPaymentRequest = new CreatePaymentRequest(130, "GBP");
    }

    internal async Task TheCreatePaymentIsRequested()
    {
        // Setting up a queue listener to verify, other options to verify are available such as checking the completed event is in the DB
        // or creating a test harness for the notification that gets send at the end 
        _paymentCompleted = await _testConsumer.ListenToEvent(async () =>
        {
            var response = await _fixture.ApiClient.PostAsJsonAsync("api/payment/create", _createPaymentRequest);
            response.EnsureSuccessStatusCode();
            _createPaymentResponse = await response.Content.ReadFromJsonAsync<CreatePaymentResponse>();
        }, _fixture.RabbitMqConnString!);
    }

    internal void ThePaymentIsSuccessfullyCompleted()
    {
        _paymentCompleted.Should().NotBeNull(because: $"pay:{_createPaymentResponse?.PaymentId} payment was created");
        _paymentCompleted.AggregateId.Should().Be($"pay:{_createPaymentResponse?.PaymentId}");
    }
}
```

The test defines its workflow using a Given/When/Then format. It acts purely from the outside — using HttpClient to send requests and listening to RabbitMQ for expected events.

💸 `PaymentTests.cs`

```csharp
using FluentTesting;

namespace Regis.Pay.EndToEndTests;

public class PaymentTests(RegisPayFixture fixture) : IClassFixture<RegisPayFixture>
{
    private readonly TestSteps _testSteps = new(fixture);

    [Fact]
    public async Task SuccessfullyCompletedPayment()
    {
        await _testSteps
                .Given(c => c.ACreatePaymentRequest())
                .When(c => c.TheCreatePaymentIsRequested())
                .Then(c => c.ThePaymentIsSuccessfullyCompleted())
                .RunAsync();
    }
}
```

## 🤔 Summary

Instead of mocking services or spinning up parts of your app in isolation, `Aspire.Hosting.Testing` lets you run the whole distributed application — just like as it would when running in a environment. That includes:

- API projects
- Background workers
- Messaging consumers
- Databases or queues (via connectionString bindings)

This gives you end-to-end confidence that your services work together correctly. The keyword being end-to-end for me as it's worth mentioning in the Microsoft docs it uses example code with the name `IntegrationTest1`, I'm not sure if this is intentionally suggesting that these are a form of integration test, it maybe to some. The way I see it though is more of a black box test and it allows you to test a flow end-to-end, so following that descriptive behavior it aligns more with a end-to-end test for me, which is why I have ended up using the Aspire testing package in the EndToEndTests project. 