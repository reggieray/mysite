using System.Globalization;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

return await Bootstrapper
    .Factory
    .CreateWeb(args)
    .AddSetting(Keys.Host, "matthewregis.dev")
    .AddSetting(Keys.LinksUseHttps, true)
    .AddSetting(WebKeys.SiteTitle, "Matthew Regis")
    .AddSetting(WebKeys.SiteDescription, "Hi 👋🏽, I'm Matthew Regis.")
    .AddSetting("Intro", "Thoughts 💭 from a software engineer 👨🏽💻, mostly tech and dotnet related, but not all.")
    .AddSetting("Image", "images/bg.jpg")
    .AddSetting(WebKeys.GenerateSitemap, true)
    .AddSetting(WebKeys.GenerateSearchIndex, true)
    .AddSetting("ZipSearchResultsFile", false)
    .AddSetting("AdditionalSearchResultFields", new[] { "tags" })
    .AddSetting(WebKeys.MirrorResources, false)
    .RunAsync();
