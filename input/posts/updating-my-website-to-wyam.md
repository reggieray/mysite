Title: Updating my website to wyam
Published: 4/15/2019
Tags: 
- .Net
- Wyam
---
# Introduction

It's been a while since I made any changes to my website, but the time has come to update my website once again. 

I also thought I would like to learn something diffrent, what I could do diffrently this time and find out what's changed since last time I looked at creating a personal website.

# Previous Setup

My previous website was created with [Hugo](https://gohugo.io/) which is a static website generator. I had come to use it because I liked what it had to offer and the benefits that came with a static website. I had looked at other options such as [Jekyll](https://jekyllrb.com/), but ultimately choose to use Hugo. At the time Jekyll was feature rich, but Hugo was simple and fast and because of that it was ultimately the reason I choose it.

# New options?

There are alot of options out there now for static website generators, which you can see at [staticgen.com](https://www.staticgen.com/). One that caught my attention though was [Gatsby](https://www.gatsbyjs.org/), I had seen a number of developers tweet about it and from looking more into it, it's more than your standard website generator. If I was looking to build something more complicated then I probably would of explored this option more further.

# Why WYAM?

Because I am a .Net developer, I was more inclined to favour a .Net solution. I also liked the fact it was simple and easy to get going. If you are familiar with Razor syntax in ASP.NET then it should come easy to use. 

# How

First make sure you have the dotnet core cli and install by typing in the following in a terminal

```Powershell
dotnet tool install -g Wyam.Tool
```

You can create a webstie how you see fit but there are two types of websites, which are called recipes already predefined for you to use. For example if you wanted to create a blog then type:

```Powershell
wyam new --recipe Blog
```

You should see the scaffolded files for you to work with. Before generating the webiste you can apply a theme for a blog type website, which you can find [Here](https://wyam.io/recipes/blog/themes/). To then generate the site run:

```Powershell
wyam --recipe Blog --theme CleanBlog
```

Then to view your site locally run:

```Powershell
wyam preview
```

If you are interested in looking at the source code then you can find it [Here](https://github.com/reggieray/mysite.git). If the link doesn't work copy and paste the following link in the URL bar https://github.com/reggieray/mysite.git.