Title: Testcontainers with dotnet 🧪
Published: 09/21/2025
Tags: 
- testcontainers
- dotnet
- dotnet 9

---

The blog post explores [testcontainers](https://testcontainers.com/) (specifically for [dotnet](https://dotnet.testcontainers.org/)) and the advantages it gives us. Taking a common scenario using [EF core](https://learn.microsoft.com/en-us/ef/core/) with dotnet you’ve probably leaned on either the InMemory provider or SQLite in-memory mode for your tests. They’re quick, they integrate easily with the DbContext, and they keep your test suite blazing fast.

The problem? They don’t behave like SQL Server.
- EF Core InMemory ignores things like foreign key constraints and transaction semantics.
- SQLite is closer, but still has differences in data types, query translation, and behavior compared to MSSQL.

That’s where Testcontainers comes in. They let you run Docker containers directly from your tests. Instead of mocking a database or relying on an in-memory provider, you can spin up a real, disposable instance of a service (like SQL Server, PostgreSQL, RabbitMQ, or even a custom API) inside Docker, use it during the test, and then tear it down automatically.

## Quick start (using MsSql)

Add the package

```sh
dotnet add package Testcontainers.MsSql
```

create a instance and use in your tests.

```csharp
var container = new MsSqlBuilder().Build();
```

It's worth noting that the builder has much more options for you to instantiate a container, but a lot of the defaults should cover most use cases.

Taken from the example project below is usage with a fixture, the idea that you don't want to keep creating new containers per test, but create it once and share it with all tests that use it.

```csharp
public class SqlServerContainerFixture : IAsyncLifetime
    {
        private MsSqlContainer? _container { get; set; }
        public string ConnectionString { get; private set; } = string.Empty;

        public async Task InitializeAsync()
        {
            _container = new MsSqlBuilder().Build();

            await _container.StartAsync();

            ConnectionString = _container.GetConnectionString();

            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

            using var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            if (_container is not null)
            {
                await _container.StopAsync();
                await _container.DisposeAsync();
            }
        }
    }
```

# Example

## Getting Started 🚀

These instructions will get you a copy of the project (https://github.com/reggieray/test-containers.git) up and running on your local machine for development and testing purposes.

When running you should see the following, notice when running the test, docker spins up a test container and then spins it down when finished.

> <img src="https://github.com/reggieray/test-containers/raw/main/docs/test-containers-demo.gif" style="max-width: 100%">

### Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* [Docker](https://www.docker.com/products/docker-desktop) (for running locally and tests)

### Installation 

1. Clone the repo
   ```sh
   git clone https://github.com/reggieray/test-containers.git
   ```
2. Restore dependencies
   ```sh
   dotnet restore
   ```

## Usage 

Setup a local instance of SQL Server with docker using the following:

```sh
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

The application is configured to use the settings as shown above in the connection string in `appsettings.json`.

To run the application, execute the following command from the `src/Product.Api` directory:

```sh
dotnet run
```

The API will be available at [`http://localhost:5299/swagger`](http://localhost:5299/swagger).

## API Endpoints 📄

The following endpoints are available:

*   `GET /api/products`: Get all products
*   `GET /api/products/{id}`: Get a specific product by ID
*   `POST /api/products`: Create a new product
*   `PUT /api/products/{id}`: Update a product
*   `DELETE /api/products/{id}`: Delete a product

The `Product` model has the following structure:

```json
{
  "id": 0,
  "name": "string",
  "price": 0
}
```

## Running the Tests 🧪

The project includes tests that use [Testcontainers](https://testcontainers.com/) to spin up a real SQL Server database in a Docker container. Make sure to have Docker running when executing the test.

To run the tests, execute the following command from the root directory:

```sh
dotnet test
```

## CI/CD 🛠️

A GitHub Actions workflow is configured to build and test the project on every push and pull request to the `main` branch.

- No additional setup was need to get the test container to work
- See actions tab in Github for an example or if this run was not retained, you can fork/clone this repo and run it yourself. 
- If you're using another tool for your CI/CD pipeline, there is a good chance it's already supported.

## Technologies Used 🧑‍💻

*   [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0)
*   [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
*   [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
*   [Testcontainers for .NET](https://dotnet.testcontainers.org/)
*   [xUnit](https://xunit.net/)
*   [Fluent Assertions](https://fluentassertions.com/)