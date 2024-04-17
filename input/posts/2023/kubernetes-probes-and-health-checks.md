Title: Kubernetes probes and health checks
Published: 6/11/2023
Tags: 
- docker desktop
- docker
- minikube
- kubectl
- kubernetes

---

# Introduction

In Kubernetes, probes and health checks are mechanisms used to determine the availability and readiness of applications running within the cluster. They are essential for ensuring the stability and reliability of the deployed services. Let's explore each concept:

## 1. Probes:
Probes are diagnostics performed by Kubernetes to assess the health of a container. They can be of three types:
1. Liveness Probe: It indicates whether the container is running properly. If the liveness probe fails, Kubernetes considers the container unhealthy and attempts to restart it.
2. Readiness Probe: It determines whether the container is ready to receive traffic. If the readiness probe fails, the container is excluded from the load balancer or service endpoints until it becomes ready again.
3. Startup Probe: Introduced in Kubernetes 1.16, this probe checks if an application within the container has started successfully. It is mainly used during the initial startup period of an application.

Probes can be defined in the pod specification using the `livenessProbe`, `readinessProbe`, and `startupProbe` fields. Each probe has configurable parameters such as the type of probe (HTTP, TCP, or Exec), the path, port, and timeout. Kubernetes periodically executes the probes based on the specified settings and evaluates the results.

## 2. Health Checks:
Health checks are mechanisms used to monitor the overall health and performance of a Kubernetes cluster. They ensure that the cluster components, including the control plane and worker nodes, are functioning correctly. Health checks can be classified into two categories:

1. Node Health Checks: These checks verify the health of individual nodes in the cluster. Kubernetes uses various techniques like node liveness and node readiness probes to determine the status of each node.
2. Cluster Health Checks: These checks assess the overall health and availability of the Kubernetes cluster. They involve verifying the status of critical components like the API server, etcd, controller manager, and scheduler.

Kubernetes provides built-in health check mechanisms for cluster components, and it's also possible to implement custom health checks using tools like Prometheus, Grafana, or other monitoring solutions.

By combining probes and health checks, Kubernetes ensures that applications and the underlying cluster infrastructure are in a healthy state, providing better reliability and resilience.

# Example

