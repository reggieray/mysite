Title: Selenium web driver + dotnet 6 example
Published: 5/1/2022
Tags: 
- dotnet
- selenium
- dotnet 6
- nunit
- csharp

---

There are times that I need to look up how to implement form interaction with selenium web driver for dotnet, so I decided to put a simple example for myself to look up as a basic reference. The following selenium ui test enters information into a simple contact form and clicks submit and then confirms the URL is correct. In order to run this you have to run [this site](https://github.com/reggieray/mysite) locally. The source code for this is available [here](https://github.com/reggieray/Selenium.Example).

```csharp
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace Selenium.Example
{
    public class MySiteTests
    {
        private const string Url = "http://localhost:5080/";
        private const string HamBurgerMenuXPath = @"//*[@id='header']/nav/a";
        private const string ContactMenuXPath = @"//*[@id='menu']/div/ul/li[2]/a";
        private const string ContactNameXPath = @"//*[@id='wrapper']/section/div[1]/div/form/div/div[1]/input";
        private const string ContactEmailXPath = @"//*[@id='wrapper']/section/div[1]/div/form/div/div[2]/input";
        private const string ContactMessageXPath = @"//*[@id='wrapper']/section/div[1]/div/form/div/div[3]/textarea";
        private const string ContactSubmitXPath = @"//*[@id='wrapper']/section/div[1]/div/form/ul/li/button";

        [Test]
        public void ContactForm()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            using var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Url);
            driver.ClickByXPath(wait, HamBurgerMenuXPath);
            driver.ClickByXPath(wait, ContactMenuXPath);
            driver.EnterInputByXPath(wait, ContactNameXPath, "Matt");
            driver.EnterInputByXPath(wait, ContactEmailXPath, "matt@example.com");
            driver.EnterInputByXPath(wait, ContactMessageXPath, "Hey, is this working?");
            driver.ClickByXPath(wait, ContactSubmitXPath);

            driver.Url.Should().Be($"{Url}contact/success");
        }
    }

    public static class MySiteTestsExtensions
    {
        public static void ClickByXPath(this WebDriver driver, WebDriverWait wait, string xPath)
        {
            var element = driver.FindElement(By.XPath(xPath));
            wait.Until(x => x.FindElement(By.XPath(xPath)).Displayed);
            element.Click();
        }

        public static void EnterInputByXPath(this WebDriver driver, WebDriverWait wait, string xPath, string input)
        {
            var element = driver.FindElement(By.XPath(xPath));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            wait.Until(x => x.FindElement(By.XPath(xPath)).Displayed);
            element.SendKeys(input);
        }
    }
}
```