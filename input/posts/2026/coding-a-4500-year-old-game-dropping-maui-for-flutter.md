Title: "Coding a 4,500-Year-Old Game: Why I Dropped .NET MAUI for Flutter to Build Bored Games"
Published: 08/31/2026
Lead: "How discovering the Royal Game of Ur on YouTube led me to build a growing collection of classic board games in Flutter—and why I abandoned .NET MAUI to do it."
Tags:
- flutter
- dotnet
- maui
- app development
- indie hacking
- history
- board games
---

# Intro

> <img src="/posts/images/bored-games-feature-graphic.png" style="max-width: 100%">

I came across this video on the **[British Museum's YouTube channel](https://www.youtube.com/@britishmuseum)**: **[Tom Scott vs Irving Finkel: The Royal Game of Ur](https://www.youtube.com/watch?v=WZskjLq040I)**. In it, Irving Finkel—a curator at the British Museum—explains and plays one of the oldest known board games in human history, dating back roughly 4,500 years to ancient Mesopotamia. He reconstructed the rules from an ancient Babylonian cuneiform tablet.

As someone who enjoys chess and history, I found it fascinating. The idea that the same game was being played 4,500 years ago by another person just to pass the time is hard to wrap your head around. It's a small glimpse into the past.

I wanted to play it on my phone. That's where the problem started.

---

## 🎲 The Problem

I couldn't find a single app that had both Chess and the Royal Game of Ur. Those were the two games I knew I wanted—everything else was a bonus. If I wanted to play multiple classic board games I'd have to download a different app for each one. I didn't want that.

I wanted one app. It didn't exist, so I built it.

---

## 🔤 The Name

I wanted something that stood out rather than another generic "Board Game Collection" app. **Bored Games** — a play on "board games" — felt right. Simple, memorable, and it does what it says on the tin.

---

## 🛠️ Why I Started with .NET MAUI

Coming from a .NET background, **.NET MAUI** was the obvious first choice for a cross-platform mobile app. A single C# codebase targeting both Android and iOS sounded great on paper.

In practice, I kept running into quirks that required workarounds. Building interactive 2D game boards wasn't smooth, and when I looked at integrating Firebase for analytics and crash reporting, it felt like an afterthought in the MAUI ecosystem. Community packages were often outdated and official support was limited. Progress was slow and the friction added up.

---

## 🐦 Switching to Flutter

I made the decision to start over in **Flutter**, and the difference was noticeable straight away.

Drawing complex game boards — a 19x19 Go grid, an asymmetrical Ur board — was straightforward with Flutter's widget model. Firebase integration was first-class and worked without the usual headaches. Flutter compiles to native ARM code, so performance wasn't a concern either. And the same codebase will target iOS when I get to that.

---

## 🕹️ Building the Collection

With Chess and Ur as the starting point, I worked through as many popular and well-known classic board games as I could — pulling from different cultures and time periods. Backgammon, Senet, Checkers, Mancala, Go, Connect Four and more made the list.

A few of them — **Reversi**, **Go**, and **Mahjong** — I didn't actually know how to play before building them. Working out the rules and implementing an AI opponent for each was a good way to learn.

The collection isn't fixed. I plan to keep adding games over time.

---

## 📲 The Result: Bored Games

**Bored Games** is a growing collection of classic board games in a single, lightweight app. It works offline, has AI opponents, and a consistent UI across all games.

📥 **[Download Bored Games on Google Play](https://play.google.com/store/apps/details?id=dev.matthewregis.bored_games)**

<div style="display: grid; grid-template-columns: repeat(4, 1fr); gap: 12px; margin: 1.5em 0;">
  <img src="/posts/images/bored-games-screenshot-1.jpg" style="width: 100%; border-radius: 12px; box-shadow: 0 4px 16px rgba(0,0,0,0.4);">
  <img src="/posts/images/bored-games-screenshot-2.jpg" style="width: 100%; border-radius: 12px; box-shadow: 0 4px 16px rgba(0,0,0,0.4);">
  <img src="/posts/images/bored-games-screenshot-3.jpg" style="width: 100%; border-radius: 12px; box-shadow: 0 4px 16px rgba(0,0,0,0.4);">
  <img src="/posts/images/bored-games-screenshot-4.jpg" style="width: 100%; border-radius: 12px; box-shadow: 0 4px 16px rgba(0,0,0,0.4);">
  <img src="/posts/images/bored-games-screenshot-5.jpg" style="width: 100%; border-radius: 12px; box-shadow: 0 4px 16px rgba(0,0,0,0.4);">
  <img src="/posts/images/bored-games-screenshot-6.jpg" style="width: 100%; border-radius: 12px; box-shadow: 0 4px 16px rgba(0,0,0,0.4);">
  <img src="/posts/images/bored-games-screenshot-7.jpg" style="width: 100%; border-radius: 12px; box-shadow: 0 4px 16px rgba(0,0,0,0.4);">
  <img src="/posts/images/bored-games-screenshot-8.jpg" style="width: 100%; border-radius: 12px; box-shadow: 0 4px 16px rgba(0,0,0,0.4);">
</div>

If you're into chess or board games in general — or if you watch that Irving Finkel video and want to give Ur a go — hopefully you'll find something in there worth playing.

