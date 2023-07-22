Title: Kubernetes service accounts
Published: 6/24/2023
Tags: 
- docker desktop
- docker
- minikube
- kubectl
- kubernetes

---

# Introduction

In Kubernetes, service accounts are a mechanism used to provide an identity and credentials to pods running within a cluster. They enable pod-to-pod and pod-to-cluster communication by allowing the pods to authenticate and authorize their requests to other resources within the cluster.

Here are some key points about service accounts in Kubernetes:

1. Identity: Service accounts are associated with pods and provide an identity for authentication purposes. Each pod can be assigned a unique service account, allowing it to act on behalf of that account.

2. Credentials: Service accounts are assigned a token that serves as their authentication credentials. This token is mounted into the pod's filesystem, typically in the /var/run/secrets/kubernetes.io/serviceaccount/ directory.

3. Authorization: Service accounts are bound to Kubernetes roles or cluster roles that define the set of permissions they have within the cluster. These roles control the operations that a pod can perform on various resources, such as creating, reading, updating, or deleting objects.

4. Default Service Account: Every Kubernetes namespace has a default service account automatically created for it. If a pod is created without explicitly specifying a service account, it is assigned the default service account of its namespace.

5. Communication: Service accounts are primarily used for communication between pods and other Kubernetes resources, such as the API server, other pods, or external services. When making requests, the pod uses its assigned service account's credentials to authenticate itself.

6. Secrets and Tokens: The service account token, along with other credentials and secrets, are stored in Kubernetes Secrets. These secrets are accessible within the pod's filesystem, allowing the pod to retrieve the necessary information for authentication.

By leveraging service accounts, pods can interact securely with other resources in the cluster, ensuring proper authentication and authorization. This helps maintain the security and integrity of the Kubernetes environment.

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

1. Create namespace to run example in. 

```ps
> k create ns my-ns
namespace/my-ns created
```

2. Create service account

```ps
k create serviceaccount my-sa -n=my-ns
serviceaccount/my-sa created
```

3. Create pod definition 

```ps
k run my-pod --image=nginx -n=my-ns --dry-run=client -oyaml > pod-with-service-account.yaml
```
Output should be like the following:

```yaml
apiVersion: v1
kind: Pod
metadata:
  creationTimestamp: null
  labels:
    run: my-pod
  name: my-pod
  namespace: my-ns 
spec:
  containers:
  - image: nginx
    name: my-pod
    resources: {}
  serviceAccountName: my-sa # add this line 
  dnsPolicy: ClusterFirst
  restartPolicy: Always
status: {}
```
Apply the pod definition

```ps
> k apply -f .\pod-with-service-account.yaml
pod/my-pod created
```

4. Verify pod with service account

```ps
> k get po my-pod -n=my-ns
NAME     READY   STATUS    RESTARTS   AGE
my-pod   1/1     Running   0          3m33s

> k describe po my-pod -n=my-ns
Name:             my-pod
Namespace:        my-ns
Priority:         0
Service Account:  my-sa
### output shortened for brevity ### 
```
5. Clean up

```ps
> k delete po my-pod -n=my-ns
pod "my-pod" deleted

> k delete ns my-ns
namespace "my-ns" deleted
```

# Further reading

- [Service accounts](https://kubernetes.io/docs/concepts/security/service-accounts/)
- [Configure Service Accounts for Pods](https://kubernetes.io/docs/tasks/configure-pod-container/configure-service-account/)