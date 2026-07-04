Title: Migrating My Blog from Wyam to Statiq under .NET 8 (AI-Powered)
Published: 07/04/2026
Lead: A deep dive into migrating my personal developer blog from the legacy Wyam generator to Statiq Web with custom search, light/dark themes, and automated GitHub Actions deployment—fully driven by agentic AI.
Tags:
- dotnet
- statiq
- wyam
- web development
- static site generator
- AI
- gemini
- ai generated
---

# Intro

For years, my blog was powered by **Wyam**, a fantastic .NET static site generator. However, technology moves fast, and Wyam became legacy, bound to older .NET Framework runtimes and dependencies. 

Recently, I decided to upgrade my blog's engine to **[Statiq Web](https://statiq.dev/web/)**, the modern, performance-oriented successor to Wyam built on top of **.NET 8**. Along the way, I didn't just migrate files—I gave the site a complete visual, functional, and developer-experience facelift.

The twist? **This entire migration was performed in partnership with agentic AI coding assistants.**

---

## 🤖 Behind the Scenes: AI-Driven Pair Programming

Instead of manually modifying 300+ HTML/Markdown files and fighting compiler warnings for days, I paired with AI coding assistants to handle the bulk of the refactoring work. 

My workflow was split across two powerful models:
1.  **Initial Migration with Claude Opus**: I started the heavy lifting using the Claude Opus model. It was instrumental in analyzing the legacy project structure, suggesting the initial porting strategy, and mapping the baseline files.
2.  **Finetuning & Feature Engineering with Gemini 3.5 Flash**: Once the foundation was laid, I transitioned to Gemini 3.5 Flash. I used Gemini to chip away at the remaining issues and push the migration over the finish line. Gemini was exceptionally capable at resolving template compilation errors, handling CSS flex layouts, and writing the custom JavaScript features (such as search, themes, and code copy buttons).

Here is a breakdown of what the AI agents helped me build:
*   **Codebase Discovery**: The AI scanned my repository's directory layout, located the Wyam settings, and wrote the initial modern `.NET 8` console project structure (`MySite.csproj` and `Program.cs`) from scratch.
*   **Template Debugging**: Porting legacy Razor views can be tricky due to namespace changes. The Gemini model parsed my compilation errors, resolved recursive nested layout loops in `_Layout.cshtml`, and mapped page metadata variables correctly.
*   **Styling & Design**: I wanted a premium light theme to contrast my futuristic dark theme. The AI generated a custom glassmorphism layout with indigo-lavender gradient overrides and set up transition animations that switch instantly with `localStorage` memory.
*   **Functional Engineering**: The agent wrote the real-time search engine with autocomplete suggestion dropdowns, keyboard navigation (`ArrowUp`/`ArrowDown`/`Enter`/`Escape`), and custom `<mark>` keyword query highlighting.
*   **DevOps & Automation**: The AI cleaned up my repository, deleted the old Azure DevOps files, set up **GitHub Actions**, and committed/pushed the finalized changes directly to Git.

---

## 🛠️ Why Migrate to Statiq?

Wyam served me well, but it had several issues:
1.  **Outdated Runtime**: Wyam runs on older .NET releases, making it hard to compile on modern systems without backward-compatibility SDKs.
2.  **Slower Compilation**: Building 300+ pages took significant time due to legacy templating pipelines.
3.  **Lack of Active Development**: Statiq is now the officially maintained framework, bringing modern .NET performance features (like async-first processing and Span-based parsing).

Statiq Web compiles pages in a fraction of the time, offers C# console-first configuration, and supports modern Razor templating out-of-the-box.

---

## 📋 Step 1: Initializing the Statiq Project

Unlike Wyam, which relies on a global CLI tool, Statiq sites are compiled using a standard .NET Console Application. 

We created a new project and added the `Statiq.Web` NuGet package:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <OutputType>Exe</OutputType>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Statiq.Web" Version="1.0.0-beta.60" />
  </ItemGroup>
</Project>
```

Then, we defined the entry point in `Program.cs`:

```csharp
using Statiq.App;
using Statiq.Web;

return await Bootstrapper
    .Factory
    .CreateWeb(args)
    .AddSetting(Keys.Host, "matthewregis.dev")
    .AddSetting(Keys.LinksUseHttps, true)
    .AddSetting(WebKeys.SiteTitle, "Matthew Regis")
    .AddSetting(WebKeys.GenerateSitemap, true)
    .AddSetting(WebKeys.GenerateSearchIndex, true)
    .AddSetting("ZipSearchResultsFile", false)
    .AddSetting("AdditionalSearchResultFields", new[] { "tags" })
    .RunAsync();
