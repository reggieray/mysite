Title: How I passed the Certified Kubernetes Application Developer) exam
Published: 7/29/2023
Tags: 
- kubernetes
- CKAD
---

# Introduction

In this blog post I'll share how I prepared and recently passed the [Certified Kubernetes Application Developer (CKAD)](https://training.linuxfoundation.org/certification/certified-kubernetes-application-developer-ckad/#) and how I found the whole experience. Hopefully there will be little nuggets of wisdom that you might find useful if you are preparing for the CKAD exam yourself.

I'll go in chronological order of how I prepared, which might not be the best or right way to go about it, but maybe there are some learnings that can be taken away from how I approached it.

I started at the end of March 2023 and took the exam in the middle of July 2023. Below is my certification I received from cloud native linux foundation.

> <img src="/posts/images/ckad-cert.jpg" style="max-width: 100%">

# CKAD Course 

The first thing I did before anything was buy this course [Kubernetes Certified Application Developer (CKAD) with Tests](https://www.udemy.com/share/1013BQ3@bPrI8t5qKeq7saONoCA52mKUwAjgC5fTr2VFXJsCtpi0SJKrq8Xx6mpHhwnkDazI/) from [udemy.com](https://udemy.com). I bought it when there was a discount on and in the end it cost me £16.99. It's listed at £59.99, but at the moment it's got a 73% discount offer making it £15.99. It seem that through out the year there is always a discount for this course.

The course structure consists of videos based off an older curriculum, but has been updated to keep up to date with todays curriculum and all of the videos structured in the old way are relevant today.

This course also gives you access to a online simulator environment via https://kodekloud.com/. In the course it gives you instructions on how to redeem this access. After each section in the course there were some labs where you were given some tasks against the simulator and near the end there were two mock tests.

> <img src="/posts/images/kodekloud-simulator.jpg" style="max-width: 100%">

I made heavy use of the simulator mock exams, even though I knew the answers after a few attempts it's good to practice becoming faster. I feel this significantly helped me in the real exam, because I did find I had some time at the end to review and adjust my answers. As you can see from the image above the simulator consists of two parts, the questions on the left and a console on the right. This is not to dissimilar to the real exam except in the real exam you are remoted into a linux box (Ubuntu 20.04 in my case) where you would end up launching a terminal and have access to a web browser.

# Local Setup

After watching the first videos of the course I felt like I wanted to try stuff out on my laptop. For this I installed [Minikube](https://minikube.sigs.k8s.io/docs/start/). For the container/VM manager I already had [docker](https://www.docker.com/) installed. I then installed [kubectl](https://kubernetes.io/docs/tasks/tools/#kubectl) which is the same command-line tool used in the exam.

Having this setup enabled me to try things out for myself and play around with some feature like [minikube dashboard](https://minikube.sigs.k8s.io/docs/handbook/dashboard/). The minikube dashboard is good for visualizing a structure for Kubernetes which in turn gives you a better understanding of what you can do with Kubernetes and how to go about it. 

> <img src="/posts/images/minikube-dashboard.jpg" style="max-width: 100%">

# Writing Blogs

To try and reenforce what I had learned and provide documentation for myself I went about creating blogs. This follows the principles of the [Generation effect](https://en.wikipedia.org/wiki/Generation_effect) which is a phenomenon where information is better remembered if it is generated from one's own mind rather than simply read.

I tried to apply structure to what blogs I wrote, as Kubernetes cover many things, I didn't want to get lost learning topics that weren't relevant for my indented purpose. So I used the curriculum for the exam as my structure.  

The latest curriculum can be found [here](https://github.com/cncf/curriculum). I tried to cover at least one topic in each section and where I didn't write a blog about a subject I did make sure to research and study it. Below is the curriculum structure with links to the topics I did write blogs about.

[Kubectl Fundamentals](https://matthewregis.dev/posts/boost-your-kubectl-productivity-with-10-kubectl-commands)

### Application Design and Build - 20%
- [Define, build](https://matthewregis.dev/posts/6-steps-for-running-a-dotnet-api-within-minikube) and modify container images
- Understand [Jobs and CronJobs](https://matthewregis.dev/posts/kubernetes-jobs-and-cron-jobs)
- Understand multi-container Pod design patterns (e.g. [sidecar](https://matthewregis.dev/posts/kubernetes-sidecar-container), [init](https://matthewregis.dev/posts/kubernetes-init-container) and others)
- Utilize [persistent and ephemeral volumes](https://matthewregis.dev/posts/kubernetes-persistent-and-ephemeral-volumes)

### Application Deployment - 20%
- Use Kubernetes primitives to implement common deployment strategies (e.g. [blue/green](https://matthewregis.dev/posts/kubernetes-blue-green-deployment) or canary)
- Understand Deployments and how to perform [rolling updates](https://matthewregis.dev/posts/kubernetes-rolling-updates)
- Use the Helm package manager to deploy existing packages

### Application Observability and Maintenance - 15%
- Understand [API deprecations](https://kubernetes.io/docs/reference/using-api/deprecation-policy/)
- Implement [probes and health checks](https://matthewregis.dev/posts/kubernetes-probes-and-health-checks)
- Use provided tools to [monitor Kubernetes](https://matthewregis.dev/posts/kubernetes-logging-and-monitoring) applications
- [Utilize container logs](https://matthewregis.dev/posts/kubernetes-logging-and-monitoring)
- Debugging in Kubernetes

### Application Environment, Configuration and Security - 25%
- Discover and use resources that extend Kubernetes (CRD)
- Understand authentication, authorization and admission control
- Understanding and defining [resource requirements, limits and quotas](https://matthewregis.dev/posts/kubernetes-resource-requirements-limits-and-quotas)
- Understand [ConfigMaps](https://matthewregis.dev/posts/kubernetes-configmaps)
- [Create & consume Secrets](https://matthewregis.dev/posts/kubernetes-secrets)
- Understand [ServiceAccounts](https://matthewregis.dev/posts/kubernetes-service-accounts)
- Understand SecurityContexts

### Services and Networking - 20%
- Demonstrate basic understanding of NetworkPolicies
- Provide and troubleshoot access to applications via services
- Use [Ingress rules](https://matthewregis.dev/posts/kubernetes-ingress-and-egress-rules) to expose applications

# Buy Exam

Although this seems like the most obvious step, I waited until there was a discount on and throughout the year there are multiple promotions or discounts going to be available. In the end I was able to purchase with a 50% discount. Keep an eye out on their [twitter feed](https://twitter.com/lf_training). Here's an example of a promotion during [cyber monday 2022](https://training.linuxfoundation.org/cyber-2022/#)

> <img src="/posts/images/linux-foundation-cyber-2022.jpg" style="max-width: 100%">

# Exam Simulator  

When you buy the exam you will have 2 attempts (per exam registration) to an exam simulator, provided by [Killer.sh](https://killer.sh). This simulator provides the most similar environment experience as the actual exam. They also offer simulators for other Kubernetes and linux exams. You can look at [CKAD](https://killer.sh/ckad) page for more details regarding CKAD, pay particular attention to 'The Remote Desktop' section, which provides a screenshot of the environment. You can see questions on the left and a remote desktop session on the right with a terminal and web browser with a page from the official kubernetes documentation open.

I made sure to use these exam simulators soon before the exam started so I could hopefully retain the information/experience I gained and take it into the real exam,

# The Exam

You can access the exam from the [My Portal](https://trainingportal.linuxfoundation.org/learn/dashboard/) of linuxfoundation.org. You should have a button to start the exam, the button will visible but disabled before the time you have booked your exam.

## Check in

Make sure to read the instructions provided as this may change. In my experience I checked in 30 minutes before the exam, I would recommend this if you want to make sure you start your exam on time as it can take time to check in depending on your setup and who you get as a proctor.

I took the exam at my sisters home office and made sure to remove any pictures off the wall, cleared the desk and unplugged any monitors. As part of the check-in process you will be connected to the proctor who can see and hear you, but you can't see or hear them. You will be able to communicate via a chat system.  Using the webcam of my laptop I panned around the room showing my exam environment and anywhere they asked to look, for example under the desk. The proctor also asked me to show my phone and show me placing it away from me out of arms reach. Once the proctor was happy with their checks, I was able to start the exam.  

## Exam time

This went pretty smoothly, although I did get contacted from the proctor through out the test.

- The first incident was when I put my face up to the screen, as I was concentrating I unconsciously leaning forward towards my laptop at which point the proctor messaged me to move back so they could clearly see me.
- The second incident was when my sisters cat started getting lonely or curious (I'm still not sure) why she couldn't see me and started pawing and meowing loudly at the door at which point the proctor messaged me to ask what that noise was in the background.

I also found myself reframing from muttering or speaking to myself and covering my mouth as I knew this is something the proctor would pick up on.

## Results

The exam results are given 24hr after the exam is taken. You should receive a email indicating if you have passed or not. You can also check the portal. I'll admit I refreshed the portal a couple times to see if I could see my results yet. 

# Summary

I found the exam to be the most fun and practical exam I've taken. Some exams have multiple choice questions which focuses on your memoisation skills. By preparing for this exam I've learned very practical skills that I can use in my job. 

### Bonus pic

And here is the culprit that almost ruined my CKAD exam 😺... I think I would have forgiven her though.

> <img src="/posts/images/cat.jpg" style="max-width: 100%">