Title: Discriminated Unions With OneOf
Published: 06/17/2024
Tags: 
- oneof
- discriminated unions
- dotnet
- dotnet 8

---

# What is a discriminated unions?

Before I try and do a very poor job of explaining what a discriminated union is, I can point you to a documentation on it for F#. [Discriminated Unions](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions) documentation and a quote taken from said documentation.

> Discriminated unions provide support for values that can be one of a number of named cases, possibly each with different values and types. Discriminated unions are useful for heterogeneous data; data that can have special cases, including valid and error cases; data that varies in type from one instance to another; and as an alternative for small object hierarchies. In addition, recursive discriminated unions are used to represent tree data structures.

# C# support?

At time of writing this post there is no support for discriminated unions the same way there is for F#, but there has been some [discussion about implementing this feature](https://github.com/dotnet/csharplang/issues/7544) and there seems to be a growing demand for this feature to come in... so watch this space this might become a native C# feature.

That is not to say you can't implement discriminated unions in C# right now. You can do this with the use of the [OneOf](https://github.com/mcintyre321/OneOf) nuget package.

# Example Code

The entirety of the code used in this blog can be found [here](https://github.com/reggieray/example-code/tree/main/discriminated-unions-with-one-of) or using the following link: https://github.com/reggieray/example-code/tree/main/discriminated-unions-with-one-of.

In this project I setup a dotnet minimal api with two endpoints that been implemented with the same behavior, but achieve it in different ways. One with the use of OneOf and one without.

- `/weatherforecast`:
  - On Success: returns the same type of dependency service, which is a `IEnumerable<WeatherForecast>`.
  - On Failure: throws a `Exception` if a location is not supported and relies on setup of a exception handler `app.UseExceptionHandler` to map the appropriate bad request response. This returns a `ProblemDetails` response.
- `/weatherforecast-usingoneof`
  - On Success: on result of OneOf returns the `IEnumerable<WeatherForecast>` with a `Results.Ok`.  
  - On Failure: on result maps to `ProblemDetails` with `Results.BadRequest`.

For the example without OneOf I make use of a exception handler as this is a very common pattern I see in dotnet API's and I wanted to highlight the impact of using exceptions to handle expected bad responses.

The API endpoints with make use of the same service added using dependency injection. The method signature look like the following:

```csharp
interface IWeatherService 
{
    //👇🏽 - Throws a NotSupportedException on a unsupported location
    IEnumerable<WeatherForecast> GetWeatherForecast(string location);

    //👇🏽 - Returns a NotSupportedResult on a unsupported location
    OneOf<IEnumerable<WeatherForecast>, NotSupportedResult> GetWeatherForecastUsingOneOf(string location);    
}
```

If you are familiar with `Tuple` types, the syntax might look familiar, the only difference is that instead of a `Tuple` where you have to return all declared types, you can return only one of them.

This might start to click when you see the following code (ignore the typo 😄):

```csharp
public OneOf<IEnumerable<WeatherForecast>, NotSupportedResult> GetWeatherForecastUsingOneOf(string location)
{
    if (SupportedLocations.Any(x => x.Equals(location, StringComparison.InvariantCultureIgnoreCase)))
    {
        return GetWeatherForcast();
    }

    return new NotSupportedResult($"{location} is not a supported location!");
}
```

And in the result of the returning code you can use OneOf's `Match` method to map the appropriate response, like as follows:

```csharp
var result = weatherService.GetWeatherForecastUsingOneOf(location);

return result.Match(
    forecast => Results.Ok(forecast),
    notSupported => Results.BadRequest(new ProblemDetails { Title = notSupported.Message }));
```

If you want to see all the code, you can view the [Program.cs](https://github.com/reggieray/example-code/blob/main/discriminated-unions-with-one-of/DiscriminatedUnionsWithOneOf/Program.cs).

# Performance

## Benchmark

I have also added a benchmark project making use of [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) as I wanted to see the impact of using OneOf vs exception handling. I also make use make use of [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing) which is mainly used for integration testing, but I'm using it to spin up the API in memory to benchmark the full request flow.

The laptop I ran these benchmarks on is:

```
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3593/23H2/2023Update/SunValley3)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.106
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2
```

Which came up with the following results: 

| Method                       | Mean      | Error    | StdDev   | Min       | Max       | Median    | Gen0   | Gen1   | Allocated |
|----------------------------- |----------:|---------:|---------:|----------:|----------:|----------:|-------:|-------:|----------:|
| GetWeatherForecast           | 702.41 μs | 8.063 μs | 7.148 μs | 693.46 μs | 721.50 μs | 701.21 μs | 4.3945 | 0.9766 |  38.79 KB |
| GetWeatherForecastUsingOneOf |  34.91 μs | 1.260 μs | 3.714 μs |  26.83 μs |  41.74 μs |  34.92 μs | 1.4648 | 0.4883 |  13.14 KB |

If you are already familiar with how costly exceptions are then this should come at no surprise. By changing how the API handles certain expected errors you could gain a huge performance boost. 

## Load Test

Just for fun I also added a performance test to show how it translates to HTTP request performance and latency. I make use of [Nbomber](https://nbomber.com/) for load tests and I have setup the API to run in docker which is what the load tests will be configured to hit.

### Without OneOf

> <img src="/posts/images/http_without_discriminated_unions_scenario.png" style="max-width: 100%">

### With OneOf

> <img src="/posts/images/http_with_discriminated_unions_scenario.png" style="max-width: 100%">

# Summary

Discriminated unions is a in demand feature for dotnet and for good reasons, it lets you structure your code in a more intentional readable way and as a side effect vs error handling it gives you a massive performance boost allowing you serve more requests and consume less resources. I didn't explore it in this post, but it also makes it a bit more easier to test.

Hopefully discriminated unions finds it way into C# like F#, but for now that shouldn't let that stop you from using it as there is already a great option to use through the use of [OneOf](https://github.com/mcintyre321/OneOf).