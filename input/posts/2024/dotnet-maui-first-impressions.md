Title: .NET MAUI First Impressions
Published: 7/20/2024
Tags: 
- maui
- xamarin.forms
- dotnet
- dotnet 8

---

# Introduction

In my past I've built mobile apps for companies and my side projects using Xamarin.Forms, Android apps using Kotlin/Java, so I'm a bit familiar with the Android ecosystem. Although it has been a while since I had coded in this space and all my side project Android apps looks more like a grave yard of delisted applications due to neglect.

So predominantly as a dotnet engineer for the longest time I wanted to try dotnet MAUI. So with that in mind I created a dotnet MAUI app, but to make it interesting, I went about seeing if I could port a few years old Xamarin.Forms app called [LottoNumbers](https://github.com/reggieray/LottoNumbers). It's a simple app that I created for demo purposes, it generates numbers for lottery games, but it highlighted use of a few things you'd typically see in a production app, including but not limited to:

- MVVM
- Dependency Injection
- Navigation
- Animation
- Firebase
- Custom controls
- OS specific features

The app made use of [Prism](https://github.com/PrismLibrary/Prism) which helped with setting up the ViewModels, DI and navigation. Animation was achieved with Lottie and Xamanimation. Here a few links to the packages/frameworks. 

* [Xamarin.Forms](https://www.xamarin.com/forms)
* [Xamarin.Essentials](https://www.github.com/xamarin/essentials)
* [Prism](https://github.com/PrismLibrary/Prism)
* [LottieXamarin](https://github.com/Baseflow/LottieXamarin)
* [Xamarin.Firebase.Config](https://github.com/xamarin/GooglePlayServicesComponents)
* [Xamanimation](https://github.com/jsuarezruiz/Xamanimation)

# Research

Before I started I did a bit of research to see if I could use the same/similar libraries. .NET MAUI is a evolution of Xamarin.Forms, at least that's what the tag line says and from what I found some libraries do seem to work, but it's a bit of hit and miss.

I did find some libraries such as [Prism](https://prismlibrary.com/) did exist for .NET MAUI, but with two big caveats at time of writing, one it was a pre-release and two they had introduced a license fee, although that said it's mainly aimed at commercial apps. That in mind I made the intentional decision not to use Prism. It must be said though I do like the lib and what problems it solves for you allowing you to focus on other things, like building features.

# Things I learned

1. Setup was not straight forward:
    
    I followed the tutorial of setting up a new project, but for some reason the project would not build successfully. I removed some lines in the `.csproj` so it only targeted Android, which I think did help. As I try and recall though the errors weren't very useful. It's a shame you don't get that new project build experience or at least from what I had experienced. 

2. [MVVM toolkit](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/): 

    When you research building .NET MAUI projects, you'll come across this in alot of examples. The things I ended up using were [`[ObservableProperty]`](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/observableproperty), [`[RelayCommand]`](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/relaycommand) & [`ObservableObject`](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/observableobject). This is not strictly limited to .NET MAUI as it does describe itself as UI Framework Agnostic.

3. Lack of support for Firebase:

    I was surprised to find that the libraries that used to work for Xamarin.Forms had not been ported over to .NET MAUI, hence no official support from Microsoft to have this working. I did however find [Plugin.Firebase](https://github.com/TobiasBuchholz/Plugin.Firebase) which is a open source project which did catered for Firebase integration.  

4. Unable to unit test a .NET MAUI project: 

    This one also surprised me to find you can't create a unit test project that reference a .NET MAUI project, the main reason is because doesn't target the same workload. The workaround is to put your logic in another library and then have your MAUI project reference that as-well as your unit test project.  

5. [AsyncAwaitBestPractices](https://github.com/brminnick/AsyncAwaitBestPractices)

    I stumbled on this library by accident for the need to run a async operation in a constructor which isn't async. There is also a good NDC talk on this on YouTube [Correcting Common Async/Await Mistakes in .NET 8 - Brandon Minnick - NDC London 2024](https://www.youtube.com/watch?v=GQYd6MWKiLI).   

# Summary

It's worth mentioning I did spend a day on and off working on this. There were some things I liked, but also a lot of things I didn't like. It has some promise, but I don't think it delivers on the promised land for building cross platform mobile applications.

The bits I did like was the code generation using attributes, although you could say that's not strictly MAUI, it's the MVVM toolkit. I think the way you register platform specific code is a bit better than Xamarin.Forms in terms of discoverability and maintainability. This was mainly due to the addition of a `MauiProgram.cs` class. Plus it had things that I liked in Xamarin.Forms which go without saying, things like XAML, data binding, etc.

The bits I didn't like is it didn't feel like a polished product that I could rely upon to build a cross platform application. Everyone points to the issues/bugs on [MAUI](https://github.com/dotnet/maui/issues), but from my own perspective, I found myself having to try and fight the framework to get it to work and I wasted alot of time constantly searching Google to find workarounds. In many places I included workaround code that made me question why go to all this effort.

As a dotnet dev I'm still hopeful it will get better or maybe they need to rethink the approach. In contrast I also tried [Flutter](https://flutter.dev/) and that was a much better experience, although not exactly the same in terms of how the app is built which I won't explore here, but as a offering for building cross platform apps. Something similar that I recently found out about in the dotnet ecosystem that is similar to Flutter in terms of how it works is [avaloniaui](https://avaloniaui.net/), which I'd like to try at some point.

# Links

Here are the links to the Github repos:

- [LottoNumber (Xamarin.Forms)](https://github.com/reggieray/LottoNumbers)
- [MauiLottoNumbers (.NET MAUI)](https://github.com/reggieray/MauiLottoNumbers)