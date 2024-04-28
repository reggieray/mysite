Title: Load testing Event Driven Microservices with NBomber 
Published: 4/28/2024
Tags: 
- nbomber
- dotnet
- dotnet 8
- csharp
- microservice architecture 

---

# Introduction

In this blog post I'll be exploring how to load test a event driven microservice. The code I use in this post can also be found in the [Regis Pay](https://github.com/reggieray/regis-pay) github repository. I'll also be using this microservice example for running the load tests 
against.

I have previously published some blog posts that you might useful as it may enhance this content which I'll link below. I won't repeat what was covered in those posts. 

- [Getting Started with NBomber](/posts/getting-started-with-nbomber)
- [.NET Event Driven Microservice (Part 1) - Transactional outbox pattern](/posts/dotnet-event-driven-microservice-part-1-transactional-outbox-pattern)
- [.NET Event Driven Microservice (Part 2) - Event Sourcing](/posts/dotnet-event-driven-microservice-part-2-event-sourcing)
- [.NET Event Driven Microservice (Part 3) - Architecture Overview](/posts/dotnet-event-driven-microservice-part-3-architecture-overview)

# Clarifications

### Load Test Types

Before I go into the problem and options of running a load test against a event driven microservice. I think it's worth mentioning that I could be mentioning a specific type of load test as listed below.  

- Stress Testing
- Load Testing
- Spike Testing
- Endurance Testing
- Volume Testing
- Concurrency Testing
- Capacity Testing
- Throughput Testing
- Latency Testing
- Scalability Testing

I won't give an overview of each type of load test, but only to clarify the load test I'll be coding is a straight forward load test, ie. a load test that would be 2x or 3x of anticipated traffic that I made up 😄.

When creating your own load tests definitions it's worth keeping this in mind, to create load tests that make sense to you and your context.

### Dependencies

When performing a load test it should be traditionally done against an environment similar to the production environment to give you valuable insight into how your microservice would perform in production. That is easier said than done though as you may have dependencies that won't like being load tested or even not expect it. 

In these cases I would assume an environment setup with stubbed dependencies or the ability to use stubbed responses so you can at least perform load tests on the components under test.

### Demo

All load tests run in the examples below were run against a locally run instance of the solution in Docker.

# Problem

Due to the async nature of event driven microservices a lot of the processing happens after the entry point, be it an API or Message or something else. For examples sake I continue under the context of a API.

