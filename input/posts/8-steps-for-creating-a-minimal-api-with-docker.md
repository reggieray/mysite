Title: 8 steps for creating a minimal API with docker
Published: 3/2/2023
Tags: 
- dotnet
- docker
- csharp

---

# Overview

If you want to get up and running quickly then you can find the source code to this [here](https://github.com/reggieray/simple-docker-dotnet-example) or cop this link and paste it in your browser https://github.com/reggieray/simple-docker-dotnet-example.

# Prerequisites:

Make sure you have the following prerequisites installed on your machine:

- Docker Desktop
- .NET Core SDK
- Code editor/IDE like Visual Studio Code or Visual Studio

# Steps

Step 1: Create a Minimal API

Create a new .NET 7.0 minimal API project in Visual Studio or via the command line using the following command:

```PowerShell
dotnet new web -n MyMinimalApi
```

This will create a new project called "MyMinimalApi" that includes the necessary files to create a minimal API.

Step 2: Write Code

Open the Program.cs file and add some code to define your minimal API endpoints. Here's an example of what your code might look like:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello, World!");

app.Run();
```

This code defines a single endpoint that responds to a GET request at the root URL ("/") and returns the string "Hello, World!".

Step 3: Test Your API

Run your application to ensure that it returns the expected output. To do this, click on "Debug" -> "Start Without Debugging" or press "Ctrl + F5".

Step 4: Create Dockerfile

Create a Dockerfile in the root directory of your project. Here's an example of what your Dockerfile might look like:

```
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "MyMinimalApi.dll"]
```

This Dockerfile is very similar to the one we created earlier for the console application, with a few differences. We're using the .NET 7.0 SDK image to build the application and the .NET 7.0 ASP.NET runtime image to run it. We're also copying the output from the build container to the runtime container.

Step 5: Build Docker Image

Now, build the Docker image by running the following command:

```PowerShell
docker build -t myminimalapi .
```

This will build a Docker image with the tag "myminimalapi".

Step 6: Run Docker Container

Finally, run the Docker container with the following command:

```PowerShell 
docker run -p 8080:80 myminimalapi
```

This will start the container and expose port 8080 on your machine. You can test the API by navigating to http://localhost:8080 in your web browser or using a tool like curl.

Step 7: Create Docker Compose file

Create a new file in the root directory of your project called "docker-compose.yml". Here's an example of what your Docker Compose file might look like:

```yaml
version: "3"
services: 
    myminimalapi:
        build: 
            context: ./MyMinimalApi
            dockerfile: Dockerfile
        ports: 
            - "8080:80"
```        

Step 8: Run Docker Compose

To build and run the Docker container using Docker Compose, open a terminal and navigate to the root directory of your project. Then, run the following command:

```PowerShell
docker-compose up
``` 

This will build the Docker image and start the container. You can test the API by navigating to http://localhost:8080 in your web browser or using a tool like curl.