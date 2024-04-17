Title: Advent of Code with ChatGPT
Published: 12/04/2023
Tags: 
- AoC
- advent of code
- ChatGPT 3.5
- ChatGPT
- dotnet
- dotnetfiddle.net

---

# Introduction

In this post I'll go over how I experimented with how far I could really get just using ChatGPT to solve code challenges. The code challenge I used was from Advent of Code (AoC). 

AoC is an annual programming competition held in December, featuring daily puzzles that participants solve using their programming skills. AoC is code-agnostic, meaning participants can use any programming language of their choice to solve the puzzles. 

Hopefully ChatGPT would not have had time to be trained on the question so didn't have enough data to go off to spit out a correct answer straight away.

To also make this more of a interesting challenge I put extra constraints on myself:

1. No IDE: Not allowed to make use of IDE's such as Visual Studio, Jet Brains Rider or Visual Studio Code.
2. No Tests: Not allowed to put tests around the solution to validate the code.
3. No Debugging: Not allowed to debug through the solution.

By virtue of not having a IDE it made the 2nd and 3rd points very difficult to achieve, but I wanted to put it down for clarification.

It's worth mentioning the version of ChatGPT I used is 3.5.

# The Code Challenge

The specific code challenge I gave it was **Day 4: Scratchcards**. I won't repeat the question here, if you want to see the question in detail follow this [link](https://adventofcode.com/2023/day/4).

As a dotnet developer I choose C# as my code of choice to solve the challenge, but as mentioned previously I put extra constraints on myself so I had to find a solution to run the C# code. For this I already had a website in mind [DotNetFiddle](https://dotnetfiddle.net/). 

DotNetFiddle is an online C# compiler and coding platform that allows developers to write and run C# code directly in a web browser.

# The Process

If you want to see the full chat click this [link](https://chat.openai.com/share/7e79993b-8fa7-442b-b1bc-8665660caf4f) or copy and paste this URL in your web browser https://chat.openai.com/share/7e79993b-8fa7-442b-b1bc-8665660caf4f.

As you may have noticed I started off with a instruction prompt.  

> when I ask you to create code, create it in dotnet, add the sample as a literal string, and make the methods and program public.

This was because I had previously played around with ChatGPT and found it gave answers in a similar style and I would repeat myself each iteration on getting the code I wanted. The main purpose of my instructions was so I could copy and paste the code and have it compile and run straight away.

I also just copied the question verbatim in the hopes it would not get confused with my abstract version of the question. Also part of me wanted to see how far I could get with very little effort. I did read the question myself, but not in great detail. I skim read it at best.

> <img src="/posts/images/advent-of-code-with-chatgpt-1.png" style="max-width: 100%">

The first answer it gave was a good attempt, but it had a compilation error:

`Compilation error (line 59, col 17): Cannot implicitly convert type 'double' to 'int'. An explicit conversion exists (are you missing a cast?)`

Then with very little effort I gave this feedback to ChatGPT. It took a few attempts, but I managed to get a solution that would run, but the answer it came too was wrong. 🤔

> <img src="/posts/images/advent-of-code-with-chatgpt-2.png" style="max-width: 100%">

It was 22 when it should have been 13. This was based of the example in the question. I had yet to use the specific question input I was given. If your not familiar with AoC, each individual will get a specific input and in turn each person will have a different answer, so you cannot just copy someone else answer. You have to run your input through code to come up with your answer.

> <img src="/posts/images/advent-of-code-with-chatgpt-3.png" style="max-width: 100%">

Again with very little thinking I tried to get it to come up with the correct answer without guiding it, but just feeding back if the solution was correct or incorrect. I could see this was not getting very far, so I had to then resort to being a bit more deliberate in guiding it into coming up with the correct solution.


At this point I had to go over the question and try and understand what the question was asking and then look at the code to understand where it needed guidance. I think I needed more guidance though because I wrongly assumed it was asking to calculate points/score based off the [fibonacci sequence](https://en.wikipedia.org/wiki/Fibonacci_sequence) 😆. Once I realized my mistake I had to correct ChatGPT.

> <img src="/posts/images/advent-of-code-with-chatgpt-4.png" style="max-width: 100%">

After then carefully reading the question and code ChatGPT had generated I had identified the area that needed the correction. I found I could be specific in asking it to alter just the function in question. 

To get the result I wanted I had to work out the first iterations as examples and feed this into ChatGPT, with this I hoped it would then get better clarity on what I was asking it to achieve and refine the certain piece of code.

> <img src="/posts/images/advent-of-code-with-chatgpt-5.png" style="max-width: 100%">

Eventually it came up to the correct answer. At this point I copied my specific input and pasted over the example in the code and ran it. It spat out a number which I then used to put into AoC and voila, it worked like a charm. 

# The Solution

If you want to see the full solution with the example input here it is. If you can't see the code below you can browse to it using this [link](https://dotnetfiddle.net/Widget/o02dDc).

<iframe width="100%" height="475" src="https://dotnetfiddle.net/Widget/o02dDc" frameborder="0"></iframe>

# Observations

- It apologized a lot: I'm not sure if this was something that it had learned or a prompt put in, but I found it to be mildly annoying after the first apology. I found myself having to filter out the the "fluff" just so I could get what I was looking for. In most cases I ignored the response and focused on the code output. Also when I found I was incorrect with something I apologized without thinking. It's funny how you pick things up subliminally.  
- Invalid code: I surprised to find at the start most of the code it generated didn't work straight away. Although most of it was valid, it slipped up on the minor intricacies.    
- Back and forth: I was also surprised to find myself having to keep going back to the ChatGPT to refine it's answer. I think it took me 18 attempts to get the answer. 
- Code generated: It generated a lot of correct code at the start, it understood the question to a certain level and had already broken it down into what methods to create and adding in comments to boot. It was good enough that someone who could code could easily refine it themselves to make it work.
- Code style: The code it generated was easy to follow. It could of took a number of different approaches, but it seemed to opt for the most readable. 

# Summary

As someone who has recently started using [Github Copilot](https://github.com/features/copilot) I find myself using AI more and more now and find it to be quite a positive experience. It does a lot of the grunt work for you, but it doesn't get it right 100% of the time, that's why it's just as important to understand what it's spitting out. And with this as an example it has only reenforced that belief.

The potential looks promising. I don't see it replacing human programmers in the future (I could be wrong), but it seems like you need someone with the knowledge to validate what it's generating. Maybe we'll have a AI for that as-well and eventually a consortium of AI's 😆, but without trying to get too speculative about the future I think at the moment AI is another tool in a persons tool set to achieve a task or goal and in the coding context it does it quite well.

I think you can't afford to be lazy in not proof reading what it has generated, but instead see it as a tool that you can learn from to achieve your goal or task quicker or more effectively/efficiently.

One thing that comes to mind is [AlphaGo - The Movie](https://www.youtube.com/watch?v=WXuK6gekU1Y) on YouTube, if you haven't watched it then I highly recommend to do so. One of the themes that I took away from this is that if we work with AI, we can learn from it and it  can make us better at a particular task or topic. 

# Links

- [AoC - Day 4: Scratchcards](https://adventofcode.com/2023/day/4)
- [ChatGPT chat](https://chat.openai.com/share/7e79993b-8fa7-442b-b1bc-8665660caf4f)
- [dotnetfiddle solution](https://dotnetfiddle.net/Widget/o02dDc)
- [AlphaGo Documentary](https://www.youtube.com/watch?v=WXuK6gekU1Y)