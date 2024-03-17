Title: Creating a CLI tool with dotnet & Spectre.Console.Cli
Published: 03/17/2024
Tags: 
- dotnet
- Spectre.Console
- Spectre.Console.Cli

---

# Introduction 

When building CLI tools it can feel like there is a lot of ceremony in creating command line argument parsers and setting up your project just to get to the stage where you start coding your feature/business logic. This is where [Spectre.Console.Cli](https://spectreconsole.net/cli/getting-started) comes in. It's a very opinionated library for paring command line arguments and structuring your project using established industry conventions.

# How it works 

A quote taken straight from the documentation website:

> A Spectre.Console.Cli app will be comprised of Commands and a matching Setting specification. The settings file will be the model for the command parameters. Upon execution, Spectre.Console.Cli will parse the args[] passed into your application and match them to the appropriate settings file giving you a strongly typed object to work with.

# Showcasing with an example

Before I start going showcasing the usage of spectre console I'll give some context on the tool I intend to create. In this example I will be creating a CLI tool that will generate QR code from text input.

You maybe questioning how can a CLI tool output an image... the answer [`Spectre.Console.ImageSharp`](https://spectreconsole.net/widgets/canvas-image) which is also part of the Spectre.Console framework. To also aid with this example tool I'll be using nuget package [`Net.Codecrete.QrCodeGenerator`](https://www.nuget.org/packages/Net.Codecrete.QrCodeGenerator) to generate the QR code images.

In total I will be using the following packages:

- `Net.Codecrete.QrCodeGenerator`
- `Spectre.Console.Cli` 
- `Spectre.Console.ImageSharp`

## Code

To start off with we have the following classes:

1. `QRCodeCommand`
    - Extends the `Command<T>` class where `T` extends `CommandSettings`. This allows you to override the `Execute` method.
    - The `Execute` method is where you add you specific code logic.
    - The settings class where `T` extends `CommandSettings` gets passed into the `Execute` method allowing you to do whatever the input arguments.

2. `QRCodeCommandSettings`
    - Extends the `CommandSettings` class as mentioned above.
    - To define a argument (setting) you add it as a property as shown with `Text` in the code.
    - Attributes such as `CommandArgument` are used to define if a argument should be required or optional. Read [specifying settings](https://spectreconsole.net/cli/settings) for more information on attributes.

This is pretty much it, with minimal effort I was able to encapsulate my code in a method separating it from the concerns of parsing arguments (ie. settings).

Notice how I specified `Text` as `string[]`, this was to allow entering of text with spaces, I could of specified it as just a `string`, but I wanted to be more flexible and allow entering of text with spaces without having to specify quotations around the string. Where this comes unstuck though is if I wanted to add more settings as the spaces normally indicate a separate argument.

```csharp
using Net.Codecrete.QrCodeGenerator;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Dotnet.QRCode
{
    public class QRCodeCommandSettings : CommandSettings
    {
        [Description("Text to generate QR code for")]
        [CommandArgument(0, "<text>")]
        public required string[] Text { get; init; }
    }

    public class QRCodeCommand : Command<QRCodeCommandSettings>
    {
        public override int Execute(CommandContext context, QRCodeCommandSettings settings)
        {
            var qr = QrCode.EncodeText(string.Join(" ", settings.Text), QrCode.Ecc.Medium);
            var image = new CanvasImage(qr.ToBmpBitmap(1));
            AnsiConsole.Write(image);

            return 0;
        }
    }
}
```

Now that I've defined my command I'll need to setup the `Program.cs` to load the command and execute it as expected. As you probably may already know `Main` method is where the entry point to a console application, so this is where to set it up. 

Notable setup methods:

- `AddExample`: Adds a example of how to use the application. Also gets displayed on the help command. 
- `PropagateExceptions`: Will re-throw exceptions, useful for investigating errors.
- `ValidateExamples`: Runs the examples set to validate them.

```csharp
using Dotnet.QRCode;
using Spectre.Console;
using Spectre.Console.Cli;

public class Program
{
    public static int Main(string[] args)
    {
        var app = new CommandApp<QRCodeCommand>();
        app.Configure(config =>
        {
            config.SetApplicationName("dotnet-qrcode");

            config.AddExample("Hello, World!");

#if DEBUG
            config.PropagateExceptions();
            config.ValidateExamples();
#endif
        });

        try
        {
            return app.Run(args);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            return -1;
        }
    }
}

```

And that is pretty much the setup and out of the box and you get a nice `--help` command which outputs how your CLI can be used with all the examples provided in the `Program.cs` of which would look like the following output for this example.

```powershell
USAGE:
    dotnet-qrcode <text> [OPTIONS]                                                                                                                             
    
EXAMPLES:
    dotnet-qrcode Hello, World!

ARGUMENTS:
    <text>    Text to generate QR code for

OPTIONS:
    -h, --help       Prints help information
    -v, --version    Prints version information
```

## Demo

All that being said sometimes it's just easier to show it in action:

> <img src="https://github.com/reggieray/dotnet-qrcode/raw/main/demo.gif" style="max-width: 100%">

## Testing

A testing package [`Spectre.Console.Testing`](https://www.nuget.org/packages/Spectre.Console.Testing/) is also made available to assist with testing the commands. This package is used in the tests for the `Spectre.Console` package itself. More infomation can be found on [best practices](https://spectreconsole.net/best-practices) documentation.

Here I've setup a test that runs multiple arguments against the command and the expectation is that it should handle it as opposed to not recognizing the extra arguments resulting in a error being thrown.

```csharp
using FluentAssertions;
using Spectre.Console.Testing;

namespace Dotnet.QRCode.Tests
{
    public class QRCodeAppTests
    {
        [Theory]
        [MemberData(nameof(SingleAndMultipleArgumentsTestData))]
        public async void Should_Handle_SingleAndMultipleArguments(string[] args)
        {
            //Arrange
            var app = new CommandAppTester();
            app.SetDefaultCommand<QRCodeCommand>();

            //Act
            var result = await app.RunAsync(args);

            //Assert
            result.ExitCode.Should().Be(0);
            result.Settings.Should().BeOfType<QRCodeCommandSettings>()
                .Which.Text.Should().BeEquivalentTo(args);
        }

        public static IEnumerable<object[]> SingleAndMultipleArgumentsTestData =>
        new List<object[]>
        {
            new object[] { new string[] { "Hello, World!" } },
            new object[] { new string[] { "Hello,", "World!" } },
            new object[] { new string[] { "Hello", ",", "World!" } },
            new object[] { new string[] { "Hello", ",", "World", "!" } }
        };
    }
}
```

# Summary

As it says on the tin [Spectre.Console.Cli](https://spectreconsole.net/cli/getting-started) is very opinionated library, so might not satisfy your personal preferences, but what you get in return is a framework that gets you started with a robust CLI argument parsing with minimal effort. Allowing you to focus on the important stuff which is implementing your code logic. I personally like Command approach because it reminds me [CQRS pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs) which I have extensively used in the last few years, making this framework easy to adopt.

If your interested in the example code used in this blog, you can find it here [https://github.com/reggieray/dotnet-qrcode](https://github.com/reggieray/dotnet-qrcode).
