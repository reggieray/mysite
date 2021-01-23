Title: Refit examples - A type-safe HTTP client for .NET
Published: 15/1/2020
Tags: 
- dotnet
- csharp
---
# What is it?

[Refit](https://github.com/reactiveui/refit) is a type-safe HTTP client for .NET Core, Xamarin and .NET. Heavily inspired by Square's [Retrofit](https://square.github.io/retrofit/) library for Android and Java. It let's you define your API client using a interface and takes away the boilerplate of setting up a HTTP client calls.

# API Interface for Refit

Here's an interface with a few examples I created for a notes API. There's GET, POST, PUT and DELETE verbs, but Refit does support others. 

```csharp
public interface INoteApi
{
    [Get("/api/v1/notes")]
    Task<NotesResponse> GetNotes(NoteQueryParameters parameters = null);

    [Get("/api/v1/notes/{id}")]
    Task<NoteResponse> GetNoteById(string id);

    [Post("/api/v1/notes")]
    Task<string> Create([Body] CreateNoteRequest createNoteRequest);

    [Put("/api/v1/notes/{id}")]
    Task<HttpResponseMessage> Update(string id, [Body] UpdateNoteRequest updateNoteRequest);

    [Delete("/api/v1/notes/{id}")]
    Task<HttpResponseMessage> Delete(string id);
}
```

# Using the API Interface for Refit

Once you have the interface setup you can use it like below. Here I make calls to all the methods I added to the INoteApi interface.

```csharp
public static async Task Main(string[] args)
{
    //Creating a refrence to the API
    var api = RestService.For<INoteApi>("https://my-notes-api.com");

    //Calling the API using the refrence from the previous line 
    var notesResponse = await api.GetNotes();

    var noteId = await api.Create(new CreateNoteRequest { Title = "my title", Content = "my content" });

    var noteResponse = await api.GetNoteById(noteId);

    var updateResponse = await api.Update(noteId, new UpdateNoteRequest { Title = "updated title" });
    updateResponse.EnsureSuccessStatusCode();

    var deleteResponse = await api.Delete(noteId);
    deleteResponse.EnsureSuccessStatusCode();
}
```

One thing to mention though is the Create or Update methods demonstrate a happy path scenario, but what if the API responds with an error? 

It would throw an exception. One way to handle this could be dealt with a try and catch.

```csharp
public static async Task Main(string[] args)
{    
    var api = RestService.For<INoteApi>("https://my-notes-api.com");

    try
    {
        var noteId = await api.Create(new CreateNoteRequest { Title = "my title", Content = "my content" });
    }
    catch(Exception ex)
    {
        //Handle exception
    }
}
```

There is another way of handling errors without using a try and catch. To do this we can make use of the ```ApiResponse<T>``` class, which is generic wrapper class you can use with your return object. It not only returns your content as an object, but also includes meta data associated with the request/response.

To make use of ```ApiResponse<T>``` class I have to make a small update to the INoteApi interface. I've only updated the create method for brevity.

```csharp
//Updated interface
public interface INoteApi
{
    [Post("/api/v1/notes")]
    Task<ApiResponse<string>> Create([Body] CreateNoteRequest createNoteRequest);
}
//Usage of updated api
public static async Task Main(string[] args)
{    
    var api = RestService.For<INoteApi>("https://my-notes-api.com");

    var createApiResponse = await api.Create(new CreateNoteRequest { Title = "my title", Content = "my content" });

    if(createApiResponse.IsSuccessStatusCode)
    {
        //Do the thing 

        //Get the note id
        var noteId = createApiResponse.Content;
    }
}

```