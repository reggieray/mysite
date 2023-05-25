Title: Blue Green deployment
Published: 4/4/2023
Tags: 
- docker desktop
- docker
- minikube
- kubectl
- kubernetes

---

# Introduction

Kubernetes has different deployment strategies you can use to deploy your applications/services with [rolling update](https://kubernetes.io/docs/tutorials/kubernetes-basics/update/update-intro/) and [canary deployments](https://kubernetes.io/docs/concepts/cluster-administration/manage-deployment/#canary-deployments) among the options. Blue-green deployments is just one of many deployment strategies. 

Blue-green deployments offer an effective strategy to achieve seamless updates in Kubernetes. This approach allows you to roll out new versions of your application without any downtime, minimizing the impact on your users.

Understanding Blue-Green Deployments:
Blue-green deployments involve maintaining two identical production environments, known as the blue environment and the green environment. The blue environment represents the current stable version of your application, while the green environment represents the new version being deployed. The deployment process involves gradually shifting traffic from the blue environment to the green environment, ensuring a smooth transition.

## Benefits of Blue-Green Deployments:

- Zero Downtime: By maintaining separate environments, blue-green deployments eliminate downtime during updates. Users can seamlessly access the application without experiencing service interruptions.
- Rollback Capability: If any issues arise during the green environment's deployment, it is easy to roll back to the stable blue environment without affecting users' experience.
- Testing in Production: Blue-green deployments enable comprehensive testing of new versions in a production-like environment. This approach provides valuable insights and helps identify issues that may not be apparent in staging or development environments.
- Improved Resilience: Having two separate environments ensures redundancy and fault tolerance. If an issue occurs in the green environment, the blue environment remains unaffected, ensuring the availability of your application.

# Example implementation

## Prerequisites

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

## Example scenario

The following describes the scenario used to try and explain a blue-green deployment: 
1. Blue deployment is running and exposed via the service.
2. Green deployment is also running but not exposed. In theory you would run tests and verify this is working as expected before proceeding.
3. Update service to point to the green deployment.

## Implementation

> **_NOTE:_** The deployments use a custom image that displays the environment variables, this image is used for demo purposes only.

1. Create a file `blue-green-deployment.yaml` with the following contents, which comprises of a blue deployment, green deployment and service.

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: blue-app
  labels:
    app: my-app
spec:
  selector:
    matchLabels:
      version: v1
  template:
    metadata:
      name: my-app
      labels:
        version: v1
    spec:
      containers:
      - name: my-app-container
        image: matthewregis/public:dotnet-display-env-var
        ports:
        - containerPort: 80
        env:
        - name: DEPLOYMENT_EXAMPLE
          value: "Blue"
  replicas: 1

---

apiVersion: apps/v1
kind: Deployment
metadata:
  name: green-app
  labels:
    app: my-app
spec:
  selector:
    matchLabels:
      version: v2
  template:
    metadata:
      name: my-app
      labels:
        version: v2
    spec:
      containers:
      - name: my-app-container
        image: matthewregis/public:dotnet-display-env-var
        ports:
        - containerPort: 80
        env:
        - name: DEPLOYMENT_EXAMPLE
          value: "Green"
  replicas: 1

---

apiVersion: v1
kind: Service
metadata:
  name: my-app-service
spec:
  selector:
    version: v1 # notice this is pointing to version: v1 which is the blue deployment to start off with.
  ports:
    - port: 80
      targetPort: 80
  type: NodePort
```

2. Apply the deployments and service to minikube 

```bash
k apply -f .\blue-green-deployment.yaml 
```

3. Verify the current running service by browsing to it using the following command. You should see that the environment variable DEPLOYMENT_EXAMPLE is set to Blue. Press `Ctrl+C` once finished.
```bash
minikube service my-app-service
```

4. Update the service to point to v2 by updating `version: v1` to `version: v2`. In theory you would have verified that the green deployment is working as expected before proceeding. 
```bash
k edit service my-app-service
```

5. Verify the current running service again with the same command as step 3. You should see that the environment variable DEPLOYMENT_EXAMPLE is set to Green. 
```bash
minikube service my-app-service
```

## Clean-up

You can clear your minikube environment by deleting the created deployments and service using the following commands: 
```bash
k delete service my-app-service
k delete deployment blue-app
k delete deployment green-app
```

# Further reading
- [Deployments](https://kubernetes.io/docs/concepts/workloads/controllers/deployment/) - Deployments documentation on kubernetes.io
- [Blue/green Deployment](https://kubernetes.io/blog/2018/04/30/zero-downtime-deployment-kubernetes-jenkins/#blue-green-deployment) - Blog on Blue/Green deployments on kubernetes.io