Title: .NET Event Driven Microservice (Part 2) - Event Sourcing
Published: 4/22/2024
Tags: 
- azure
- cosmosdb
- patterns
- microservice architecture 

---

# Introduction

This is part of a multi part blog post where I go over an example event-driven microservice project, using a fictional payment processor named [Regis Pay](https://github.com/reggieray/regis-pay) as an example. To see the previous blog post click this link on the [transactional outbox pattern](/posts/dotnet-event-driven-microservice-part-1-transactional-outbox-pattern).

This blog post will be exploring event sourcing.

## Event Sourcing definition

> Event sourcing is a software design pattern that involves modeling the state of an application as a sequence of events. Instead of storing the current state of an entity, you store a log of events that describe actions that have occurred. These events are immutable and represent changes in the system.

Here's how it typically works:

1. Event Creation: Whenever a state change occurs in the system, instead of directly updating the state, an event is generated to represent that change. For example, if you're dealing with an e-commerce system, events could include "order placed," "payment received," or "item shipped."
1. Event Storage: Events are stored in an event log or event store. This log contains a chronological sequence of events that have taken place in the system. Each event is immutable and represents a fact about the system at a particular point in time.
1. Rebuilding State: The current state of any entity in the system is derived by replaying the events from the event log. This process involves starting with an initial state and then applying each event in the log to that state in chronological order. By doing this, you can reconstruct the state of the system at any point in time by replaying events from the beginning.


Event sourcing offers several benefits, including:

- Auditability and Traceability: Since every state change is represented as an event, you have a complete audit trail of everything that has happened in the system, which can be useful for debugging, compliance, and analytics.
- Scalability and Performance: Event sourcing can improve scalability and performance because it's append-only. You're always adding new events to the log, rather than updating existing records, which can reduce contention and improve concurrency.
- Temporal Queries: Since you have a full history of events, you can perform temporal queries, such as "What was the state of this entity at a particular point in time?" or "What events led to this particular state?"
- Flexibility and Extensibility: It's easier to evolve and extend your system over time because you can add new types of events without changing existing code or data schemas. This makes it particularly suitable for systems that need to evolve rapidly or accommodate changing business requirements.


However, event sourcing also introduces complexity, especially around managing event consistency, versioning, and handling distributed systems. It's not a one-size-fits-all solution, but it can be powerful in the right context, such as domains with complex business logic or regulatory requirements.


## Event Storming definition

Before diving straight into the code example it's worth mentioning a technique that is useful when exploring the usage of event sourcing and that is **Event Storming**. This technique can particularly useful when it comes to defining the domain events that you store against your domain aggregate.

> Event Storming is a collaborative modeling technique used primarily in Domain-Driven Design (DDD) to explore complex business domains and design software systems. It's a workshop format where stakeholders from various backgrounds, including domain experts, developers, and business analysts, come together to visualize and understand the domain's behavior through events.

Here's how Event Storming typically works:

1. Gather Participants: Invite stakeholders who have knowledge about the domain under discussion. This may include domain experts, developers, product owners, and other relevant parties.
1. Setup: Arrange a large physical space where participants can gather around a long wall or board. Provide ample supplies such as sticky notes, markers, and space for writing.
Start with Events: The facilitator begins by introducing the concept of "events" as significant occurrences within the domain. Events are factual statements about something that has happened or will happen.
1. Event Modeling: Participants start by identifying and writing down events on sticky notes, using a "post-it" note for each event. These events are often written in the past tense to indicate something that has already occurred. Events can represent anything from user actions, system states, or external interactions.
1. Event Ordering: Participants then organize the events on the board, arranging them in chronological order or by dependencies. This helps in understanding the flow of events and their relationships.
1. Aggregates and Commands: Alongside events, participants may identify "aggregates" (domain objects that group related data and enforce consistency) and "commands" (requests or actions that trigger events).
1. Bounded Contexts: As the workshop progresses, participants may identify different bounded contexts within the domain, which represent distinct areas of functionality or meaning. Bounded contexts help in delineating the scope of different parts of the system.
1. Discover Insights: Throughout the event storming session, participants discuss, refine, and iterate on the events and their relationships. This often leads to valuable insights into the domain's behavior and requirements.
1. Documentation: The outcome of an event storming session can be captured in various forms, including photographs of the board, digital documentation, or more formalized diagrams.


Event Storming encourages a collaborative, visual approach to domain exploration, fostering shared understanding among stakeholders and helping teams uncover complex domain logic and requirements. It's particularly useful in the early stages of a project when teams are trying to grasp the intricacies of a new domain or when redesigning existing systems.


# Exploring the example

## Code

### Common

The code in this section mainly focuses on persisting the data to the event store, with the implementation aware of the technology used, in this case Cosmos DB.

[IEventStore.cs](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.Common/EventStore/IEventStore.cs) - This interface defines the contract for an event store, which includes methods for loading event streams and appending events to a stream.

```csharp
namespace Regis.Pay.Common.EventStore
{
    public interface IEventStore
    {
        Task<EventStream> LoadStreamAsync(string streamId);

        Task<bool> AppendToStreamAsync(
            string streamId,
            int expectedVersion,
            IEnumerable<IDomainEvent> events);
    }
}
```

[CosmosEventStore.cs](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.Common/EventStore/CosmosEventStore.cs) - This class `CosmosEventStore` implements the `IEventStore` interface. It provides functionality to load event streams and append events to a Cosmos DB container.

```csharp
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Regis.Pay.Common.Configuration;

namespace Regis.Pay.Common.EventStore
{
    public class CosmosEventStore : IEventStore
    {
        private readonly IEventTypeResolver _eventTypeResolver;
        private readonly Container _container;
        private readonly CosmosConfigOptions _cosmosConfigOptions;

        public CosmosEventStore(
            IEventTypeResolver eventTypeResolver,
            Container container,
            CosmosConfigOptions cosmosConfigOptions)
        {
            _eventTypeResolver = eventTypeResolver;
            _container = container;
            _cosmosConfigOptions = cosmosConfigOptions;
        }

        public async Task<EventStream> LoadStreamAsync(string streamId)
        {
            var sqlQueryText = "SELECT * FROM events e"
                + " WHERE e.stream.id = @streamId"
                + " ORDER BY e.stream.version";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText)
                .WithParameter("@streamId", streamId);

            int version = 0;
            var events = new List<IDomainEvent>();

            FeedIterator<EventWrapper> feedIterator = _container.GetItemQueryIterator<EventWrapper>(queryDefinition);
            while (feedIterator.HasMoreResults)
            {
                FeedResponse<EventWrapper> response = await feedIterator.ReadNextAsync();
                foreach (var eventWrapper in response)
                {
                    version = eventWrapper.StreamInfo.Version;

                    events.Add(eventWrapper.GetEvent(_eventTypeResolver));
                }
            }

            return new EventStream(streamId, version, events);
        }

        public async Task<bool> AppendToStreamAsync(string streamId, int expectedVersion, IEnumerable<IDomainEvent> events)
        {
            var partitionKey = new PartitionKey(streamId);

            dynamic[] parameters =
            [
                streamId,
                expectedVersion,
                SerializeEvents(streamId, expectedVersion, events)
            ];

            return await _container.Scripts.ExecuteStoredProcedureAsync<bool>("spAppendToStream", partitionKey, parameters);
        }

        private static string SerializeEvents(string streamId, int expectedVersion, IEnumerable<IDomainEvent> events)
        {
            var items = events.Select(e => new EventWrapper
            {
                Id = $"{streamId}:{++expectedVersion}",
                StreamInfo = new StreamInfo
                {
                    Id = streamId,
                    Version = expectedVersion
                },
                EventType = e.GetType().Name,
                EventData = JObject.FromObject(e)
            });

            return JsonConvert.SerializeObject(items);
        }
    }
}
```

### Domain

This section focuses on code related to the domain as the title implies. Pay attention (no pun intended) to how the payment is loaded. In the previous section events are retrieved from the event store in order of `version`, this list of events is then passed into the `Mutate` method on the `AggregateBase.cs` class. This will replay the events in order, invoking the associated `When` method for the domain event. 

[PaymentRepository.cs](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.Domain/PaymentRepository.cs) - This class `PaymentRepository` is responsible for loading and saving payments using an event store.

```csharp
using Regis.Pay.Common.EventStore;

namespace Regis.Pay.Domain
{
    public class PaymentRepository : IPaymentRepository
    {
        private const string StreamIdPrefix = "pay";
        private readonly IEventStore _eventStore;

        public PaymentRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Payment> LoadAsync(string streamId)
        {
            var stream = await _eventStore.LoadStreamAsync(streamId);

            if (stream is null)
            {
                throw new Exception($"Unable to find payment for streamId: {streamId}"); //Add custom exception
            }

            return new Payment(stream!.Events);
        }

        public async Task<bool> SaveAsync(Payment payment)
        {
            if (payment.Changes.Any())
            {
                var streamId = $"{StreamIdPrefix}:{payment.PaymentId}";

                return await _eventStore.AppendToStreamAsync(
                streamId,
                payment.Version,
                payment.Changes);
            }

            return true;
        }
    }
}
```

[Payment.cs](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.Domain/Payment.cs) - This class `Payment` represents a payment entity in the domain model. It inherits from `AggregateBase` and defines properties and methods related to payment events.

In the example I have defined properties that could/would be useful for usage down stream such as `ThridPartyReference` (which is a typo I'll need to update 😄) and `PaymentCompletedTimestamp`.

```csharp
using Regis.Pay.Common.EventStore;
using Regis.Pay.Domain.Events;

namespace Regis.Pay.Domain
{
    public class Payment : AggregateBase
    {
        public Guid PaymentId { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public DateTime? PaymentCreatedTimestamp { get; private set; }
        public Guid? ThridPartyReference { get; private set; }
        public DateTime? PaymentCompletedTimestamp { get; private set; }

        public Payment(IEnumerable<IDomainEvent> events) : base(events)
        {
        }

        public Payment(Guid paymentId, decimal amount, string currency) : base()
        {
            Apply(new PaymentInitiated()
            {
                PaymentId = paymentId,
                Amount = amount,
                Currency = currency
            });
        }

        public void Created()
        {
            Apply(new PaymentCreated());
        }

        public void Settled(Guid paymentReference)
        {
            Apply(new PaymentSettled() { PaymentReference = paymentReference });
        }

        public void Complete()
        {
            Apply(new PaymentCompleted());
        }

        public void When(PaymentInitiated @event)
        {
            PaymentId = @event.PaymentId;
            Amount = @event.Amount;
            Currency = @event.Currency;
        }

        public void When(PaymentCreated @event)
        {
            PaymentCreatedTimestamp = @event.Timestamp;
        }

        public void When(PaymentSettled @event) 
        {
            ThridPartyReference = @event.PaymentReference;
        }

        public void When(PaymentCompleted @event)
        {
            PaymentCompletedTimestamp = @event.Timestamp;
        }
    }
}
```

[AggregateBase.cs](https://github.com/reggieray/regis-pay/blob/main/src/Regis.Pay.Domain/AggregateBase.cs) - This abstract class `AggregateBase` serves as a base class for aggregate roots in the domain model. It provides functionality for managing domain events and versioning.

```csharp
using Regis.Pay.Common.EventStore;

namespace Regis.Pay.Domain
{
    public abstract class AggregateBase
    {
        public int Version { get; private set; }

        public List<IDomainEvent> Changes { get; }

        protected AggregateBase() 
        {
            Changes = new List<IDomainEvent>();
        }   

        protected AggregateBase(IEnumerable<IDomainEvent> events)
        {
            Changes = new List<IDomainEvent>();

            foreach (var @event in events)
            {
                Mutate(@event);
                Version += 1;
            }
        }

        protected void Apply(IDomainEvent @event)
        {
            Changes.Add(@event);
            Mutate(@event);
        }

        private void Mutate(IDomainEvent @event)
        {
            ((dynamic)this).When((dynamic)@event);
        }
    }
}
```

## Data

Here I'll go over what the above code produces when data is persisted to the event store in Cosmos DB. There are four domain events defined in the Regis Pay example which is represented by this diagram: 

> <img src="/posts/images/domain-events.drawio.png" style="max-width: 100%">


When each domain event is persisted to the event store they would look like the following:

1. `PaymentInitiated`

```json
{
    "id": "pay:7551548a-b366-4081-9330-ae19bfc5557f:1",
    "stream": {
        "id": "pay:7551548a-b366-4081-9330-ae19bfc5557f",
        "version": 1
    },
    "eventType": "PaymentInitiated",
    "eventData": {
        "PaymentId": "7551548a-b366-4081-9330-ae19bfc5557f",
        "Amount": 86.1,
        "Currency": "GBP",
        "Timestamp": "2024-04-06T00:02:14.9984673Z"
    },
    "_rid": "J+MUALnYydYmAAAAAAAAAA==",
    "_self": "dbs/J+MUAA==/colls/J+MUALnYydY=/docs/J+MUALnYydYmAAAAAAAAAA==/",
    "_etag": "\"00000000-0000-0000-87b5-aedb6add01da\"",
    "_attachments": "attachments/",
    "_ts": 1712361735
}
```

2. `PaymentCreated`

```json
{
    "id": "pay:7551548a-b366-4081-9330-ae19bfc5557f:2",
    "stream": {
        "id": "pay:7551548a-b366-4081-9330-ae19bfc5557f",
        "version": 2
    },
    "eventType": "PaymentCreated",
    "eventData": {
        "Timestamp": "2024-04-06T00:02:20.2212849Z"
    },
    "_rid": "J+MUALnYydYnAAAAAAAAAA==",
    "_self": "dbs/J+MUAA==/colls/J+MUALnYydY=/docs/J+MUALnYydYnAAAAAAAAAA==/",
    "_etag": "\"00000000-0000-0000-87b5-b1ca6dc601da\"",
    "_attachments": "attachments/",
    "_ts": 1712361740
}
```

3. `PaymentSettled`

```json
{
    "id": "pay:7551548a-b366-4081-9330-ae19bfc5557f:3",
    "stream": {
        "id": "pay:7551548a-b366-4081-9330-ae19bfc5557f",
        "version": 3
    },
    "eventType": "PaymentSettled",
    "eventData": {
        "PaymentReference": "ff7fc400-783e-4070-8a91-bfb3b906729f",
        "Timestamp": "2024-04-06T00:02:24.9964504Z"
    },
    "_rid": "J+MUALnYydYoAAAAAAAAAA==",
    "_self": "dbs/J+MUAA==/colls/J+MUALnYydY=/docs/J+MUALnYydYoAAAAAAAAAA==/",
    "_etag": "\"00000000-0000-0000-87b5-b49f61f401da\"",
    "_attachments": "attachments/",
    "_ts": 1712361745
}
```

4. `PaymentCompleted`

```json
{
    "id": "pay:7551548a-b366-4081-9330-ae19bfc5557f:4",
    "stream": {
        "id": "pay:7551548a-b366-4081-9330-ae19bfc5557f",
        "version": 4
    },
    "eventType": "PaymentCompleted",
    "eventData": {
        "Timestamp": "2024-04-06T00:02:30.0929079Z"
    },
    "_rid": "J+MUALnYydYpAAAAAAAAAA==",
    "_self": "dbs/J+MUAA==/colls/J+MUALnYydY=/docs/J+MUALnYydYpAAAAAAAAAA==/",
    "_etag": "\"00000000-0000-0000-87b5-b7a8c20f01da\"",
    "_attachments": "attachments/",
    "_ts": 1712361750
}
```

When querying data from Cosmos DB, you could query using the stream id. In the example above it would be something like:

```sql
SELECT * FROM c
WHERE c.stream.id = "pay:7551548a-b366-4081-9330-ae19bfc5557f"
```

I have included a screenshot of what it looks like when you query Cosmos DB. This is how I retrieved the persisted domain events in the examples above.  

> <img src="/posts/images/event-sourcing-cosmos-db.png" style="max-width: 100%">


## Demo

In this demo I put a demo payment through by sending a request to the API, notice in the logs for the change feed when it logs change feed changes with the domain event. This is the change feed working after each domain event is persisted to the event store.

> <img src="https://raw.githubusercontent.com/reggieray/regis-pay/main/docs/images/manual-test.gif" style="max-width: 100%">

This demo was all ran locally using Docker Desktop. If you want to test for yourself feel free to check out this [getting started](https://github.com/reggieray/regis-pay?tab=readme-ov-file#getting-started) guide.


# Summary

The Event Souring pattern is a great pattern to use IMO, but I caveat that with the **it depends** on your particular use case where **context is king 👑**. In my experience though, where I have used this pattern in the context of payments and orders in a event driven microservice environment it made perfect sense and solved a lot of business needs. That doesn't necessarily mean I would only recommend it in a event driven microservice environment, it's just an area I have seen this successfully adopted, having said that I can see this pattern also being useful in many contexts.

If you want to read more about this pattern I would recommend the [Microsoft documentation](https://learn.microsoft.com/en-us/azure/architecture/patterns/event-sourcing) and the [Event Sourcing](https://martinfowler.com/eaaDev/EventSourcing.html) blog post by Martin Fowler and all the code walked through in this blog post can be found [here](https://github.com/reggieray/regis-pay).