It's common to find API's in microservices are very thin and only do one thing like persisting some data of some kind with the idea of keeping it minimal so it can be executed as fast as possible. This is the same case for the example microservice I created for [Regis Pay](https://github.com/reggieray/regis-pay).

Most load testing frameworks are geared to testing API's, but in a event driven world, API's may only account for a small percentage of the total code executed, which only gives you a small amount of insight to how well your components perform under load.

# Options

Before I go over the options it's worth mentioning that [NBomber](https://nbomber.com/) being a .NET testing framework has enabled faster test development because the technologies is consistent with the solution and tests, so a certain about of test code enabled to be shared.

This complete code from the snippets below can be found here: [`PaymentLoadTests.cs`](https://github.com/reggieray/regis-pay/blob/main/test/Regis.Pay.LoadTests/PaymentLoadTests.cs).


### API only

This option only tests the API which is still important as it's usually the customer facing part of the microservice.

Here it hits an endpoint and records the latency for the report. Still very useful information, not just for happy paths, but also testing unhappy paths, like making sure all your validation response execute in a timely manner.

```csharp
[Fact]
public void CreatePaymentApiLoadTest()
{
    var apiClient = RestService.For<IRegisPayApiClient>("https://localhost:4433");

    var scenario = Scenario.Create("create_payment_api_response", async context =>
    {
        var request = new CreatePaymentRequest(100000, "GBP");

        var response = await apiClient.CreatePayment(request);

        var paymentId = response?.Content?.PaymentId;

        return response!.IsSuccessStatusCode ? Response.Ok(paymentId) : Response.Fail(paymentId);
    })
    .WithLoadSimulations(
        Simulation.Inject(rate: 3,
                            interval: TimeSpan.FromSeconds(1),
                            during: TimeSpan.FromMinutes(1))
    );

    NBomberRunner
        .RegisterScenarios(scenario)
        .Run();
}
```

A drawback to this is it only has the API in the report. A workaround is having good telemetry in place so you can observe how well the whole of the microservice has performed. In this scenario the load test itself is more used as a data generator.

A drawback to this is that you also have to set something up to easily monitor the performance once the load test is over or you're unlikely to act upon any feedback.

Below is a screenshot of what a `html` report would look like for this load test.

I have also included the `html` report which you can find here: [nbomber_report_2024-04-28--20-32-18](/posts/html/microservice-load-test/2024-04-28_20.30.53_session_f6638a80/nbomber_report_2024-04-28--20-32-18.html)

> <img src="/posts/images/microservice-load-test.png" style="max-width: 100%">

I have included the `txt` output below:

```ps
test info
test suite: nbomber_default_test_suite_name
test name: nbomber_default_test_name
session id: 2024-04-28_20.30.53_session_f6638a80

scenario: create_payment_api_response
  - ok count: 180
  - fail count: 0
  - all data: 0 MB
  - duration: 00:01:00

load simulations: 
  - inject, rate: 3, interval: 00:00:01, during: 00:01:00

+--------------------+------------------------------------------------------+
| step               | ok stats                                             |
+--------------------+------------------------------------------------------+
| name               | global information                                   |
+--------------------+------------------------------------------------------+
| request count      | all = 180, ok = 180, RPS = 3                         |
+--------------------+------------------------------------------------------+
| latency            | min = 7.21, mean = 29.65, max = 61.97, StdDev = 6.11 |
+--------------------+------------------------------------------------------+
| latency percentile | p50 = 29.28, p75 = 32.27, p95 = 39.23, p99 = 48.77   |
+--------------------+------------------------------------------------------+

status codes for scenario: create_payment_api_response
+-------------+-------+---------+
| status code | count | message |
+-------------+-------+---------+
| no status   | 180   |         |
+-------------+-------+---------+
```

What's interesting is that around 45 seconds the latency started to spike for the 99th percentile. It is worth noting that this was run on Cosmos DB emulator.

### Multiple API endpoints

I haven't created code for this, but it would be similar to the next option except only API's would get called. This option also highly depends on multiple endpoints being available.

For example we are using the create payment endpoint which does a `POST` against `/api/payment/create`, we could also have a `GET` endpoint such as `/api/payment/status`, which gives use the status of that payment. We could code the load test to query this endpoint after the initial API request to finish once the desired state has been reached.

A disadvantage to this approach though is it would create a lot of requests for the `GET` endpoint, more than you would normally anticipate, but in return you can get a report that shows how well the async code execution is performing.

### API + Integration Events

Another approach and one I have included in the solution is to listen to the same integration events that the microservice is listening too.

What this also enables is tracking performance between integration events, so you can gain insight into how well a specific part of the system is performing under load. 

```csharp
[Fact]
public void FullPaymentJourneyLoadTest()
{
    var timeout = TimeSpan.FromMinutes(10);
    var apiClient = RestService.For<IRegisPayApiClient>("https://localhost:4433");

    var paymentInitiatedEvents = new MultiPaymentInitiatedEventTestConsumer();
    var paymentCreatedEvents = new MultiPaymentCreatedEventTestConsumer();
    var paymentSettledEvents = new MultiPaymentSettledEventTestConsumer();
    var paymentCompltedEvents = new MultiPaymentCompletedEventTestConsumer(); //Typo :)

    paymentInitiatedEvents.ListenToEvents();
    paymentCreatedEvents.ListenToEvents();
    paymentSettledEvents.ListenToEvents();
    paymentCompltedEvents.ListenToEvents();

    try
    {
        var scenario = Scenario.Create("full_payment_journey", async context =>
        {
            var create_payment_request_step = await Step.Run("create_payment_request", context, async () =>
            {
                var request = new CreatePaymentRequest(100000, "GBP");

                var response = await apiClient.CreatePayment(request);

                var paymentId = response?.Content?.PaymentId;

                return response!.IsSuccessStatusCode ? Response.Ok(paymentId) : Response.Fail(paymentId);
            }).WaitAsync(timeout);

            var expecetedAggregateId = $"pay:{create_payment_request_step.Payload.Value}";

            var payment_initiated_step = await Step.Run("payment_initiated", context, async () =>
            {
                while (!paymentInitiatedEvents.EventIds.TryTake(out var result))
                {
                    await Task.Delay(1);
                }

                return Response.Ok();
            }).WaitAsync(timeout);

            var payment_created_step = await Step.Run("payment_created", context, async () =>
            {
                while (!paymentCreatedEvents.EventIds.TryTake(out var result))
                {
                    await Task.Delay(1);
                }

                return Response.Ok();
            }).WaitAsync(timeout);

            var payment_settled_step = await Step.Run("payment_settled", context, async () =>
            {
                while (!paymentSettledEvents.EventIds.TryTake(out var result))
                {
                    await Task.Delay(1);
                }

                return Response.Ok();
            }).WaitAsync(timeout);

            var payment_completed_step = await Step.Run("payment_completed", context, async () =>
            {
                while (!paymentCompltedEvents.EventIds.TryTake(out var result))
                {
                    await Task.Delay(1);
                }

                return Response.Ok();
            }).WaitAsync(timeout);

            return Response.Ok();
        })
        .WithLoadSimulations(
            Simulation.Inject(rate: 1,
                                interval: TimeSpan.FromSeconds(1),
                                during: TimeSpan.FromSeconds(30))
        );

        NBomberRunner
            .RegisterScenarios(scenario)
            .Run();
    }
    catch
    (Exception)
    { }
    finally 
    {
        paymentInitiatedEvents.Dispose();
        paymentCreatedEvents.Dispose();
        paymentSettledEvents.Dispose();
        paymentCompltedEvents.Dispose();
    }
}
```

To make this more interesting I added a significant delay to one of the consumers to highlight the ability to see how each stage performs under load with one being an outlier.

The specific consumer I altered was [PaymentSettledConsumer.cs](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.EventConsumer/Consumers/PaymentSettledConsumer.cs). So we should see the performance of the `PaymentCompleted` change. 

```csharp
using MassTransit;
using Regis.Pay.Domain;
using Regis.Pay.Domain.IntegrationEvents;

namespace Regis.Pay.EventConsumer.Consumers
{
    public class PaymentSettledConsumer : IConsumer<PaymentSettled>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentSettledConsumer> _logger;

        public PaymentSettledConsumer(
            IPaymentRepository paymentRepository,
            ILogger<PaymentSettledConsumer> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentSettled> context)
        {
            _logger.LogInformation("Consuming {event} for paymentId: {paymentId}", nameof(PaymentSettled), context.Message.AggregateId);

            var payment = await _paymentRepository.LoadAsync(context.Message.AggregateId);

            Random r = new Random();
            int rInt = r.Next(5000, 9000); //<--- Added this for a significant delay

            await Task.Delay(rInt); // Do some process here on payment settled. eg. send out webhook.

            payment.Complete();

            await _paymentRepository.SaveAsync(payment);
        }
    }
}

```

The `html` report for this can be found here: [nbomber_report_2024-04-28--21-01-50](/posts/html/microservice-load-test/2024-04-28_20.59.27_session_3a2f65dd/nbomber_report_2024-04-28--21-01-50.html) which is what the screenshot below is of.

> <img src="/posts/images/microservice-load-test-2.png" style="max-width: 100%">

The `txt` output I have included below:

```ps
test info
test suite: nbomber_default_test_suite_name
test name: nbomber_default_test_name
session id: 2024-04-28_20.59.27_session_3a2f65dd

scenario: full_payment_journey
  - ok count: 30
  - fail count: 0
  - all data: 0 MB
  - duration: 00:00:30

load simulations: 
  - inject, rate: 1, interval: 00:00:01, during: 00:00:30

+--------------------+------------------------------------------------------------------+
| step               | ok stats                                                         |
+--------------------+------------------------------------------------------------------+
| name               | global information                                               |
+--------------------+------------------------------------------------------------------+
| request count      | all = 30, ok = 30, RPS = 1                                       |
+--------------------+------------------------------------------------------------------+
| latency            | min = 9867.31, mean = 23092.84, max = 44284.35, StdDev = 9144.35 |
+--------------------+------------------------------------------------------------------+
| latency percentile | p50 = 19283.97, p75 = 30064.64, p95 = 41091.07, p99 = 44302.34   |
+--------------------+------------------------------------------------------------------+
|                    |                                                                  |
+--------------------+------------------------------------------------------------------+
| name               | create_payment_request                                           |
+--------------------+------------------------------------------------------------------+
| request count      | all = 30, ok = 30, RPS = 1                                       |
+--------------------+------------------------------------------------------------------+
| latency            | min = 8.53, mean = 31.35, max = 43.47, StdDev = 8.38             |
+--------------------+------------------------------------------------------------------+
| latency percentile | p50 = 31.34, p75 = 37.92, p95 = 42.78, p99 = 43.49               |
+--------------------+------------------------------------------------------------------+
|                    |                                                                  |
+--------------------+------------------------------------------------------------------+
| name               | payment_initiated                                                |
+--------------------+------------------------------------------------------------------+
| request count      | all = 30, ok = 30, RPS = 1                                       |
+--------------------+------------------------------------------------------------------+
| latency            | min = 249.86, mean = 2806.65, max = 4899.71, StdDev = 1417.59    |
+--------------------+------------------------------------------------------------------+
| latency percentile | p50 = 2826.24, p75 = 4057.09, p95 = 4853.76, p99 = 4902.91       |
+--------------------+------------------------------------------------------------------+
|                    |                                                                  |
+--------------------+------------------------------------------------------------------+
| name               | payment_created                                                  |
+--------------------+------------------------------------------------------------------+
| request count      | all = 30, ok = 30, RPS = 1                                       |
+--------------------+------------------------------------------------------------------+
| latency            | min = 0.01, mean = 5055.23, max = 15167.45, StdDev = 3452.55     |
+--------------------+------------------------------------------------------------------+
| latency percentile | p50 = 5046.27, p75 = 5074.94, p95 = 10215.42, p99 = 15171.58     |
+--------------------+------------------------------------------------------------------+
|                    |                                                                  |
+--------------------+------------------------------------------------------------------+
| name               | payment_settled                                                  |
+--------------------+------------------------------------------------------------------+
| request count      | all = 30, ok = 30, RPS = 1                                       |
+--------------------+------------------------------------------------------------------+
| latency            | min = 0, mean = 5058.94, max = 10137.04, StdDev = 3450.36        |
+--------------------+------------------------------------------------------------------+
| latency percentile | p50 = 5062.66, p75 = 5124.1, p95 = 10133.5, p99 = 10141.7        |
+--------------------+------------------------------------------------------------------+
|                    |                                                                  |
+--------------------+------------------------------------------------------------------+
| name               | payment_completed                                                |
+--------------------+------------------------------------------------------------------+
| request count      | all = 30, ok = 30, RPS = 1                                       |
+--------------------+------------------------------------------------------------------+
| latency            | min = 14.77, mean = 10139.33, max = 30394.29, StdDev = 9050.55   |
+--------------------+------------------------------------------------------------------+
| latency percentile | p50 = 10067.97, p75 = 15196.16, p95 = 30392.32, p99 = 30408.7    |
+--------------------+------------------------------------------------------------------+

status codes for scenario: full_payment_journey
+-------------+-------+---------+
| status code | count | message |
+-------------+-------+---------+
| no status   | 180   |         |
+-------------+-------+---------+
```

As you can see the latency for `payment_completed` is significantly higher. This is because this is the stage of which the altered consumer affects.

# Summary

By leveraging tools like NBomber, developers can effectively assess the behavior of their microservices under  load conditions with multiple options to assess at their disposal. Giving valuable insight into how well isolated parts of the microservice perform under load. Given that NBomber is also a .NET framework it makes it a strong choice for developers working on a .NET solution with the ability to code share for faster load test development.