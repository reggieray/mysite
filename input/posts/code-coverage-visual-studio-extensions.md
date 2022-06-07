Title: Code coverage Visual Studio extensions
Published: 6/2/2022
Tags: 
- dotnet
- code coverage
- visual studio
- test
- csharp
- NCrunch
- Fine Code Coverage

---

# Overview

In this post I'll mention two Visual Studio extensions that gives you a nice visual representation of test coverage, NCrunch (Paid) and Fine Code Coverage (Free). 

I originally only used NCrunch, but it's not free and I was given it as part of my job. I found that I wanted a similar experience when working on my own projects, but I couldn't justify the cost for personal use. The solution I ended up with was Fine Code Coverage. 

A few things worth mentioning:

- These tools are not mutually exclusive, in fact I have both installed on my work laptop.
- I won't cover all the features of each tool.

# NCrunch

Download from the site [www.ncrunch.net/download](https://www.ncrunch.net/download)

What does NCrunch say about themselves:

> NCrunch is a fully automated testing extension, engineered to make coding and testing a breeze.
Forget about stopping to run your tests and let NCrunch do the work for you.
Code and test at the speed you think!

The green dots denote the line of code being covered and the test is passing. When hovering over the green dot you can navigate to the test code that is providing that coverage. The dots will turn to red if the tests are failing and empty dots show code that has no test coverage.

> <img src="/posts/images/ncrunch-dots.png" height="500">

NCrunch can be configured to execute tests in parallel, which gives you quick feedback. 

> <img src="/posts/images/ncrunch-window-pane.png" height="500">

NCrunch has many more features, but main feature I find aids development the most is the dots that visually show code coverage and then allow you to easily navigate to those tests.

# Fine Code Coverage

Download this extension from the [Visual Studio Market Place ( vs 2019 )](https://marketplace.visualstudio.com/items?itemName=FortuneNgwenya.FineCodeCoverage), [Visual Studio Market Place ( vs 2022 )](https://marketplace.visualstudio.com/items?itemName=FortuneNgwenya.FineCodeCoverage2022)
or download from [releases](https://github.com/FortuneN/FineCodeCoverage/releases).  Older versions can be obtained from [here](https://ci.appveyor.com/project/FortuneN/finecodecoverage/history).

A quote from the description:

> Visualize unit test code coverage easily for free in Visual Studio Community Edition (and other editions too)

Not offering like for like features as NCrunch. This tool indicates if a line is covered by the lines on the side. Green shows code that is covered by passing tests. Amber signals that the line is covered, but not all paths. Red shows if a covering test is failing.

> <img src="/posts/images/fine-code-coverage-code.png" height="500">

Fine code coverage also provides a windows pane with some tabs that provide insightful data on how much lines of code is covered.

> <img src="/posts/images/fine-code-coverage.png" height="400">

To get quick feedback when working on my own projects I set the tests to run after every build.

> <img src="/posts/images/fine-code-coverage-unit-tests.png" height="450">

The shortcut to run a build is `Ctrl + Shift + B` which then kicks off the tests and gives me quicker feedback when coding. It's not quite the same as NCrunch, but still find it useful when developing.

A tool that is worth exploring if you want to use code coverage in your automated builds is [Coverlet](https://github.com/coverlet-coverage/coverlet) which is what Fine Code Coverage uses.