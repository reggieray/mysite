Title: Kubernetes secrets
Published: 6/18/2023
Tags: 
- docker desktop
- docker
- minikube
- kubectl
- kubernetes

---

# Introduction

In Kubernetes, Secrets are similar to ConfigMaps but specifically designed for storing and managing sensitive information such as passwords, API keys, tokens, or any other confidential data that should not be exposed in plaintext.

Like ConfigMaps, Secrets are created using YAML or JSON manifests and can be managed through the Kubernetes API or tools like kubectl. However, Secrets have an additional layer of security and are encoded in Base64 format when stored in the cluster.

Secrets are primarily used to provide sensitive data to applications running in containers, ensuring that the data remains secure and protected from unauthorized access. They can be mounted as volumes or exposed as environment variables in the application containers, allowing them to access the secret data during runtime.

Compared to ConfigMaps, Secrets have a few key differences:

1. Data Encryption: Secrets are automatically encrypted at rest when stored in etcd (the key-value store used by Kubernetes), providing an extra layer of security for sensitive information.
2. Base64 Encoding: Secret data is stored in Base64 encoding, which provides a basic level of obfuscation. However, it's important to note that Base64 encoding is not equivalent to encryption, and it can be easily decoded if the secret is compromised.
3. Access Control: Secrets can be tightly controlled using Kubernetes RBAC (Role-Based Access Control) mechanisms, allowing fine-grained access control to ensure that only authorized users or containers can access the sensitive data.

It's crucial to follow security best practices when using Secrets, such as limiting access, rotating the secrets regularly, and using encryption mechanisms (like TLS) for transport-level security.

> **_NOTE:_** I wouldn't recommend using Kubernetes secrets for real world applications due to the limitations such as lack of encryption. Instead I would research the best approach for your use-case. For example if you are already tied into a cloud providers ecosystem such as Azure or AWS then [Microsoft Azure Key Vault](https://azure.microsoft.com/en-us/products/key-vault/) or [AWS Secrets Manager](https://aws.amazon.com/secrets-manager/) would be worth investigating. 

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

1. Create secret

```bash
> k create secret generic db-user-pass --from-literal=username=admin --from-literal=password='S!B\*d$zDsb='
secret/db-user-pass created
```

2. Verify secret

```ps
> k get secrets
NAME           TYPE     DATA   AGE
db-user-pass   Opaque   2      115s
```

3. Consume secret

Create a deployment and service definition using the following content for a `secret-deploy.yaml` file.

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
        - name: username
          valueFrom: 
            secretKeyRef: 
              key: username
              name: db-user-pass
        - name: password
          valueFrom: 
            secretKeyRef: 
              key: password
              name: db-user-pass
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
> k apply -f .\secret-deploy.yaml
deployment.apps/my-app-deployment created
service/my-app-service created
```

4. Verify consumed secret

```ps
> minikube service my-app-service
|-----------|----------------|-------------|---------------------------|
| NAMESPACE |      NAME      | TARGET PORT |            URL            |
|-----------|----------------|-------------|---------------------------|
| default   | my-app-service |          80 | http://192.168.49.2:30244 |
|-----------|----------------|-------------|---------------------------|
🏃  Starting tunnel for service my-app-service.
|-----------|----------------|-------------|------------------------|
| NAMESPACE |      NAME      | TARGET PORT |          URL           |
|-----------|----------------|-------------|------------------------|
| default   | my-app-service |             | http://127.0.0.1:55096 |
|-----------|----------------|-------------|------------------------|
🎉  Opening service default/my-app-service in default browser...
❗  Because you are using a Docker driver on windows, the terminal needs to be open to run it.
✋  Stopping tunnel for service my-app-service.
```

This opened a web browser and I was able to verify the username and password as environment variables were set and they also have the value I had set originally in step 1.  

5. Clean up

```ps
> k delete service my-app-service
service "my-app-service" deleted

> k delete deployment my-app-deployment
deployment.apps "my-app-deployment" deleted

> k delete secret db-user-pass
secret "db-user-pass" deleted
```

# Further reading

- [Secrets](https://kubernetes.io/docs/concepts/configuration/secret/)
- [Managing Secrets using kubectl](https://kubernetes.io/docs/tasks/configmap-secret/managing-secret-using-kubectl/)