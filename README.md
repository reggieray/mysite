# Matthew Regis - Personal Tech Blog

This is the source code for my personal tech blog, migrated from Wyam to **[Statiq Web](https://statiq.dev/web/)** (powered by .NET 8). It features a modern, responsive user interface with premium dark and light theme options, clickable tag groupings, and instant client-side search with autocomplete.

---

## 🚀 Features

*   **Static Site Generator**: Migrated to Statiq Web for fast compilation and .NET 8 modern library support.
*   **Premium Theme Switcher**: 
    *   Futuristic dark theme (original design).
    *   Sophisticated light theme with glassmorphic navbar, custom indigo-lavender gradients, and high-readability slate typography.
    *   Persistent preferences stored using `localStorage`.
*   **Real-time Search & Autocomplete**:
    *   Pre-indexed search on page titles, links, and tags.
    *   Dropdown suggestions panel on typing.
    *   Full keyboard accessibility (use `ArrowUp`, `ArrowDown`, `Enter` to navigate suggestions, and `Escape` to close).
*   **Interactive Tag Grouping**: Filter posts by tag categories directly from the home page or specific tag views.
*   **CI/CD Deployment**: Automatic deployments to Netlify using GitHub Actions.

---

## 🛠️ Prerequisites

*   **.NET 8.0 SDK** (or later)

---

## 💻 Local Development

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/reggieray/mysite.git
    cd mysite
    ```

2.  **Run the local development server**:
    Run the preview server command:
    ```bash
    dotnet run -- preview
    ```
    This compiles the static site and launches a local server.
    
    Alternatively, you can run the PowerShell helper script:
    ```powershell
    ./localBuild.ps1
    ```

3.  **View the site**:
    Open [http://localhost:5080](http://localhost:5080) in your web browser. The server supports hot-reloading (changes to files in `input/` will recompile automatically).

---

## 📦 Deployment (GitHub Actions to Netlify)

The project is configured to build and deploy automatically to Netlify via GitHub Actions when pushing changes to the `master` branch (configured in `.github/workflows/deploy.yml`).

To activate deployment, configure the following **Repository Secrets** in your GitHub repository (**Settings** -> **Secrets and variables** -> **Actions**):

1.  **`NETLIFY_AUTH_TOKEN`**: A Netlify Personal Access Token (generate one in your Netlify User Settings -> *Applications* -> *Personal access tokens*).
2.  **`NETLIFY_SITE_ID`**: Your Netlify Site's API ID (located in Site Settings -> *General* -> *Site details*).

Once configured, pushes to `master` and pull requests will automatically build and publish!
