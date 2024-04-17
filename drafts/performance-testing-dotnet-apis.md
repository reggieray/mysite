Title: Intro to performance testing
Published: 12/12/2022
Tags: 
- dotnet
- performance testing
- dotnet 6
- csharp

---
# Introduction

Performance testing is an essential part of ensuring the quality and reliability of a API. It involves evaluating the speed, scalability, and stability of the API under various conditions and workloads. In this blog post, I'll give a introduction to performance testing, different types of performance testing and some tools that you can use to perform with dotnet API's in mind.

One of the key considerations in performance testing is choosing the right metrics to measure. These metrics should be relevant to the specific goals of the API, such as response time, throughput, or resource utilization. For example, if the API is used for real-time communication, response time may be the most important metric, while for an e-commerce API, throughput and scalability may be more relevant.

Another important aspect of performance testing is the test environment. The environment should be as similar as possible to the production environment, including the hardware, operating system, and network conditions. This will help ensure that the results of the tests are accurate and representative of the real-world performance of the API. That said not all performance tests have to be close to production environments, as environments with a similar setup but with lesser resources can also give you vital information about your API. Ideally you can have both. This kind of fits into [shift left testing](https://learn.microsoft.com/en-us/devops/develop/shift-left-make-testing-fast-reliable#shift-left-to-test-earlier) and [shift right testing](https://learn.microsoft.com/en-us/devops/deliver/shift-right-test-production) which I won't get into now, but it's worth mention it's not one choice over another. Pick what fits your purpose, sometimes bits from both would be applicable.

Once the metrics and test environment have been determined, the next step is to design the test cases. These test cases should cover a range of scenarios and workloads, from normal usage to extreme conditions. This will help identify any bottlenecks or weaknesses in the API and allow for optimization and improvement.

One of the challenges of performance testing is generating realistic workloads. This can be difficult to do manually, so it is often necessary to use automated tools and services. These tools can generate large numbers of concurrent requests and simulate realistic user behavior, such as making multiple API calls in parallel. Before I mention some tools it probably a good idea to go over the various different types of performance testing.

## Performance test types

- Load testing, which involves subjecting the system to normal and peak usage conditions to determine its behavior and performance under different levels of concurrency
- Stress testing, which involves subjecting the system to extreme workloads and conditions to determine its limits and breaking point
- Scalability testing, which involves increasing the workload on the system to determine its ability to scale up and handle larger volumes of data and users
- Volume testing, which involves testing the system with large volumes of data to determine its performance and behavior under such conditions
- Spike testing, which involves sudden and significant increases in the workload on the system to test its response and recovery capabilities
- Soak testing is a type of performance testing that is used to evaluate how a system performs under a sustained heavy load. It is designed to identify any potential problems that may only become apparent after the system has been running for a while, such as memory leaks or other performance issues.


## NBomber

[NBomber](https://nbomber.com/) is a load and performance testing tool for .NET that allows developers to test the performance and scalability of their applications. It is designed to make it easy to write and run performance tests by providing a simple, intuitive API and a set of powerful features. Some of the key features of NBomber include support for distributed testing, real-time monitoring and reporting, customizable test scenarios, and support for a wide range of protocols and data formats. It is open-source and available on GitHub.

```csharp
using Nbomber;
using Nbomber.Plugins.Http;
using System.Threading.Tasks;

namespace MyLoadTests
{
    public class MyTest
    {
        [LoadTest]
        public async Task MyLoadTest()
        {
            // create a new Nbomber context
            var context = NbomberContext.Create();

            // define the target URL
            var targetUrl = "http://www.example.com";

            // define a scenario with a single step
            var scenario = new Scenario()
                .WithStep(new HttpRequestStep("my step", targetUrl));

            // define the load test with a duration of 10 seconds and 100 concurrent users
            var loadTest = new LoadTest(
                "my load test",
                scenario,
                duration: 10,
                concurrency: 100);

            // run the load test
            await context.RunAsync(loadTest);
        }
    }
}

```

In this code, we use nbomber to create a load test that sends HTTP requests to the specified target URL. The load test runs for 10 seconds and simulates 100 concurrent users. You can customize this code to suit your specific testing needs, such as changing the duration, concurrency, or target URL.

## Apache JMeter

[Apache JMeter](https://jmeter.apache.org/) is an open-source, Java-based tool for load and performance testing. It is used to test the performance of web applications, web services, and other applications that run over HTTP or other protocols. JMeter is designed to simulate a large number of users accessing an application simultaneously, and can be used to measure the response time, throughput, and other performance metrics of the application under various levels of load. It is highly customizable and can be extended through the use of plugins and other extensions.