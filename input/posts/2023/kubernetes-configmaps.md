Title: Kubernetes configmaps
Published: 6/19/2023
Tags: 
- docker desktop
- docker
- minikube
- kubectl
- kubernetes

---

# Introduction

In Kubernetes, ConfigMaps are objects that allow you to decouple configuration data from the container images. They provide a way to store and manage non-confidential configuration data that can be consumed by your application containers running in a Kubernetes cluster.

ConfigMaps are commonly used to store configuration files, command-line arguments, environment variables, or any other configuration data that your application requires. By using ConfigMaps, you can separate the configuration concerns from the application code, making it easier to manage and update configurations without modifying and redeploying the container images.

ConfigMaps are created using YAML or JSON manifests and can be created directly through the Kubernetes API or using configuration management tools like kubectl or deployment scripts.

Once created, ConfigMaps can be mounted as volumes or exposed as environment variables in your application containers. This allows the containers to access the configuration data stored in the ConfigMap and use it during runtime.

ConfigMaps provide a flexible and scalable way to manage configurations in Kubernetes, making it easier to maintain consistency across multiple environments and deployments. They can be updated and reloaded without restarting the containers, which helps in achieving a more dynamic and agile configuration management approach.

Overall, ConfigMaps in Kubernetes are a powerful mechanism for managing and providing configuration data to your application containers, enabling better separation of concerns and more efficient configuration management in a Kubernetes cluster.

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

1. Create config map

```bash
> k create configmap my-config --from-literal=myconfig.url=https://matthewregis.dev --from-literal=myconfig.contactPath=/contact
configmap/my-config created
```

2. Verify config map

```ps
> k get configmap
NAME               DATA   AGE
kube-root-ca.crt   1      79d
my-config          2      15s

> k describe configmap my-config
Name:         my-config
Namespace:    default
Labels:       <none>
Annotations:  <none>

Data
====
myconfig.url:
----
https://matthewregis.dev
myconfig.contactPath:
----
/contact

BinaryData
====

Events:  <none>
```

3. Consume config map

Create a deployment and service definition using the following content for a `config-map-deploy.yaml` file.

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: my-app-deployment
  labels:
    app: my-app
spec:
  selector:
    matchLabels:
      app: my-app
  template:
    metadata:
      name: my-app
      labels:
        app: my-app
    spec:
      containers:
      - name: my-app-container
        image: matthewregis/public:dotnet-display-env-var
        ports:
        - containerPort: 80
        env:
        - name: configUrl
          valueFrom: 
            configMapKeyRef: 
              key: myconfig.url
              name: my-config
        - name: configContactPath
          valueFrom: 
            configMapKeyRef: 
              key: myconfig.contactPath
              name: my-config
  replicas: 1

---

apiVersion: v1
kind: Service
metadata:
  name: my-app-service
spec:
  selector:
    app: my-app
  ports:
    - port: 80
      targetPort: 80
  type: NodePort
```

Apply deployment and service

```ps
> k apply -f .\config-map-deploy.yaml
deployment.apps/my-app-deployment created
service/my-app-service created
```

4. Verify consumed config map

```ps
> minikube service my-app-service
|-----------|----------------|-------------|---------------------------|
| NAMESPACE |      NAME      | TARGET PORT |            URL            |
|-----------|----------------|-------------|---------------------------|
| default   | my-app-service |          80 | http://192.168.49.2:32758 |
|-----------|----------------|-------------|---------------------------|
🏃  Starting tunnel for service my-app-service.
|-----------|----------------|-------------|------------------------|
| NAMESPACE |      NAME      | TARGET PORT |          URL           |
|-----------|----------------|-------------|------------------------|
| default   | my-app-service |             | http://127.0.0.1:56507 |
|-----------|----------------|-------------|------------------------|
🎉  Opening service default/my-app-service in default browser...
❗  Because you are using a Docker driver on windows, the terminal needs to be open to run it.

```

This opened a web browser and I was able to verify the `configUrl` and `configContactPath` as environment variables were set correctly and they had the values I had set originally in step 1.  

5. Clean up

```ps
> k delete service my-app-service
service "my-app-service" deleted

> k delete deployment my-app-deployment
deployment.apps "my-app-deployment" deleted

> k delete configmap my-config
configmap "my-config" deleted
```

# Further reading

- [ConfigMaps](https://kubernetes.io/docs/concepts/configuration/configmap/)
- [Configure a Pod to Use a ConfigMap](https://kubernetes.io/docs/tasks/configure-pod-container/configure-pod-configmap/)