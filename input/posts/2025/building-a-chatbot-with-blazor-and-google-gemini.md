Title: Building a Chatbot with Blazor and Google's Gemini 🤖
Published: 08/23/2025
Tags: 
- dotnet
- dotnet 9
- blazor
- Blazor WASM
- gemini
- ai

---

In this blog post, we'll explore how to build a simple chatbot using [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) WebAssembly and Google [Gemini](https://ai.google.dev/). We'll also use [MudBlazor](https://mudblazor.com/) for UI. What you should end up with is something like this, where you can ask it all the important questions.

> <img src="/posts/images/chat-bot-demo.gif" style="max-width: 100%">
 
Live Demo 🌐 

Click [Here](https://reggieray.github.io/chat-bot/) or copy and paste `https://reggieray.github.io/chat-bot/` in your browser.

### Why Blazor WASM?

- C# and dotnet: 
  - I'm a dotnet dev by trade 😄, so having the ability to use C# and dotnet tooling was a big draw for me.
  - dotnet has been around for a long time and has many [NuGet](https://www.nuget.org/) packages that you can make use of. 
- Static files: 
  - Having the ability to deploy a website that is just static files opens up many doors for publishing. I took advantage of [Github pages](https://docs.github.com/en/pages) to host, but there are much more hosting providers that offer free hosting for static files, [netlify](https://www.netlify.com/) being one of many that this site is hosted on.


### Why Gemini?

- Generous Free tier:
  - Gemini from my research seemed to be the only gen AI that offered a free tier. I tried ChatGPT, xAI's Grok among others and you have to add credit to get it to work.
- Models: 
  - Gemini offers many models which offer you the flexibility to choose the right model for your scenario.

As a side note I think Google's Gemini (or what it will be called in the future) will always be at the forefront of gen ai or ai in general because of the people behind it. [Google Deepmind](https://deepmind.google/) has been described like the [bell labs](https://www.forbes.com/sites/michaelposner/2024/10/11/from-bell-labs-to-google-corporate-accolades-and-responsibilities/) of AI, which is a world apart from how I would describe other AI's, which seem to be business focused.

### Why MudBlazor?

- Components:
  - MudBlazor offers an extensive range of components, making it easy to build things fast.
  - There is also a variety of [community extensions](https://mudblazor.com/mud/community/extensions) 
- Documentation
  - The documentation with code examples make it easy to pick up.  

# Project Setup 🛠️

Create the blazor project.

```
dotnet new blazorwasm -o Chat.Bot
```

I then moved this into a `src` folder.

Add the following Nuget Packages:

- [`Blazored.LocalStorage`](https://github.com/Blazored/LocalStorage) - To store any data locally within the browser.
- [`Google_GenerativeAI`](https://github.com/gunpal5/Google_GenerativeAI) - A unofficial library to interface with Google gemini. There was no official SDK when this was created.
- [`MudBlazor.Markdown`](https://github.com/MyNihongo/MudBlazor.Markdown) - A component for displaying markdown. Useful for display gen ai content as they love to respond if markdown. 

The `csproj` should look like:

```xml
<ItemGroup>
    <PackageReference Include="Blazored.LocalStorage" Version="4.5.0" />
    <PackageReference Include="Google_GenerativeAI" Version="3.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="9.0.8" />
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="9.0.8" PrivateAssets="all" />
    <PackageReference Include="MudBlazor.Markdown" Version="8.11.0" />
</ItemGroup>
```
We also need to register the necessary services in `Program.cs`:

```csharp
builder.Services.AddMudServices();
builder.Services.AddMudMarkdownServices();
builder.Services.AddBlazoredLocalStorage();
```

Also follow the instructions for setting up MudBlazor and MudBlazor.Markdown on the [getting started](https://github.com/MyNihongo/MudBlazor.Markdown?tab=readme-ov-file#-getting-started) documentation.


### Home Page 🏡

Update the home page with the following, this is to setup the Gemini key and make sure it connects successfully.

```csharp
@page "/"
@using GenerativeAI
@using GenerativeAI.Exceptions
@inject Blazored.LocalStorage.ILocalStorageService localStorage

<PageTitle>Chat Bot</PageTitle>
<MudPaper Class="pa-6 mx-auto mt-6" Elevation="4" Rounded="true">
    <MudMarkdown Value="@_welcomeContent" />
</MudPaper>


<MudPaper Class="pa-6 mx-auto mt-6" Elevation="4" Rounded="true">
    <MudStack Spacing="3">
        <MudText Typo="Typo.h5" GutterBottom="true">Gemini Settings</MudText>

        <MudTextField @bind-Value="_inputText" Label="Api Key" Variant="Variant.Outlined" FullWidth="true" />

        <MudButton Variant="Variant.Filled" Color="Color.Primary" OnClick="SyncModels">
            Sync Models
        </MudButton>

        @if (_options.Any())
        {
            <MudSelect T="string" @bind-Value="_selectedOption" Label="Gemini Model" Variant="Variant.Outlined"
                FullWidth="true">
                @foreach (var option in _options)
                {
                    <MudSelectItem Value="@option">@option</MudSelectItem>
                }
            </MudSelect>

            <MudButton Variant="Variant.Filled" Color="Color.Primary" OnClick="SaveSettings">
                Save
            </MudButton>
        }

        @if (!string.IsNullOrEmpty(_errorMessage))
        {
            <MudAlert Severity="Severity.Error" ContentAlignment="HorizontalAlignment.End">@(_errorMessage)</MudAlert>
        }

    </MudStack>
</MudPaper>

@code {
    private string _inputText { get; set; }
    private string _selectedOption { get; set; }
    private string _errorMessage { get; set; }
    private string _welcomeContent =>
    @"### Getting Started

1. Create a free [Genimi api key](https://aistudio.google.com/apikey)
1. Enter the api key below.
1. Click `SYNC MODELS`. A dropdown should appear with available models.
1. Select Model and click `SAVE`.
1. Navigate to `Chat` page from the side menu.
1. Chat away 🚀🚀🚀

> **_NOTE:_** Any data is stored on your local storage within the browser. This app doesn't log or send data
anywhere. See [source code](https://github.com/reggieray/chat-bot).

";

    private List<string> _options = new();

    protected override async Task OnInitializedAsync()
    {
        // Load saved settings from local storage
        _inputText = await localStorage.GetItemAsync<string>("api_key") ?? string.Empty;
        _selectedOption = await localStorage.GetItemAsync<string>("model") ?? string.Empty;
    }

    private async Task SaveSettings()
    {
        // Save to local storage
        await localStorage.SetItemAsync("api_key", _inputText);
        await localStorage.SetItemAsync("model", _selectedOption);

        Console.WriteLine($"Saved -> Text: {_inputText}, Option: {_selectedOption}");
    }

    private async Task SyncModels()
    {
        try
        {
            var models = await new GoogleAi(_inputText).ListModelsAsync();

            _options = models?.Models.Select(x => x.Name).ToList();
        }
        catch (ApiException ex)
        {
            _errorMessage = ex.ErrorMessage;
        }
    }
}
```

- Getting started: Describes the steps on how to use the app 
- Gemini Settings: 
  - Api Key: A simple text box to enter the API KEY and if it works show a drop down to pick a gemini model
  - Model Picker: If the previous step works this will populate with models available to pick.

### Chat Page 🤖

Set up the chat page with the following, this sets up the chat interface.

```csharp
﻿@page "/chat"
@using GenerativeAI
@inject Blazored.LocalStorage.ILocalStorageService localStorage

<MudContainer MaxWidth="MaxWidth.Medium" Class="pa-0">
    <div class="d-flex flex-column" style="height: 90vh;">

        <div id="chat-container" class="flex-grow-1 pa-4" style="overflow-y: auto;">
            @foreach (var message in chatMessages)
            {
                <div class="@($"d-flex flex-row my-2 {(message.Author == "user" ? "flex-row-reverse" : "")}")">
                    <MudPaper Class="@($"pa-3 rounded-lg {(message.Author == "user" ? "mud-theme-primary" : "mud-theme-surface")}")" Elevation="2">
                        @if (message.Author == "user")
                        {
                            <MudText>@(message.Text)</MudText>
                        }
                        else
                        {
                            <MudMarkdown Value="@message.Text" />
                        }
                        
                    </MudPaper>
                </div>
            }
            @if (_isSending)
            {
                <div class="d-flex flex-row my-2">
                    <MudPaper Class="pa-3 rounded-lg mud-theme-surface" Elevation="2">
                        <MudProgressCircular Indeterminate="true" Size="Size.Small" />
                    </MudPaper>
                </div>
            }
        </div>

        <MudPaper Elevation="2" Class="pa-2 d-flex">
            <MudTextField @bind-Value="newMessage"
                          Placeholder="Ask the AI anything..."
                          Variant="Variant.Outlined"
                          Adornment="Adornment.End"
                          AdornmentIcon="@Icons.Material.Filled.Send"
                          OnAdornmentClick="SendMessage"
                          OnKeyUp="HandleKeyUp"
                          Class="flex-grow-1"
                          Lines="1"
                          Disabled="_isSending" />
        </MudPaper>
        <MudText>@model</MudText>
    </div>
</MudContainer>

@code {

    private string apiKey = "";
    private string model = "";

    private string newMessage = "";
    private List<ChatMessage> chatMessages = new List<ChatMessage>();
    private bool _isSending = false;

    private ChatSession? _geminiChatSession;

    private async Task SendMessage()
    {
        _isSending = true;

        if (!string.IsNullOrWhiteSpace(newMessage))
        {
            if (_geminiChatSession is null)
            {
                _geminiChatSession = GetGeminiClient().StartChat();
            }

            var userMessage = new ChatMessage { Text = newMessage, Author = "user" };
            chatMessages.Add(userMessage);

            var geminiResponse = await _geminiChatSession.GenerateContentAsync(newMessage);

            chatMessages.Add(new ChatMessage { Text = geminiResponse.Text()!, Author = "ai" });

            newMessage = "";
        }

        _isSending = false;
    }

    private async Task HandleKeyUp(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            await SendMessage();
        }
    }

    private GenerativeModel GetGeminiClient()
    {
        return new GoogleAi(apiKey)
            .CreateGenerativeModel(model);
    }

    protected override async Task OnInitializedAsync()
    {
        apiKey = await localStorage.GetItemAsync<string>("api_key") ?? string.Empty;
        model = await localStorage.GetItemAsync<string>("model") ?? string.Empty;
    }

    public class ChatMessage
    {
        public string Text { get; set; }
        public string Author { get; set; } // "user" or "ai"
    }
}
```

- Chat display area → shows user and AI messages, styled differently.
- Input box → lets the user type a message, press Enter, or click send.
- Loading state → shows a spinner while waiting for AI’s response.
- Code-behind logic →
   - Loads API key + model from local storage.
   - Manages chat messages list. Notice the use of `ChatSession`, this keeps the context of the chat session.
   - Sends user input to Gemini and adds the AI’s reply to the chat.

### Nav Menu 🔗

Don't forget to update the nav menu to link to each page.

```xml
<MudNavMenu>
    <MudNavLink Href="" Match="NavLinkMatch.All" Icon="@Icons.Material.Filled.Home">Home</MudNavLink>
    <MudNavLink Href="chat" Match="NavLinkMatch.Prefix" Icon="@Icons.Material.Filled.Chat">Chat</MudNavLink>
</MudNavMenu>
```


# Conclusion 🎉

And that's it! We've built a simple but functional chatbot using Blazor and the Google's Gemini. You can extend this project by adding features like conversation history, user authentication, and more.

The source code can be found [here](https://github.com/reggieray/chat-bot) or at this URL `https://github.com/reggieray/chat-bot`.

