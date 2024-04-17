Title: .NET Event Driven Microservice (Part 1) - Transactional outbox pattern
Published: 4/12/2024
Tags: 
- azure
- cosmosdb
- patterns
- microservice architecture 

---

# Introduction

This will be the start of a multi part blog post where I go over an example event-driven microservice project, using a fictional payment processor named [Regis Pay](https://github.com/reggieray/regis-pay) as an example.

In this blog post I'll go over the transactional outbox pattern with Azure Cosmos DB. If you've done a bit of research on this topic before you've probably seen Microsoft documentation with the same title, which I'll link here: [Transactional Outbox pattern with Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/architecture/databases/guide/transactional-outbox-cosmos). It's worth reading this as it gives a good explanation of this pattern and why you would want to use it. 

# Pattern principles

I'll try not to repeat the contents of the Microsoft documentation, as it's also worth noting that is specifically written with Azure services in mind. Here is a short explanation of the pattern without being technology specific:

1. **Database Transaction**: When an operation is performed that involves writing to the primary data store (e.g., inserting or updating records in a database), the Transactional Outbox pattern ensures that this operation is wrapped within a database transaction. This transaction guarantees that either all changes are committed or none at all, maintaining data consistency.
2. **Outbox Table**: Alongside the primary operation, a record is inserted into an "outbox" table within the same database transaction. This outbox record contains information about the action that was performed (e.g., the data that was modified) and any additional metadata required for processing the action.
3. **Outbox Processor**: A separate component, often referred to as an "Outbox Processor" or "Event Publisher," periodically polls the outbox table for new records. When it detects a new record, it processes the information contained within it and sends corresponding messages or events to external systems or services.

Here's a very crude diagram I made up to illustrate the above points and how they fit together. 

> <img src="/posts/images/transactional-outbox-diagram.drawio.png" style="max-width: 100%">

# Azure Cosmos DB

So bringing it back specifically to Azure Cosmos DB, why use this as the database? Here I'll go over the what and why.

## What?

Before I delve into the why, I'll go over the what. Here's a quote taken from this [wiki page](https://en.wikipedia.org/wiki/Cosmos_DB).

> Azure Cosmos DB is a globally distributed, multi-model database service offered by Microsoft. It is designed to provide high availability, scalability, and low-latency access to data for modern applications.

There are many advantages to using this database which you can explore on the product page [Azure Cosmos DB](https://azure.microsoft.com/en-gb/products/cosmos-db) and find out more from a technical aspect with the [Azure Cosmos DB documentation](https://learn.microsoft.com/en-us/azure/cosmos-db/). Alternatives are available, [Amazon DynamoDB](https://aws.amazon.com/dynamodb/) come to mind. 

## Why?

The TL;DR answer, the **Change Feed** feature which you can read more about on [Change feed in Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/change-feed).

This makes it a perfect fit for the transactional outbox pattern. The change feed is the process of receiving the info from the outbox table, except in Cosmos DB terminology it's referred to as `leases` and Cosmos DB has built the syncing of change events, so you don't have too.

# Code 

So what does it look like? In [Regis Pay](https://github.com/reggieray/regis-pay) here is the [`Worker.cs`](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.ChangeFeed/Worker.cs) code for setting up the change feed processor:

```csharp
using Microsoft.Azure.Cosmos;
using Regis.Pay.Common.Configuration;
using Regis.Pay.Common.EventStore;

namespace Regis.Pay.ChangeFeed
{
    public class Worker(
        ILogger<Worker> logger,
        CosmosClient cosmosClient,
        CosmosConfigOptions cosmosConfigOptions,
        IChangeEventHandler changeEventHandler) : BackgroundService
    {
        private readonly ILogger<Worker> _logger = logger;
        private readonly CosmosClient _cosmosClient = cosmosClient;
        private readonly CosmosConfigOptions _cosmosConfigOptions = cosmosConfigOptions;
        private readonly IChangeEventHandler _changeEventHandler = changeEventHandler;
        private ChangeFeedProcessor _changeFeedProcessor;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _changeFeedProcessor = await StartChangeFeedProcessorAsync();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _changeFeedProcessor.StopAsync();
        }

        private async Task<ChangeFeedProcessor> StartChangeFeedProcessorAsync()
        {
            var leaseContainer = _cosmosClient.GetContainer(_cosmosConfigOptions.DatabaseName, _cosmosConfigOptions.LeasesContainerName);
            ChangeFeedProcessor changeFeedProcessor = _cosmosClient.GetContainer(_cosmosConfigOptions.DatabaseName, _cosmosConfigOptions.ContainerName)
                .GetChangeFeedProcessorBuilder<EventWrapper>(processorName: "eventsChangeFeed", onChangesDelegate: HandleChangesAsync)
                    .WithInstanceName("Regis.Pay.ChangeFeed")
                    .WithLeaseContainer(leaseContainer)
                    .Build();

            _logger.LogInformation("Starting Change Feed Processor...");
            await changeFeedProcessor.StartAsync();
            _logger.LogInformation("Change Feed Processor started.");
            return changeFeedProcessor;
        }

        async Task HandleChangesAsync(
                ChangeFeedProcessorContext context,
                IReadOnlyCollection<EventWrapper> events,
                CancellationToken cancellationToken)
        {
            await _changeEventHandler.HandleAsync(events, cancellationToken);
        }
    }
}
```

The important part is in method `StartChangeFeedProcessorAsync()`, specifically this code block:

```csharp
var leaseContainer = _cosmosClient.GetContainer(_cosmosConfigOptions.DatabaseName, _cosmosConfigOptions.LeasesContainerName);
            ChangeFeedProcessor changeFeedProcessor = _cosmosClient.GetContainer(_cosmosConfigOptions.DatabaseName, _cosmosConfigOptions.ContainerName)
                .GetChangeFeedProcessorBuilder<EventWrapper>(processorName: "eventsChangeFeed", onChangesDelegate: HandleChangesAsync)
                    .WithInstanceName("Regis.Pay.ChangeFeed")
                    .WithLeaseContainer(leaseContainer)
                    .Build();
```

Breaking it down:
- Create a `ChangeFeedProcessor`: By getting the container you want to listen to change events and using the `GetChangeFeedProcessorBuilder<T>` method.
- Specify the lease container: This creates a lease container which keeps track of the changes.
- `onChangesDelegate: HandleChangesAsync`: The method that processes the change events. This then gets forwarded onto `IChangeEventHandler` which I'll go over below.

The [`ChangeEventHandler.cs`](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.ChangeFeed/ChangeEventHandler.cs) contains the logic for handling the changes. 


```csharp
using MassTransit;
using Regis.Pay.Common.EventStore;
using Regis.Pay.Domain;

namespace Regis.Pay.ChangeFeed
{
    public interface IChangeEventHandler
    {
        Task HandleAsync(IReadOnlyCollection<EventWrapper> events, CancellationToken cancellationToken);
    }

    public class ChangeEventHandler(
        IBus bus,
        ILogger<ChangeEventHandler> logger) : IChangeEventHandler
    {
        private readonly IBus _bus = bus;
        private readonly ILogger<ChangeEventHandler> _logger = logger;

        public async Task HandleAsync(IReadOnlyCollection<EventWrapper> events, CancellationToken cancellationToken)
        {
            foreach (var @event in events)
            {
                _logger.LogInformation("Detected change feed {event} for {eventId}", @event.EventType, @event.Id);

                var integrationEvent = IntegrationEventResolver.Resolve(@event);

                if (integrationEvent is null)
                {
                    _logger.LogInformation("No integration event found for event with {eventId}", @event.Id);
                    break;
                }

                await _bus.Publish(integrationEvent, cancellationToken);
            }

            _logger.LogInformation("Finished handling changes.");
        }
    }
}
```
Breaking it down:

- The `IChangeEventHandler` interface defines a method for handling change feed events.
- The `IntegrationEventResolver` is a separate utility class for resolving integration events based on `EventWrapper`.
- The `IBus` dependency represents the MassTransit bus for event publishing, which is currently setup to use RabbitMQ.

The change event handler will handle multiple changes and publish multiple events. Taking one domain event as an example, say the [`PaymentInitiated.cs`](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.Domain/Events/PaymentInitiated.cs) domain event. This would result in a [`PaymentInitiated.cs`](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.Domain/IntegrationEvents/PaymentInitiated.cs) integration event being published. 

This should then be consumed by the [`PaymentInitiatedConsumer.cs`](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.EventConsumer/Consumers/PaymentInitiatedConsumer.cs) which contains the logic for handling/consuming the event.

```csharp
using MassTransit;
using Regis.Pay.Domain;
using Regis.Pay.Domain.IntegrationEvents;

namespace Regis.Pay.EventConsumer.Consumers
{
    public class PaymentInitiatedConsumer : IConsumer<PaymentInitiated>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentInitiatedConsumer> _logger;

        public PaymentInitiatedConsumer(
            IPaymentRepository paymentRepository,
            ILogger<PaymentInitiatedConsumer> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentInitiated> context)
        {
            _logger.LogInformation("Consuming {event} for paymentId: {paymentId}", nameof(PaymentInitiated), context.Message.AggregateId);

            var payment = await _paymentRepository.LoadAsync(context.Message.AggregateId);

            await Task.Delay(300); // Do some process here on payment initiated. eg. save to SQL database or third party system.

            payment.Created();

            await _paymentRepository.SaveAsync(payment);
        }
    }
}
```

In this example:
- Loads the aggregate/payment
- Executes a `Task.Delay(300)` which represents some kind of process
- Updates the aggregate in memory
- Then finally persists the updates to the aggregate to the database.

You can then repeat this process of the change feed to consumer until you have the desired state.

## Notes

There are a couple things I wanted to mention with regards to the current setup: 

- The current implementation uses RabbitMQ which defaults to using a [Fanout Exchange](https://www.rabbitmq.com/tutorials/amqp-concepts#exchange-fanout) when publishing a message. This is mainly for demo purposes, but another typical way of setting this up is having a **Topic** and then create a **Queue** for each of your consumers and if you want a consumer to handle a event sent to a topic, you can create a **Subscription** to forward to the Queue given filter conditions have been met. Keeping Azure ServiceBus in mind you can find more info on [Service Bus queues, topics, and subscriptions](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-queues-topics-subscriptions).
- I glossed over the aggregate, which I will go over in a separate blog post on Event Sourcing as this is also a important part to this architecture.

# Summary

The transactional outbox pattern is a important pattern to know in the microservice world, it's one I see heavily used by Companies with a lot of microservices and for good reason it's a battle tested approach that works well, keeping all the [“-ilities”](https://codesqueeze.com/the-7-software-ilities-you-need-to-know/) in mind.

