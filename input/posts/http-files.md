Title: HTTP Files
Published: 01/30/2024
Tags: 
- http files
- visual studio
- vscode
- dotnet
- dotnet 8

---

# Introduction 

If you've used the dotnet 8 templates for creating an API you might have noticed a new file type, a `.http` file. This blog post explores this new file and how we can use it.

## What is it?

`.http` files allow you to send HTTP requests with support directly from within visual studio, vscode, rider among others. If you've used [Postman](https://www.postman.com/) and similar applications this will be very familiar.

## Why use it?

After having mentioned Postman, you might be thinking why use it when I can just use Postman or similar applications. Here are a few pros I could think of:

- Multi IDE support - It's not just supported for Visual Studio, but also vscode & Jetbrains Rider to name a few.
- Non vendor lock-in - As mentioned in the point above, your no longer tied to using specific applications, this may come at the right time since Postman started requiring account creation to take advantage of most of it's advanced features.
- Code documentation - It can serve as documentation from within your code repository.
- Manual testing - It can also serve as testing tool for your APIs without having to open another application. 

## What does it look like?

Here is an example I created for demonstrating GET, POST, PUT & DELETE requests taken from this [example github repo](https://github.com/reggieray/http-file-examples/blob/main/http-file-examples.http) that I created on GitHub.

```http
@base_address = http://localhost:5295

 
GET {{base_address}}/todos/
Accept: application/json

###


GET {{base_address}}/todos/{{$guid}}
Authorization: Basic {{$dotenv Authorization}}
Accept: application/json

### 

POST {{base_address}}/todos/
Authorization: Basic {{$dotenv Authorization}}
Content-Type: application/json

{
    "id": "{{$guid}}",
    "title": "Todo Title ({{$timestamp}})",
    "isComplete": false
}

### 


@todo_id = {{$guid}}
PUT {{base_address}}/todos/{{todo_id}}
Authorization: Basic {{$dotenv Authorization}}
Content-Type: application/json

{
    "id": "{{todo_id}}",
    "title": "Todo Title ({{$timestamp}})",
    "isComplete": false
}

### 

DELETE {{base_address}}/todos/{{$guid}}
Authorization: Basic {{$dotenv Authorization}}
Accept: application/json
```

This HTTP file performs the following actions:

1. GET all todos (Unsecured): Retrieves a list of todos without authentication.
2. GET a specific todo by ID (Secured): Retrieves a specific todo by ID with basic authentication.
3. POST a new todo (Secured): Creates a new todo with basic authentication.
4. PUT (update) an existing todo (Secured): Updates an existing todo with basic authentication.
5. DELETE a todo by ID (Secured): Deletes a todo by ID with basic authentication.

# Code Example  

If you want to get started straight away you can get the source code [here](https://github.com/reggieray/http-file-examples) or can git clone

```ps
git clone https://github.com/reggieray/http-file-examples.git
```

## Understanding the API

In order to demonstrate features of `.http` files, I created a mock minimal API with a RESTful looking endpoint.  I added BasicAuthentication to the minimal API so the example could demonstrate setting auth headers. Basic authentication is implemented with a hardcoded username (admin) and password (admin). The API requires this authentication for endpoints marked with the [Authorize] attribute.


- `/todos` (GET): Retrieves a list of Todo items.
- `/todos/{id}` (GET): Retrieves a specific Todo item by ID.
- `/todos` (POST): Creates a new Todo item.
- `/todos/{id}` (PUT): Updates an existing Todo item.
- `/todos/{id}` (DELETE): Deletes a Todo item by ID.

## Running HTTP files

Below I'll demonstrate two ways of running the `.http` files.

It's worth mentioning that I added a `.env` file which can be used to get variables for the `.http` file. In the [Microsoft documentation](https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-8.0#env-files) it mentions using `$dotEnv`, I found this didn't work for me, but using `$dotenv` did.  It's also worth noting I committed this file in for demo purposes. I would advise not to commit any information that has sensitive information.

Below I have included some GIFs of the usage from each respective IDE.

### Visual Studio

For Visual Studio you'll need [Visual Studio 2022 version 17.8 or later](https://visualstudio.microsoft.com/vs/) with the ASP.NET and web development workload installed.

> <img src="/posts/images/http-file-example-vs.gif" style="max-width: 100%">


### VSCode

For VSCode you'll need the [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) extension.

> <img src="/posts/images/http-file-example-vs-code.gif" style="max-width: 100%">

# Useful links

- [Full source code](https://github.com/reggieray/http-file-examples).
- [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
- [Use .http files in Visual Studio 2022](https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-8.0)