```

---

## 🎨 Step 2: Creating a Premium Light & Dark Theme

The blog's theme is based on the HTML5 UP **SolidState** theme—a futuristic dark layout. To improve accessibility and user choice, we implemented a custom theme switcher supporting a brand-new, premium light mode.

### Instant Theme Loading (Preventing Flashing)
To avoid the annoying "dark-mode flash" on loading the light theme, the layout utilizes an inline script in `<head>` that applies the theme class before the browser paints the body:

```html
<script>
    (function() {
        var theme = localStorage.getItem('theme') || 'dark';
        if (theme === 'light') {
            document.documentElement.classList.add('light-theme');
            window.addEventListener('DOMContentLoaded', function() {
                document.body.classList.add('light-theme');
            });
        }
    })();
</script>
```

### The Light Theme Styling
The light theme features:
*   A premium blue-grey slate background (`#f8fafc`).
*   A frosted-glass header (`backdrop-filter: blur(8px)`) with fine slate dividers.
*   A beautiful, high-end greeting gradient (`linear-gradient(135deg, #e0e7ff 0%, #fae8ff 100%)`).
*   Soft-bordered card structures with micro-shadows.

### Transition Guard
To prevent page elements from animating their colors on initial load (which looks jittery), we added a `.no-transition` class to `<body>` on load, removing it 100ms later via JS:
```css
body.no-transition, body.no-transition * {
  transition: none !important;
}
```

---

## 🔍 Step 3: Real-Time Search & Autocomplete

Adding a client-side search indexing system is easy with Statiq's `SearchIndex` pipeline. 

By adding `"AdditionalSearchResultFields": ["tags"]` to `Program.cs`, Statiq compiles a `search.results.json` mapping that includes each post's URL, Title, and Tags list.

We built a custom real-time search interface in [search.cshtml](/search):
*   **Autocomplete Dropdown**: Lists up to 5 matching post suggestions as you type (searching title, link, and tags).
*   **Keyboard Navigation**: Full support for navigating matches using `ArrowUp`/`ArrowDown`, selecting with `Enter` to navigate to the post, or closing the overlay with `Escape`.
*   **Indigo Match Highlighting**: Uses a regex-based `<mark>` tag wrapper to highlight matched keywords in a soft indigo tint rather than default browser yellow.

---

## 📋 Step 4: Developer Quality-of-Life Enhancements

We also added two client-side scripts to improve code sharing and site speed:
1.  **Code Block Copy Buttons**: Every `<pre>` code block now displays a copy icon on hover. Clicking it utilizes the browser's clipboard API and flashes a green checkmark success state.
2.  **Native Lazy Image Loading**: The script scans the page and adds `loading="lazy"` to all post images dynamically, accelerating page load speeds.

---

## 📦 Step 5: Modernizing the CI/CD Pipeline

Previously, deployment was managed via an Azure DevOps pipeline zipping the output directory and pushing it via a curl request. 

We replaced this with a clean, modern **GitHub Actions Workflow** (`.github/workflows/deploy.yml`):

```yaml
name: Build and Deploy to Netlify
on:
  push:
    branches: [master]
  pull_request:

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'
      - name: Build Site
        run: dotnet run
      - name: Deploy to Netlify
        uses: nwtgck/actions-netlify@v3.0
        with:
          publish-dir: './output'
          production-branch: 'master'
          github-token: ${{ secrets.GITHUB_TOKEN }}
        env:
          NETLIFY_AUTH_TOKEN: ${{ secrets.NETLIFY_AUTH_TOKEN }}
          NETLIFY_SITE_ID: ${{ secrets.NETLIFY_SITE_ID }}
```

This setup compiles the Statiq project on a Linux runner and deploys it directly to Netlify. It even supports automatic **Deploy Previews** on Pull Requests!

---

## 🏁 Conclusion

Migrating to Statiq Web brought my developer blog into the modern era. The build pipeline is fast, the templates use the latest Razor engine features, the light theme provides a great alternative for readers, and code snippets are easy to copy. 

Doing it side-by-side with agentic AI coding tools showed how powerful collaborative development can be—starting the foundational structure with **Claude Opus**, and polishing and completing it with **Gemini 3.5 Flash**.
