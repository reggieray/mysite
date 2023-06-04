Title: Boost your kubectl productivity with these 10 kubectl commands.
Published: 5/7/2023
Tags: 
- kubernetes
- kubectl

---

# Introduction

I have been recently been studying for the [Certified Kubernetes Application Developer (CKAD)](https://training.linuxfoundation.org/certification/certified-kubernetes-application-developer-ckad/#) exam which is a practical, hands-on exam that demonstrates proficiency in Kubernetes application development. The exam is time limited, because of this it is very important that you understand how to use kubectl commands efficiently. Here are 10 commands (although not all strictly kubectl commands) that helped speed up my productivity.

# Commands

## 1. Aliases for kubectl

Powershell:
```ps
Set-Alias -Name k -Value kubectl
```
Bash:
```bash
alias k=kubectl
```
This would shorten `kubectl run test-pod --image=busybox` to `k run test-pod --image=busybox`.


## 2. Variables for commands

A bit of a hack as I don't think variables intended use was for something like this, but handy for writing commands when speed is imperative. 

Powershell:
```ps
$dr = '--dry-run=client'
$i = '--image'
```

Bash:
```bash
dr='--dry-run=client'
i = '--image'
```

This would shorten 
```ps
k run test-pod --image=busybox --dry-run=client -oyaml > testpod.yaml
``` 
to 
```ps
k run test-pod $i=busybox $dr -oyaml > testpod.yaml
```
You could go overboard with this approach and end up having too many variables to remember, in which case you can always use `echo` command to output what the command would look like before running it, like so:

```ps
> echo "k run test-pod $i=busybox $dr -oyaml > test-pod.yaml"
k run test-pod --image=busybox --dry-run=client -oyaml > test-pod.yaml
```

## 3. List all resources in all namespaces

```ps
k get all -A
```

For getting that overall view at a quick glace.

## 4. Tacking on `-h` to give you help with a command

```ps
> k run -h
Examples:
  # Start a nginx pod
  kubectl run nginx --image=nginx

### continued output removed for brevity ### 
```

```ps
> k expose -h
Expose a resource as a new Kubernetes service.                                                                                                                                                                                                                                                                                                           Looks up a deployment, service, replica set, replication controller or pod by name and uses the selector for that
resource as the selector for a new service on the specified port. 

### continued output removed for brevity ### 
```

Quickly gives you example usages of the command you want to run, in most cases the example commands will give you what you need.

## 5. Get resource documentation with `explain`

```ps
k explain pods
k explain pods.spec.containers #  Get the documentation of a specific field of a resource  
```

Use `--recursive` to give you names of all fields 
```ps
k explain pods.spec --recursive
```
In my opinion much faster and complete than browsing the online documentation in giving you the shape of resource definition, although the online documentation is very helpful.

## 6. Using imperative commands to quickly generate declarative configuration 

```ps
k create deployment test-deployment --dry-run=client --image=busybox --replicas=3 -oyaml > test-deployment.yaml
```
results in creating a `test-deployment.yaml` file with the following contents:
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  creationTimestamp: null
  labels:
    app: test-deployment
  name: test-deployment
spec:
  replicas: 3
  selector:
    matchLabels:
      app: test-deployment
  strategy: {}
  template:
    metadata:
      creationTimestamp: null
      labels:
        app: test-deployment
    spec:
      containers:
      - image: busybox
        name: busybox
        resources: {}
status: {}
```

## 7. Using the `--force` option when deleting a resource

```ps
 k delete po my-pod --force
```

Saves a few seconds which could be very important to save from distracting yourself.

## 8. Using `vi` or `vim` in terminal

This could be it's own complete subject, but the main commands I use are:

opening a file
```ps
vim pod.yaml
```
While editing a file:
- `i` to switch to insert mode
- `esc` to switch to command mode
    - `: {line number} + Enter` to jump cursor to line position
    - `: set number + Enter` to show line numbers
    - `:wq + Enter` to save and exit

## 9. Shelling into a container with `exec`

```ps
k exec --stdin --tty shell-pod -- /bin/bash
```

Further onto this using commands that can make requests

```bash
curl localhost
# or (depending on the OS)
wget localhost
```

Using `cat` to output contents of file to terminal
```bash
cat app.log
```

## 10. Using `grep` to filter results

Powershell
```ps
k get po -A |Select-String -Pattern 'dashboard'
```

Bash
```bash
k get po -A |grep 'dashboard'
```