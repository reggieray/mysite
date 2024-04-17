Title: Kubernetes init container
Published: 5/6/2023
Tags: 
- kubernetes
- kubectl
- minikube
- docker
- docker desktop

---

# What is a init container?

In Kubernetes, an init container is a specialized type of container that runs and completes its execution before the main containers in a pod start running. Init containers are designed to perform initialization tasks, such as prepping the environment, downloading or generating configuration files, or populating data needed by the main containers.

Here are some key points about init containers in Kubernetes:

- Order and Dependencies: Init containers provide a way to control the startup order of containers within a pod. They ensure that certain tasks or dependencies are fulfilled before the main containers start running. Init containers are executed sequentially, and each init container must complete successfully before the next one begins.

- Separate Execution: Init containers have their own image and lifecycle independent of the main containers in a pod. They run to completion, exit, and are not restarted. Once all init containers have finished, the main containers in the pod start.

- Shared Volume Mounts: Init containers can share volume mounts with the main containers, allowing them to read or modify files that are later accessed by the main containers. This enables them to perform tasks such as downloading files, generating configuration, or initializing databases before the main application starts.

- Use Cases: Init containers are useful in various scenarios. For example, they can be used to perform pre-flight checks, such as waiting for a database to be available or verifying network connectivity. They can also handle tasks like loading data into a shared cache, preparing SSL certificates, or performing migrations before the main application starts.

- Pod Restart: If a pod restarts due to a failure or scaling event, the init containers are executed again before the main containers start. This ensures that the initialization tasks are rerun, providing a consistent and predictable startup process.

- Error Handling: If an init container fails to complete successfully, Kubernetes retries it until it succeeds, up to a specified limit defined in the pod's configuration. If the retries exceed the limit, the pod is considered failed and can be restarted or rescheduled.

Init containers in Kubernetes allow you to perform setup tasks and ensure that dependencies are met before the main containers start running. They provide greater control over the initialization process and help maintain a reliable and consistent environment for your applications.

# Example

In the following example we will create a pod with the a init container. The init container will not run successfully until a service is created. We can observe the state of the init pod before and after the service is created.

## Prerequisites:

To run the example I had the following tools/software installed:
- [docker desktop](https://www.docker.com/products/docker-desktop/)
- [minikube](https://minikube.sigs.k8s.io/docs/start/)
- [kubectl](https://kubernetes.io/docs/tasks/tools/#kubectl)

I also setup a alias for kubectl using the following command:

```ps
Set-Alias -Name k -Value kubectl
```
Make sure minikube is up and running:

```ps
minikube start
```

## Implementation:

1. Create a `init-container.yaml` file with the following contents:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: myapp-pod
  labels:
    app.kubernetes.io/name: MyApp
spec:
  containers:
  - name: myapp-container
    image: busybox:1.28
    command: ['sh', '-c', 'echo The app is running! && sleep 3600']
  initContainers:
  - name: init-myservice
    image: busybox:1.28
    command: ['sh', '-c', "until nslookup myservice.$(cat /var/run/secrets/kubernetes.io/serviceaccount/namespace).svc.cluster.local; do echo waiting for myservice; sleep 2; done"]
```

2. Create the init container example with the following command:

```bash
k apply -f init-container.yaml
```

3. Observe init pod.

```bash
k get po myapp-pod

# output should be like the following
NAME        READY   STATUS     RESTARTS   AGE
myapp-pod   0/1     Init:0/1   0          99s
```

The container will not be created until the following command has run successfully:

`nslookup myservice.$(cat /var/run/secrets/kubernetes.io/serviceaccount/namespace).svc.cluster.local; do echo waiting for myservice; sleep 2; done` 

4. Now create `myservice` using the following command:

```bash
k expose po myapp-pod --port=80 --name=myservice
```

5. Observe init pod again, this time the pod should be up and running. Wait a few seconds if it doesn't appear as running initially.
```bash
k get po myapp-pod

# output should be like the following
NAME        READY   STATUS    RESTARTS   AGE
myapp-pod   1/1     Running   0          6m13s
```

6. Clean your kubernetes environment.

```bash
k delete pod myapp-pod
k delete service myservice
```

# References

- [Init containers](https://kubernetes.io/docs/concepts/workloads/pods/init-containers/)
- [Init containers in use](https://kubernetes.io/docs/concepts/workloads/pods/init-containers/#init-containers-in-use)