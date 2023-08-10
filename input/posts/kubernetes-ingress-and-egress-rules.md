Title: Kubernetes ingress and egress rules
Published: 7/12/2023
Tags: 
- docker desktop
- docker
- minikube
- kubectl
- kubernetes

---

# Introduction

### Ingress:
In Kubernetes, an Ingress is an API object that manages external access to services within a cluster. It acts as a traffic router, allowing you to define rules for how incoming requests should be directed to different services based on hostnames, paths, or other criteria. Ingress resources provide a way to configure HTTP and HTTPS routing for your applications without requiring changes to the application code. Essentially, an Ingress acts as a way to expose HTTP and HTTPS routes from outside the cluster to services within the cluster.

An Ingress typically works with an Ingress Controller, which is responsible for fulfilling the rules defined in the Ingress resource. Examples of Ingress Controllers include Nginx Ingress Controller and Traefik.

### Egress:
Egress, on the other hand, refers to outbound network traffic from within the Kubernetes cluster to external destinations, such as services or resources outside the cluster. Egress traffic originates from your applications running in the cluster and is directed towards external servers, APIs, or other services. Egress is managed by the networking setup of the cluster and is subject to various networking policies and security configurations.

Managing egress traffic is important to ensure that your applications can access external resources securely and efficiently while adhering to any network security policies you have in place.

# Example

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

In the example I will mainly be looking at creating a ingress rule within Minikube.

1. Enable Ingress

Make sure ingress is enabled on Minikube

```bash
> minikube addons enable ingress
💡  ingress is an addon maintained by Kubernetes. For any concerns contact minikube on GitHub.
You can view the list of minikube maintainers at: https://github.com/kubernetes/minikube/blob/master/OWNERS
💡  After the addon is enabled, please run "minikube tunnel" and your ingress resources would be available at "127.0.0.1"
    ▪ Using image registry.k8s.io/ingress-nginx/controller:v1.7.0
    ▪ Using image registry.k8s.io/ingress-nginx/kube-webhook-certgen:v20230312-helm-chart-4.5.2-28-g66a760794
    ▪ Using image registry.k8s.io/ingress-nginx/kube-webhook-certgen:v20230312-helm-chart-4.5.2-28-g66a760794
🔎  Verifying ingress addon...
🌟  The 'ingress' addon is enabled
```
Verify ingress is enabled

```bash
> minikube addons list
|-----------------------------|----------|--------------|--------------------------------|
|         ADDON NAME          | PROFILE  |    STATUS    |           MAINTAINER           |
|-----------------------------|----------|--------------|--------------------------------|
| ambassador                  | minikube | disabled     | 3rd party (Ambassador)         |
| auto-pause                  | minikube | disabled     | Google                         |
| cloud-spanner               | minikube | disabled     | Google                         |
| csi-hostpath-driver         | minikube | disabled     | Kubernetes                     |
| dashboard                   | minikube | enabled ✅   | Kubernetes                     |
| default-storageclass        | minikube | enabled ✅   | Kubernetes                     |
| efk                         | minikube | disabled     | 3rd party (Elastic)            |
| freshpod                    | minikube | disabled     | Google                         |
| gcp-auth                    | minikube | disabled     | Google                         |
| gvisor                      | minikube | disabled     | Google                         |
| headlamp                    | minikube | disabled     | 3rd party (kinvolk.io)         |
| helm-tiller                 | minikube | disabled     | 3rd party (Helm)               |
| inaccel                     | minikube | disabled     | 3rd party (InAccel             |
|                             |          |              | [info@inaccel.com])            |
| ingress                     | minikube | enabled ✅   | Kubernetes                     |
| ingress-dns                 | minikube | disabled     | Google                         |
### output shortened for brevity ### 
```

2. Create Deployment

Create a deployment that will create a pod with `gcr.io/google-samples/hello-app:1.0`.
```bash
> k create deployment hello-server --image=gcr.io/google-samples/hello-app:1.0
deployment.apps/hello-server created

> k get deployment
NAME           READY   UP-TO-DATE   AVAILABLE   AGE
hello-server   1/1     1            1           12s
```
3. Expose Deployment

Create a service that exposes the deployment.
```bash
> k expose deployment hello-server --type=NodePort --port=8080
service/hello-server exposed

> k get svc
NAME             TYPE        CLUSTER-IP       EXTERNAL-IP   PORT(S)          AGE
hello-minikube   NodePort    10.98.61.57      <none>        8080:31847/TCP   121d
hello-server     NodePort    10.105.187.213   <none>        8080:30700/TCP   40s
kubernetes       ClusterIP   10.96.0.1        <none>        443/TCP
```
4. Create Ingress Rule

Create a file `ingress.yaml` with the following contents:
```yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: example-ingress
  annotations:
    ingress.kubernetes.io/rewrite-target: /
spec:
  rules:
  - host: hello-server.local
    http:
      paths:
        - path: /
          pathType: Prefix
          backend:
            service:
              name: hello-server
              port:
                number: 8080
```
and apply this to minikube

```bash
> k apply -f .\ingress.yaml
ingress.networking.k8s.io/example-ingress created
```

5. Verify Ingress Rule

To do this I will create a temporary pod that I will use to run `curl hello-server.local`, which should respond with the response from the deployment if successful.

```bash
> k run my-shell --rm -i --tty --image nginx -- /bin/sh

# curl hello-server.local
Hello, world!
Version: 1.0.0
Hostname: hello-server-6d889c497f-c2v2w
# exit
Session ended, resume using 'kubectl attach my-shell -c my-shell -i -t' command when the pod is running
pod "my-shell" deleted
```

6. Clean Up

```bash
> k delete deployment hello-server
deployment.apps "hello-server" deleted

> k delete ingress example-ingress
ingress.networking.k8s.io "example-ingress" deleted

> k delete svc hello-server
service "hello-server" deleted
```

# Further reading
- [Services, Load Balancing, and Networking](https://kubernetes.io/docs/concepts/services-networking/)
- [Ingress](https://kubernetes.io/docs/concepts/services-networking/ingress/)
- [Network Policies](https://kubernetes.io/docs/concepts/services-networking/network-policies/)