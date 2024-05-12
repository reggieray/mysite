Title: Building distributed applications with MassTransit
Published: 05/12/2024
Tags: 
- microservice architecture 
- mass transit
- dotnet
- dotnet 8

---

# What is MassTransit?

MassTransit is a mature and feature-rich messaging framework that facilitates communication between distributed components using messaging patterns such as publish/subscribe, request/response, and message routing. Built on top of .NET, MassTransit abstracts away the complexities of message queuing systems like [RabbitMQ](https://www.rabbitmq.com/), [Azure Service Bus](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview), and [Amazon SQS](https://aws.amazon.com/sqs/), enabling developers to focus on application logic rather than infrastructure concerns.

# Code Example

For this section I'll be taking code snippets from [Regis Pay](https://github.com/reggieray/regis-pay), an example event-driven microservice project, using a fictional payment processor named  Regis Pay.

For more background on "Regis Pay" you can read from following blog post:

- [.NET Event Driven Microservice (Part 1) - Transactional outbox pattern](/posts/dotnet-event-driven-microservice-part-1-transactional-outbox-pattern)
- [.NET Event Driven Microservice (Part 2) - Event Sourcing](/posts/dotnet-event-driven-microservice-part-2-event-sourcing)
- [.NET Event Driven Microservice (Part 3) - Architecture Overview](/posts/dotnet-event-driven-microservice-part-3-architecture-overview)

Now let's dive into the example code to illustrate how MassTransit can be integrated into a .NET application. In Regis Pay we have two services, one responsible for publishing messages `Regis.Pay.ChangeFeed` and one for consuming those messages `Regis.Pay.EventConsumer`. 

1. Installing `MassTransit` via Nuget:

To just install MassTransit you can use the following command:

```ps
dotnet add package MassTransit
```

However in the example project I am going to use it with RabbitMQ, so you don't need to install the above package as a prerequisites as it has it's own standalone package which you can get via using the following command: 

```ps
dotnet add package MassTransit.RabbitMQ
```

2. Defining the message contract

I have taken one event, the `PaymentInitiated` as an example. The code has been [refactored](https://github.com/reggieray/regis-pay/tree/main/src/Regis.Pay.Domain/IntegrationEvents) to `abstract` classes and a `interface`, but essentially the shape of the message would look like the following:  

```csharp
public class PaymentInitiated 
{
    public string AggregateId { get; set; } = default!;
}
```

3. Implement the consumer in `Regis.Pay.EventConsumer`

[`PaymentInitiatedConsumer.cs`](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.EventConsumer/Consumers/PaymentInitiatedConsumer.cs) is where you can see the original source code. For the example below I have striped out bits to emphasize how you can read properties from the message, in this case the `AggregateId` property. 

```csharp
public class PaymentInitiatedConsumer : IConsumer<PaymentInitiated>
{
    private readonly ILogger<PaymentInitiatedConsumer> _logger;

    public PaymentInitiatedConsumer(ILogger<PaymentInitiatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentInitiated> context)
    {
        _logger.LogInformation("Consuming {event} for paymentId: {paymentId}", nameof(PaymentInitiated), context.Message.AggregateId);
    }
}
```

4. Configure MassTransit in the `Regis.Pay.EventConsumer` service.

Again I have simplified the example below, you can find the original source code in [`ServiceCollectionExtensions.cs`](https://github.com/reggieray/regis-pay/blob/6e9fba6d0126e8128d1c0cbe006664cb4482e68b/src/Regis.Pay.Common/ServiceCollectionExtensions.cs) where it's also re-used for the publisher service `Regis.Pay.ChangeFeed`.

```csharp
services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });

    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => typeof(IConsumer).IsAssignableFrom(p) && p.IsClass &&
                            !p.IsAbstract && p.Namespace!.Contains("Regis.Pay.EventConsumer."))
                            .Select(x => x.Assembly)
                            .ToArray();

    x.AddConsumers(assemblies);
    
});
```

Here you can see you don't need to specify every consumer you want registered, but instead you can do it via scanning the assemblies for the exact types you want.

5. Send message from publisher service `Regis.Pay.ChangeFeed`

[`ChangeEventHandler.cs`](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.ChangeFeed/ChangeEventHandler.cs) is where you can see the original source code and again to keep this focused on MassTransit I have simplified the code snippet below highlight the use of the `IBus` and the usage of the `Publish` method.

```csharp
public class ChangeEventHandler(IBus bus) : IChangeEventHandler
{
    private readonly IBus _bus = bus;

    public async Task HandleAsync(IReadOnlyCollection<EventWrapper> events, CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            var integrationEvent = IntegrationEventResolver.Resolve(@event);

            if (@event.EventType == nameof(PaymentInitiated))
            {
                await _bus.Publish<PaymentInitiated>(integrationEvent);
            }
        }
    }
}
```

You would also have to register MassTransit for the `Regis.Pay.ChangeFeed` service which would be exactly like point 4. I have included it below for clarity. 

```csharp
services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});
```

To help visualize the the consumer handling the message here's a GIF of a the Regis Pay example running in Docker and handling a payment go through.

> <img src="https://raw.githubusercontent.com/reggieray/regis-pay/main/docs/images/manual-test.gif" style="max-width: 100%">

As you can in the logs it consuming the message as-well as the other consumers and all of this done with little to no configuration as MassTransit handles that all for you. That said it abstracts it that you could even swap your messaging system as mentioned at the start. 

# Summary:
MassTransit empowers .NET developers to build resilient and scalable distributed systems by abstracting away the complexities of messaging infrastructure. With its intuitive API, extensive documentation, and vibrant community, MassTransit remains a top choice for implementing message-based communication patterns in .NET applications. Whether you're building microservices, event-driven architectures, or integration solutions, MassTransit provides the tools and abstractions necessary to unlock the full potential of distributed computing.