For the example I will specifically looking at setting up a pod with `livenessProbe` and  `readinessProbe`. As I am predominately a dotnet engineer I have made use of a custom docker image which implements health checks in a dotnet minimal API. You can checkout the source code of this application at [https://github.com/reggieray/dotnet-health-checks](https://github.com/reggieray/dotnet-health-checks) and you can read how health checks work in dotnet in this [Dotnet health checks](https://matthewregis.dev/posts/dotnet-health-checks) blog post.

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

## 1. Create pod

Create a `pod.yaml` file with the following contents. As mentioned previously it makes use of a custom build image that I created specifically for demonstrating health checks. The link to the source code is put above.

I have defined a `livenessProbe` and `readinessProbe`, also pay attention to the environment variables, as these can be set to give a healthy or unhealthy results depending on what value is set. To start off with I have set them to values that results in both returning healthy results. 

```yaml
apiVersion: v1
kind: Pod
metadata:
  labels:
    app: healthz
  name: healthz
  namespace: default
spec:
  containers:
  - image: matthewregis/public:dotnet-health-checks
    imagePullPolicy: IfNotPresent
    name: healthz
    ports:
    - containerPort: 80
    livenessProbe:
      httpGet:
        path: /healthz/live
        port: 80
      initialDelaySeconds: 15
      periodSeconds: 20
    readinessProbe:
      httpGet:
        path: /healthz/ready
        port: 80
      initialDelaySeconds: 5
      periodSeconds: 10
    env:
    - name: HealthCheck__MyCustomStartUpHealthCheck
      value: "True"
    - name: HealthCheck__UriCheck
      value: "https://matthewregis.dev"
    resources: {}
```

Apply the `pod.yaml`.

```bash
> k apply -f pod.yaml
pod/healthz created
```

## 2. Create service

Create a service that exposes the pod so we can manually test the API and it's endpoints. Create `service.yaml` file with following:

```yaml
apiVersion: v1
kind: Service
metadata:
  name: healthz
spec:
  selector:
    app: healthz
  ports:
  - name: http
    port: 80
    targetPort: 80
  type: NodePort
```

Then apply the service

```bash
> k apply -f service.yaml
service/healthz created
```

## 3. Verify successfully running behavior

Create a tunnel with minikube

```bash
> minikube service healthz 
|-----------|---------|-------------|---------------------------|
| NAMESPACE |  NAME   | TARGET PORT |            URL            |
|-----------|---------|-------------|---------------------------|
| default   | healthz | http/80     | http://192.168.49.2:30904 |
|-----------|---------|-------------|---------------------------|
🏃  Starting tunnel for service healthz.
|-----------|---------|-------------|------------------------|
| NAMESPACE |  NAME   | TARGET PORT |          URL           |
|-----------|---------|-------------|------------------------|
| default   | healthz |             | http://127.0.0.1:52625 |
|-----------|---------|-------------|------------------------|
🎉  Opening service default/healthz in default browser...
❗  Because you are using a Docker driver on windows, the terminal needs to be open to run it.
```

This opened http://127.0.0.1:52625/ for me, to test the endpoints I updated the url to:

- http://127.0.0.1:52625/weatherforecast
- http://127.0.0.1:52625/healthz/all
- http://127.0.0.1:52625/healthz/ready
- http://127.0.0.1:52625/healthz/live

All the endpoints worked as expected. 

You can also verify the pod is working as expected by looking at the pod

```bash
> k logs healthz
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:80
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
info: System.Net.Http.HttpClient.UriCheck.LogicalHandler[100]
      Start processing HTTP request GET https://matthewregis.dev/
### output shortened for brevity ### 

> k describe pod healthz
### output shortened for brevity ### 
Events:
  Type    Reason     Age    From               Message
  ----    ------     ----   ----               -------
  Normal  Scheduled  7m52s  default-scheduler  Successfully assigned default/healthz to minikube
  Normal  Pulled     7m52s  kubelet            Container image "matthewregis/public:dotnet-health-checks" already present on machine
  Normal  Created    7m52s  kubelet            Created container healthz
  Normal  Started    7m51s  kubelet            Started container healthz

### if you have minikube dashboard, you can also use this to verify ### 
> minikube dashboard
``` 

## 4. Update liveness probe to fail

Remove the pod

```bash
> k delete pod healthz
pod "healthz" deleted
```

Update the following section of the pod definition with a website that would fail. In my case I removed the `.dev` at the end.

```yaml
- name: HealthCheck__UriCheck
      value: "https://matthewregis"
```
Re-apply the pod

```bash
> k apply -f pod.yaml
pod/healthz created
```

## 5. Verify unhealthy livenessProbe behavior

Now you should see that because the `livenessProbe` returns a unhealthy result, Kubernetes continually restarts the pod to try and get it back into a healthy state.

```bash
> k describe pod healthz
### output shortened for brevity ###
Events:
  Type     Reason     Age                  From               Message
  ----     ------     ----                 ----               -------
  Normal   Scheduled  2m54s                default-scheduler  Successfully assigned default/healthz to minikube
  Normal   Pulled     54s (x3 over 2m54s)  kubelet            Container image "matthewregis/public:dotnet-health-checks" already present on machine
  Normal   Killing    54s (x2 over 114s)   kubelet            Container healthz failed liveness probe, will be restarted
  Normal   Created    53s (x3 over 2m54s)  kubelet            Created container healthz
  Normal   Started    53s (x3 over 2m54s)  kubelet            Started container healthz
  Warning  Unhealthy  14s (x8 over 2m34s)  kubelet            Liveness probe failed: Get "http://10.244.0.153:80/healthz/live": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
```

## 6. Clean up.

```bash
> k delete pod healthz
pod "healthz" deleted

> k delete svc healthz
service "healthz" deleted
```

# Further reading

- [Configure Liveness, Readiness and Startup Probes](https://kubernetes.io/docs/tasks/configure-pod-container/configure-liveness-readiness-startup-probes/)
- [Probe v1 core](https://kubernetes.io/docs/reference/generated/kubernetes-api/v1.26/#probe-v1-core)
- [Dotnet health checks blog post](https://matthewregis.dev/posts/dotnet-health-checks)