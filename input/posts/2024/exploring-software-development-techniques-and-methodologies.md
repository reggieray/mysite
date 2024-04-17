Title: Exploring Software Development Techniques & Methodologies
Published: 02/09/2024
Tags: 
- Software Craftsmanship 
- Code Kata
- Object Calisthenics
- Extreme Programming (XP)
- Sociable Unit Tests

---

# Introduction

I was perusing over some of my old blog posts because I was curious what I had written before. The one blog post that stood out most to me was [top development books](/posts/top-development-books), not because I thought it was great, but because I had mentioned certain books that I knew had a polarizing effect on people, like [Clean Code: A Handbook of Agile Software Craftsmanship](https://www.amazon.co.uk/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882/ref=as_li_ss_tl?keywords=clean+code&qid=1558296957&s=books&sr=1-1&linkCode=sl1&tag=regis02-21&linkId=0799e4dde35f6b1378f68807d41d487d) from "Uncle Bob" although at the time I was unaware.

For those unfamiliar, the main gripe I can gather that have been mentioned about Clean Code is that it prescribes to rigid programming practices that can lead people to follow them blindly that may not be relevant to a certain use case. A similarity that I can see is when someone starts learning design patterns and starts applying them to everything, even when it does't make sense, but because at that time the design pattern is shiny and new you try and apply it whenever you can. 

This could be summarized by this quote:

> If the only tool you have is a hammer, you tend to see every problem as a nail.

Not a solution, but a recommend approach to this is to apply critical thinking into everything you do. Ask questions to yourself, can you justify this approach if asked? What have you considered?

This got me thinking, if this book was useful to me? Yes and no, a-lot of what the book covered was stuff that I was already aware of, if anything it reaffirmed what I already knew. This led me onto another question though which was what things did I learn that did have a profound impact on me as a software engineer? This is what I will go over in this blog post.

# Code Katas 🥋

### What is it?

It's a practice involving repetitive problem-solving exercises to enhance coding skills and problem-solving abilities. Similar to martial arts kata, Code Kata emphasizes deliberate practice and exploration of different coding approaches.

### My experiences

Although I haven't been practicing this as much lately, what I found is that it had improved my ability to write testable code without thinking too much because I had programmed it into my muscle memory. Even better if you can take on a code kata with a group of people and share your solution after. This is a great opportunity to learn from others. This is what I found most impactful was the ability to learn from others.    

### Useful resources

- [codekata.com](http://codekata.com/) - a website of curated code katas.
- [awesome-katas](https://github.com/gamontal/awesome-katas) - a github repo of curated code katas.
- [code wars](https://www.codewars.com/) - a website designed to solve code challenges.  
- [codinggame](https://www.codingame.com/start/) - another website designed to solve code challenges.  

# Object Calisthenics 💪🏽

### What is it?

Object Calisthenics is a set of programming exercises designed to improve object-oriented design and code quality. These exercises promote practices such as encapsulation, cohesion, and the Single Responsibility Principle (SRP) to create cleaner, more maintainable code.

I won't delve into the details of the rules, you can go over them in [this blog post](https://williamdurand.fr/2013/06/03/object-calisthenics/) I found that did a good job.

### My experiences

If you've been in programming for a while, like I had before discovering this you may have already been applying a lot of the practices without realizing it. What I found is that it made it easy to understand which was the real benefit to me. It took a bunch of stuff of what I thought was unrelated and grouped it into a nice easy to understand list. Which makes it easier to refresh my memory and share this knowledge to others.

### Useful resources

- [Object Calisthenics](https://williamdurand.fr/2013/06/03/object-calisthenics/) - a blog post that goes over each rule.

# Extreme Programming 👩🏽‍💻

### What is it?

Programming (XP), an agile software development methodology focused on iterative development, customer collaboration, and continuous feedback. XP advocates for practices like test-driven development (TDD), pair programming, continuous integration, and frequent releases to improve software quality and responsiveness to change.

### My experiences

This is a massive topic in it's own right, which I won't try and cover here, I'll add some links below which should give you a good summary with more links to the topic in much greater detail.

There wasn't one main thing from this that had a impact, but it was a collective group of things as you may find out XP covers a lot. If you work with a team then it helps if you have team buy in as this is where I saw most of the benefit. When you have a team collectively working together then what you output will end up being better for it in my opinion and these practices help align people. Don't fall in the trap that I mentioned at the beginning though, don't follow rules just because it says so, be practical and within reason with what you decide and not decide to do.

### Useful resources

- [Extreme Programming wiki](https://en.wikipedia.org/wiki/Extreme_programming) - the wiki page on extreme programming.
- [ExtremeProgramming blog post](https://martinfowler.com/bliki/ExtremeProgramming.html) - a blog post from Martin Fowler.

# Sociable Unit Tests 🧪

### What is it?

Sociable Unit Tests is a testing approach that emphasizes testing units of code in isolation while allowing for integration and interaction testing when necessary. Sociable unit tests promote the writing of tests that are maintainable, readable, and focused on behavior rather than implementation details.

### My experiences

I wrote a blog post about this previously with use of a specific nuget though. You don't have to use that Nuget package though. The main takeaway is by structuring your tests in a certain way, you can achieve a better developing experience with fewer tests to maintain while still having high test coverage.

This should get you also thinking about your own test strategy, [The Practical Test Pyramid](https://martinfowler.com/articles/practical-test-pyramid.html) is a useful source to give you a good idea.   

### Useful resources

- [Sociable Unit Tests](https://matthewregis.dev/posts/sociable-unit-tests-with-bddfy) - my blog post with more links to resources on the topic. 
- [The Practical Test Pyramid](https://martinfowler.com/articles/practical-test-pyramid.html) - a blog post by Martin Fowler.