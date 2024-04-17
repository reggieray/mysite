Title: Efficent api calls within an api client
Published: 8/6/2019
Tags: 
- dotnet
- csharp
---
# Introduction

This blog post is built off another blog post found [Here](https://johnthiriet.com/efficient-api-calls/#). In that post it goes over the steps of making efficent api calls. I won't repeat what was written, please read the orignal source to get a bit of background. 

In this post I am going to take what was learnt from the blog post linked above and use it within an ApiClient class where I can resue a generic get call. In the example ApiClient is a static class, but if your using DI in your code then you can easily refactor to make it injectable. 

If you want to play with a working demo you can find the source code of this example on Github [Here](https://github.com/reggieray/ExampleApiClient). Example usage is within a .Net core (2.2) console app. 

# Code 

The ApiClient code.

```csharp
public static class ApiClient
{
    /// <summary>
    /// Gets the async.
    /// </summary>
    /// <returns>The async.</returns>
    /// <param name="url">URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken)
    {
        using (var request = new HttpRequestMessage(HttpMethod.Get, url))
        using (var client = new HttpClient())
        using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
        {
            var stream = await response.Content.ReadAsStreamAsync();

            if (response.IsSuccessStatusCode)
                return DeserializeJsonFromStream<T>(stream);

            var content = await StreamToStringAsync(stream);
            throw new ApiException { StatusCode = (int)response.StatusCode, Content = content };
        }
    }
    /// <summary>
    /// Deserializes the json from stream.
    /// </summary>
    /// <returns>The json from stream.</returns>
    /// <param name="stream">Stream.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    private static T DeserializeJsonFromStream<T>(Stream stream)
    {
        if (stream == null || stream.CanRead == false)
            return default(T);

        using (var sr = new StreamReader(stream))
        using (var jtr = new JsonTextReader(sr))
        {
            var jr = new JsonSerializer();
            var searchResult = jr.Deserialize<T>(jtr);
            return searchResult;
        }
    }
    /// <summary>
    /// Streams to string async.
    /// </summary>
    /// <returns>The to string async.</returns>
    /// <param name="stream">Stream.</param>
    private static async Task<string> StreamToStringAsync(Stream stream)
    {
        string content = null;
        if (stream != null)
        {
            using (var sr = new StreamReader(stream))
            {
                content = await sr.ReadToEndAsync();
            }
        }
        return content;
    }
}
```

To use use ApiClient, you would do as follows:

```csharp
try
{
    using (var cancelSrc = new CancellationTokenSource())
    {
        var response = await ApiClient.GetAsync<Todo>($"https://jsonplaceholder.typicode.com/todos/3", cancelSrc.Token);
        Console.WriteLine($"response: {response}");
    }
}
catch (ApiException apiEx)
{
    Console.WriteLine(apiEx.StackTrace);
}
catch (Exception ex)
{
    Console.WriteLine(ex.StackTrace);
}
finally
{
    Console.ReadLine();
}

```

Other classes used in the previous code.

```csharp
public class ApiException : Exception
{
    /// <summary>
    /// Gets or sets the status code.
    /// </summary>
    /// <value>The status code.</value>
    public int StatusCode { get; set; }
    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    /// <value>The content.</value>
    public string Content { get; set; }
}

public class Todo
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    /// <value>The user identifier.</value>
    [JsonProperty("userId")]
    public int UserId { get; set; }
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    [JsonProperty("id")]
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>The title.</value>
    [JsonProperty("title")]
    public string Title { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Example.ApiClient.Todo"/> is completed.
    /// </summary>
    /// <value><c>true</c> if completed; otherwise, <c>false</c>.</value>
    [JsonProperty("completed")]
    public bool Completed { get; set; }

    public override string ToString()
    {
        return $"\nuserId:{UserId}\nid:{Id}\ntitle:{Title}\ncompleted:{Completed}";
    }
}
```

# Summary

The main bulk of the code is within ApiClient. By using a [Generic class](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-classes) I can now pass a type within ```<T>```. This means I don't have to repeat the api calling code, I can just create my class that maps to the Json object I expect and voila.