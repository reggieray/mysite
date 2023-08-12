Title: ASP.NET API string localization
Published: 2/20/2021
Tags: 
- dotnet
- csharp
- localization 
- aspnetcore
---
# Overview

In this blog post I'll briefly go over an example of adding localized strings to a ASP.NET application. In general, localization is the process in which we add support for applications for a specific region or market place. Localization is not only text but may also includes graphics, layout, currency, dates, phone numbers and addresses to name a few.

In my [example](https://github.com/reggieray/localization-example) I start off with a brand new ASP.NET web application boilerplate code project which creates a weather forecast API. If your not familiar with this boilerplate code all it contains is a `WeatherForecastController.cs` controller that returns example weather forecast data with summaries such as 'Cool' or 'Warm' upon a GET request. I take this boilerplate code further by adding localized strings of the weather forecast summaries for Spanish and Chinese because I want to add support to two of the most spoken languages around the world apart from English :). 

# Code

 I add `.resx` files to the project which is an XML resource (.resx) file that contains string, image, or object data. I add the following files: 

- `WeatherSummaries.resx` - The default strings, which will be in English
- `WeatherSummaries.es-ES.resx` - Spanish translations. Notice the addition of `es-ES` in the name.
- `WeatherSummaries.zh-CN.resx` - Chinese translations

Also make sure you that in the properties for `WeatherSummaries.resx` that 'Custom Tool' is set to 'ResXFileCodeGenerator'. This generates the code that will be used to get the translated strings. I have highlighted the areas on the screenshot below.

<img src="/posts/images/localization-example-project.PNG" style="max-width: 100%">
<br/>

To decide what translation to use I add a query parameter 'lang' to the GET request which is a string with a default of 'en-US', but will also supports 'es-ES' & 'zh-CN' through the use of the `.resx` files added earlier. 

To also handle invalid values being passed in for the 'lang' query parameter I created a class that returns a `CultureInfo` class. If a invalid value gets passed in then it should throw an `CultureNotFoundException` exception, which would then return a culture of 'en-US' as a fallback. 

It's also worth mentioning if a valid lang parameter gets passed through like 'en-GB' or 'it-IT' then it will return the default string found in the `WeatherSummaries.resx` file because no translations have been added to support those languages.

The main part that fetches the translation is the following. In the code of the controller it's all on one line. I have broken the code into two lines in the aim to make it a bit more easier to understand.

```csharp
var summary = Summaries[rng.Next(Summaries.Length)]; // example value could be 'Warm'
WeatherSummaries.ResourceManager.GetString(summary, cultureInfo)
```

The complete code for the `WeatherForecastController.cs` class file.

```csharp
using Localization.Example.Resources;
using Localization.Example.Weather;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Localization.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Gets the weather forecast.
        /// </summary>
        /// <param name="lang">The language. Support languages en-US, es-ES, zh-CN</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery] string lang = WeatherLocale.DefaultLocale)
        {
            var cultureInfo = WeatherLocale.GetLocale(lang);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = WeatherSummaries.ResourceManager.GetString(Summaries[rng.Next(Summaries.Length)], cultureInfo)
            })
            .ToArray();
        }
    }
}
```

This is the code that returns a `CultureInfo` class.


```csharp
using System.Globalization;

namespace Localization.Example.Weather
{
    public static class WeatherLocale
    {
        public const string DefaultLocale = "en-US";

        public static CultureInfo GetLocale(string lang)
        {
            try
            {
                return new CultureInfo(lang);
            }
            catch (CultureNotFoundException)
            {
                return new CultureInfo(DefaultLocale);
            }
        }
    }
}

```

And this is an example of what the default response from the API if you were to call it with the following `WeatherForecast?lang=en-US` 

```json
[
  {
    "date": "2021-02-21T10:26:57.9708277+00:00",
    "temperatureC": -1,
    "temperatureF": 31,
    "summary": "Cool"
  },
  {
    "date": "2021-02-22T10:26:57.9721746+00:00",
    "temperatureC": 41,
    "temperatureF": 105,
    "summary": "Cool"
  },
  {
    "date": "2021-02-23T10:26:57.9721824+00:00",
    "temperatureC": 20,
    "temperatureF": 67,
    "summary": "Bracing"
  },
  {
    "date": "2021-02-24T10:26:57.9721912+00:00",
    "temperatureC": 2,
    "temperatureF": 35,
    "summary": "Sweltering"
  },
  {
    "date": "2021-02-25T10:26:57.9721942+00:00",
    "temperatureC": 44,
    "temperatureF": 111,
    "summary": "Balmy"
  }
]
```

If you were to call it with `WeatherForecast?lang=es-ES` then you would get the following response.

```json
[
  {
    "date": "2021-02-21T10:26:57.9708277+00:00",
    "temperatureC": -1,
    "temperatureF": 31,
    "summary": "Fresco"
  },
  {
    "date": "2021-02-22T10:26:57.9721746+00:00",
    "temperatureC": 41,
    "temperatureF": 105,
    "summary": "Fresco"
  },
  {
    "date": "2021-02-23T10:26:57.9721824+00:00",
    "temperatureC": 20,
    "temperatureF": 67,
    "summary": "Vigorizante"
  },
  {
    "date": "2021-02-24T10:26:57.9721912+00:00",
    "temperatureC": 2,
    "temperatureF": 35,
    "summary": "Sofocante"
  },
  {
    "date": "2021-02-25T10:26:57.9721942+00:00",
    "temperatureC": 44,
    "temperatureF": 111,
    "summary": "Balsámico"
  }
]
```

If you wanted to try it out for yourself you can get the code from Github using the link below. If you run the project it will be configured to run Swagger where you can call the API and play with it yourself like the screenshot below. 

<img src="/posts/images/localization-example-swagger.PNG" style="max-width: 100%">
<br/>

# Links

- [Code on Github](https://github.com/reggieray/localization-example)
- [Docs on Resources in .NET apps](https://docs.microsoft.com/en-us/dotnet/framework/resources/)
- [Docs on Globalization and localization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-5)
- [Docs on Localization](https://docs.microsoft.com/en-us/dotnet/standard/globalization-localization/localization)
- [Docs on Globalization](https://docs.microsoft.com/en-us/dotnet/standard/globalization-localization/globalization)