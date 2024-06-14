Title: Keyed Services in .NET 8
Published: 06/14/2024
Tags: 
- dependency injection
- DI
- dotnet
- dotnet 8

---

# Keyed Services

Keyed services allow you to register multiple implementations of a service with a key and resolve the desired implementation at runtime based on that key. This enhances the flexibility and maintainability of your application.

From the documentation there are the three methods you can use to add a keyed service `AddKeyScoped`, `AddKeyedSingleton` & `AddKeyedTransient`.

Taking a closer look into the `AddKeyedSingleton` method signature ([source documentation link](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addkeyedsingleton?view=net-8.0)):

```csharp
public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddKeyedSingleton (this Microsoft.Extensions.DependencyInjection.IServiceCollection services, Type serviceType, object? serviceKey, Type implementationType);
```

Notice the `object? serviceKey` parameter, with this parameter we can use a `object` to add as a key. As a very simple example with other code setup omitted, it could look like the following for registration:

```csharp
builder.Services.AddKeyedSingleton<ISomeService, AService>("KeyA");
builder.Services.AddKeyedSingleton<ISomeService, BService>("KeyB");
```

and you can resolve a dependency like this:

```csharp
public class AServiceWrapper([FromKeyedServices("KeyA")] ISomeService service)
```

or you could use the `IServiceProvider` to resolve a keyed service like as follows:

```csharp
var serviceA = serviceProvider.GetRequiredKeyedService<ISomeService>("KeyA");
```

# Example Demo Code

For this section I created a example dotnet 8 webapi to show keyed service in action, you can find the source code: 

[HERE](https://github.com/reggieray/example-code/tree/main/dotnet-8-keyed-services) 
or using this link 
https://github.com/reggieray/example-code/tree/main/dotnet-8-keyed-services.

My goto example scenarios are payments and this is no exception. In this hypothetical API I want to expose the same endpoint so the contract does not change, but depending on the a parameter passed in the body it will create a payment with either Paypal or Klarna, two alternative payment methods aka APMs.

I have included entirety of the source code below, and the main difference from the example above is I'm using a `enum` as the service key, because as you might recall the key can be any object. Another key point to mention is I'm not using the attribute `FromKeyedServices` in this example I'll be using the `IServiceProvider`.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKeyedSingleton<IAlternativePaymentService, PayPalPaymentService>(AlternativePamynetMethodType.PAYPAL);
builder.Services.AddKeyedSingleton<IAlternativePaymentService, KlarnaPaymentService>(AlternativePamynetMethodType.KLARNA);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/create-payment", async (Payment payment, IServiceProvider serviceProvider) =>
{
    var apmService = serviceProvider.GetRequiredKeyedService<IAlternativePaymentService>(payment.Scheme);

    var paymentId = await apmService.Create(payment.Id);

    return paymentId;
})
.WithName("CreatePayment")
.WithOpenApi();

app.Run();

record Payment(Guid Id, AlternativePamynetMethodType Scheme);

enum AlternativePamynetMethodType 
{
    PAYPAL = 0,
    KLARNA = 1
}

interface IAlternativePaymentService 
{
    Task<Guid> Create(Guid Id);
}

class PayPalPaymentService(ILogger<PayPalPaymentService> logger) : IAlternativePaymentService
{

    public Task<Guid> Create(Guid Id)
    {
        logger.LogInformation("Created payment with PayPal for {paymentId}", Id);

        return Task.FromResult(Id);
    }
}

class KlarnaPaymentService(ILogger<KlarnaPaymentService> logger) : IAlternativePaymentService
{
    public Task<Guid> Create(Guid Id)
    {
        logger.LogInformation("Created payment with Klarna for {paymentId}", Id);

        return Task.FromResult(Id);
    }
}
```

The services are demo examples classes that just log which service was used. I'm a visual learner so I like to see things working, this is what it would look like when running the API.

> <img src="/posts/images/keyed-services-example.gif" style="max-width: 100%">

 On the left I have a `.http` file I'll be using to make requests to the API with either Paypal or Klarna and on the right is the logs of the running API.


# Summary

It's worth mentioning this isn't a new concept as other DI frameworks such as [Autofac](https://autofac.readthedocs.io/en/latest/) also have this feature, but it's nice to see we have this option now available baked into dotnet, offering us flexibility in how we want to structure our dependencies.