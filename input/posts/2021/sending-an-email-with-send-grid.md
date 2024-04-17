Title: Sending an email with Send Grid
Published: 2/15/2021
Tags: 
- dotnet
- csharp
- sendgrid
---
# Overview

There are a few ways you can send emails these days in C#/.NET. In this blog post I'll quickly go over sending an email using SendGrid. I'll just be covering the basics to show you how easy it is to get you going, but bear in mind there are alot of features that you can use which you should check using the SendGrid website.

# What is SendGrid?

In their own words:

> SendGrid is a cloud-based SMTP provider that allows you to send email without having to maintain email servers. SendGrid manages all of the technical details, from scaling the infrastructure to ISP outreach and reputation monitoring to whitelist
services and real time analytics.

# Prerequisites

There are two options available to integrate with SendGrid `Web API` (Recommended) and `SMTP Relay`.

In this example I will just be covering the web API using SendGrid's C# library, but before the code you'll need the following:

- A SendGrid account - You can sign up for free and at time of writing you can send 100 emails/day for free.
- A API key - Created within the [SendGrid](https://app.sendgrid.com/)'s portal. Make sure you give it access to "Mail Send".
- Verify an email - In order to send an email you must first verify you have access to said email address.

# Using SendGrid

Start by installing the SendGrid NuGet package:

```Powershell
Install-Package SendGrid
```

And the bit of C# code to send the email:

```csharp
var sendGridClient = new SendGridClient("API_KEY");
var from = new EmailAddress("email from", "name");
var subject = "subject";
var to = new EmailAddress("email to", "name");
var plainContent = "Hello World";
var htmlContent = "<h1>Hello World!</h1>";
var mailMessage = MailHelper.CreateSingleEmail(from, to, subject, plainContent, htmlContent);
await sendGridClient.SendEmailAsync(mailMessage);
```

And that's it. To find out more of all the available options you have with SendGrid you should checkout this [github](https://github.com/sendgrid/sendgrid-csharp) repository. 