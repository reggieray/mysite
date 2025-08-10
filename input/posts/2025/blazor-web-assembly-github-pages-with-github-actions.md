Title: 🚀 Deploying a Blazor WebAssembly App to GitHub Pages with GitHub Actions
Published: 08/10/2025
Tags: 
- dotnet
- dotnet 9
- blazor
- Blazor WASM

---

# Intro

If you’ve ever wanted to deploy a Blazor WebAssembly app to GitHub Pages, GitHub Actions makes the process straightforward and automated.

In this guide, I’ll walk you through creating a Blazor WebAssembly app, setting up the GitHub Action workflow, and triggering your deployment.

> 🌐 Live demo: https://reggieray.github.io/blazor-wasm/

# 🛠️ 1. Create the Blazor WebAssembly Application
First, create a new Blazor WebAssembly project:

```csharp
dotnet new blazorwasm -o Blazor.Wasm
```

Here, `Blazor.Wasm` is the name of your project folder.
Once it’s created, commit your changes and push the project to your GitHub repository.

# ⚙️ 2. Set Up the GitHub Action Workflow

We’ll now configure a GitHub Actions workflow to publish our app to GitHub Pages automatically.

🔗 Helpful references:

- Source code of my workflow: [static.yml](https://github.com/reggieray/blazor-wasm/blob/main/.github/workflows/static.yml)
- Original Microsoft sample: [blazor-samples static.yml](https://github.com/dotnet/blazor-samples/blob/main/.github/workflows/static.yml)

📄 Workflow file: `.github/workflows/static.yml`

```yaml
# Simple workflow for deploying static content to GitHub Pages
name: Deploy static content to Pages
env:
  PUBLISH_DIR: src/Blazor.Wasm/bin/Release/net9.0/publish/wwwroot # 👈 update project name

on:
  push:
    paths:
      - src/Blazor.Wasm/** # 👈 update project name
    branches:
      - main

  workflow_dispatch:

permissions:
  contents: read
  pages: write
  id-token: write

concurrency:
  group: "pages"
  cancel-in-progress: false

jobs:
  deploy:
    environment:
      name: github-pages
      url: ${{ steps.deployment.outputs.page_url }}
    runs-on: ubuntu-latest

    steps:
      - name: Checkout
        uses: actions/checkout@v4.2.2

      - name: Get latest .NET SDK
        uses: actions/setup-dotnet@v4.3.0
        with:
          dotnet-version: '9.0'

      - name: Publish app
        run: dotnet publish src/Blazor.Wasm -c Release # 👈 update project name

      - name: Rewrite base href
        uses: SteveSandersonMS/ghaction-rewrite-base-href@v1.1.0
        with:
          html_path: ${{ env.PUBLISH_DIR }}/index.html
          base_href: /blazor-wasm/ # 👈 update to your repo name
        
      - name: Setup Pages
        uses: actions/configure-pages@v5.0.0

      - name: Upload artifact
        uses: actions/upload-pages-artifact@v3.0.1
        with:
          path: ${{ env.PUBLISH_DIR }}

      - name: Deploy to GitHub Pages
        id: deployment
        uses: actions/deploy-pages@v4.0.5

```

# 📦 3. Trigger the Deployment

There are two ways to kick off your first deployment:

### 🔄 Option 1 — Push a Change to `main`

Make any change to your Blazor app and push it to the `main` branch. The workflow trigger looks like this:

```yaml
on:
  push:
    paths:
      - src/Blazor.Wasm/** # 👈 update project name
    branches:
      - main
```

### ▶️ Option 2 — Run It Manually

From your repository:

1. Go to the **Actions** tab.
1. Find the workflow named **Deploy static content to Pages**.
1. Click **Run workflow**.


# 🎯 Summary
✅ Now every push to your Blazor WebAssembly project’s main branch will automatically publish to GitHub Pages.

You get:
- No servers to manage
- Automatic builds
- Free hosting on GitHub Pages

🚀 Try it now and see your Blazor app live in minutes!

- Github repo: https://github.com/reggieray/blazor-wasm
- Demo link: https://reggieray.github.io/blazor-wasm/
