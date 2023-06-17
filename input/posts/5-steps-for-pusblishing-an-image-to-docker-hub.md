Title: 5 steps for publishing an image to docker hub
Published: 6/9/2023
Tags: 
- dotnet
- docker
- csharp

---

# Introduction

In this blog post I will quickly go over the steps to publish a docker image to docker hub. I will be using a dotnet application I created as part of this [repo](https://github.com/reggieray/dotnet-health-checks).

## Prerequisites:

I had [docker desktop](https://www.docker.com/products/docker-desktop/) installed, created an account with [Docker Hub](https://hub.docker.com/) and created a [public](https://hub.docker.com/r/matthewregis/public/) repository.

# Steps

1. Pull repo and navigate to Dockerfile

```PowerShell
> git clone https://github.com/reggieray/dotnet-health-checks
Cloning into 'dotnet-health-checks'...
remote: Enumerating objects: 33, done.
remote: Counting objects: 100% (33/33), done.
remote: Compressing objects: 100% (25/25), done.
remote: Total 33 (delta 4), reused 29 (delta 4), pack-reused 0
Receiving objects: 100% (33/33), 11.44 KiB | 1.63 MiB/s, done.
Resolving deltas: 100% (4/4), done.

> cd .\dotnet-health-checks\src\Healthz.Api\
```

2. Tag container image

Here I have used a tag made from a repo that I had already setup in docker hub.

```PowerShell
> docker build -t matthewregis/public:dotnet-health-checks .
```

3. Login into docker hub

Make sure you are logged into to be able to push to docker hub.

```PowerShell
> docker login -u matthewregis
Password:
Login Succeeded

Logging in with your password grants your terminal complete access to your account.
For better security, log in with a limited-privilege personal access token. Learn more at https://docs.docker.com/go/access-tokens/
```

4. Push image to docker hub

The image should exist on your local docker images, now it's a case of pushing it up.

```PowerShell
> docker push matthewregis/public:dotnet-health-checks
The push refers to repository [docker.io/matthewregis/public]
f0fce0f2d485: Pushed
d50098e78298: Pushed
095a233d22c3: Pushed
7ecd15c472c7: Pushed
2069f56b479d: Pushed
3dab9f8bf2d2: Pushed
dotnet-health-checks: digest: sha256:cb5bf975f3d6affeb31950f3e7c891f08f4392d96b0c891c47be560494aa59c4 size: 1581
```

5. Verify image on docker hub

In my case the image was made available at this url [https://hub.docker.com/layers/matthewregis/public/dotnet-health-checks/images/sha256-cb5bf975f3d6affeb31950f3e7c891f08f4392d96b0c891c47be560494aa59c4?context=explore](https://hub.docker.com/layers/matthewregis/public/dotnet-health-checks/images/sha256-cb5bf975f3d6affeb31950f3e7c891f08f4392d96b0c891c47be560494aa59c4?context=explore). I navigated to the [public repository](https://hub.docker.com/r/matthewregis/public/tags) first to see if it was listed.

# References

- [Containerize an application](https://docs.docker.com/get-started/02_our_app/)
- [Share the application](https://docs.docker.com/get-started/04_sharing_app/)