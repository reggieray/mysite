Title: Kubernetes resource requirements limits and quotas  
Published: 6/25/2023
Tags: 
- docker desktop
- docker
- minikube
- kubectl
- kubernetes

---

# Introduction

In Kubernetes, you can define resource requirements, limits, and quotas to manage and control the allocation of resources within a cluster. These mechanisms allow you to set constraints on CPU, memory, and other resources, ensuring fair resource distribution and preventing resource abuse.

Here's an overview of how you can define resource requirements, limits, and quotas in Kubernetes:

1. Resource Requests: Resource requests define the minimum amount of a resource (e.g., CPU or memory) that a container or pod requires to run. It helps Kubernetes schedule pods onto suitable nodes that can meet the requested resource levels. Requests are specified in the pod or container configuration using the resources.requests field.

2. Resource Limits: Resource limits specify the maximum amount of a resource that a container or pod is allowed to consume. It sets an upper boundary to prevent containers from using excessive resources and affecting the stability and performance of the cluster. Limits are defined using the resources.limits field in the pod or container configuration.

3. Quality of Service (QoS): Kubernetes defines three QoS classes for pods based on their resource requests and limits:

    - Guaranteed: Pods with both requests and limits set and they are equal. These pods are assured to have the requested resources available.
    - Burstable: Pods with requests and limits set but they differ. These pods are given burstable access to resources but are not guaranteed to always have the full limit available.
    - BestEffort: Pods without any resource requests or limits. These pods are not allocated any specific resources and can use whatever is available in the cluster.

4. Resource Quotas: Resource quotas provide limits on the aggregate resource usage for a namespace. Quotas can be set for CPU, memory, storage, and other resources. They define the maximum amount of resources that can be consumed by all pods and containers within a namespace. Quotas are configured using the ResourceQuota API object.

5. LimitRanges: LimitRanges allow you to define constraints on the resource requests and limits at the namespace level. It enables you to specify minimum and maximum resource limits for different resources, ensuring that pods and containers stay within predefined boundaries. LimitRanges are set using the LimitRange API object.

By defining resource requirements, limits, and quotas, you can effectively manage resource allocation, prevent resource contention, and ensure fair distribution of resources among pods and containers in your Kubernetes cluster. These mechanisms help maintain stability, performance, and efficient resource utilization.

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

In this example I'll be creating a namespace with a resource quota and then creating a pod in that namespace.

1. Create namespace and apply resource quota

```ps
> k create ns my-ns
```
Then apply this example resource quota `quota-mem-cpu.yaml` from the kubernetes.io website. You should also find it at https://k8s.io/examples/admin/resource/quota-mem-cpu.yaml

```yaml
apiVersion: v1
kind: ResourceQuota
metadata:
  name: mem-cpu-demo
spec:
  hard:
    requests.cpu: "1"
    requests.memory: 1Gi
    limits.cpu: "2"
    limits.memory: 2Gi
```
Apply the resource quota.

```ps
> k apply -f https://k8s.io/examples/admin/resource/quota-mem-cpu.yaml -n=my-ns
resourcequota/mem-cpu-demo created
```

2. Create a pod definition `resource-example.yaml` that we can edit later.

```ps
> k run resource-example --image=nginx --dry-run=client -oyaml > resource-example.yaml
```

3. Update the pod definition

From something like this 

```yaml
apiVersion: v1
kind: Pod
metadata:
  creationTimestamp: null
  labels:
    run: resource-example
  name: resource-example
spec:
  containers:
  - image: nginx
    name: resource-example
    resources: {}
  dnsPolicy: ClusterFirst
  restartPolicy: Always
status: {}
```

To this. Notice the addition of the `resources` section populated.

```yaml
apiVersion: v1
kind: Pod
metadata:
  creationTimestamp: null
  labels:
    run: resource-example
  name: resource-example
spec:
  containers:
  - image: nginx
    name: resource-example
    resources:
      limits:
        cpu: 200m
        memory: 512Mi
      requests:
        cpu: 100m
        memory: 256Mi
  dnsPolicy: ClusterFirst
  restartPolicy: Always
status: {}
```

4. Apply the pod definition

```bash
> k apply -f .\resource-example.yaml -n=my-ns
pod/resource-example created
```

5. Verify the pod

If you have enough resources the pod should start without any issues. Also use `describe` to output the pod was created in the desired state.

```bash
> k get po resource-example -n=my-ns
NAME               READY   STATUS    RESTARTS   AGE
resource-example   1/1     Running   0          18s

> k describe po resource-example -n=my-ns
Name:             resource-example
Namespace:        default
Priority:         0
Service Account:  default
Node:             minikube/192.168.49.2
Start Time:       Sun, 25 Jun 2023 11:23:49 +0100
Labels:           run=resource-example
Annotations:      <none>
Status:           Running
IP:               10.244.0.180
IPs:
  IP:  10.244.0.180
Containers:
  resource-example:
    Container ID:   docker://cf196535398cf50a14ad29466fa133927c5964dc97b23a92e96b78df44e88ce4
    Image:          nginx
    Image ID:       docker-pullable://nginx@sha256:08bc36ad52474e528cc1ea3426b5e3f4bad8a130318e3140d6cfe29c8892c7ef
    Port:           <none>
    Host Port:      <none>
    State:          Running
      Started:      Sun, 25 Jun 2023 11:24:03 +0100
    Ready:          True
    Restart Count:  0
    Limits:
      cpu:     200m
      memory:  512Mi
    Requests:
      cpu:        100m
      memory:     256Mi
    Environment:  <none>
### output shortened for brevity ### 
```

Also verify the resource quota is being used, now the pod in running we should see that resources are being used.

```bash
> k get resourcequota mem-cpu-demo --namespace=my-ns
NAME           AGE     REQUEST                                            LIMIT
mem-cpu-demo   2m28s   requests.cpu: 100m/1, requests.memory: 256Mi/1Gi   limits.cpu: 200m/2, limits.memory: 512Mi/2Gi
```

6. Clean up

```bash
> k delete po resource-example -n=my-ns
pod "resource-example" deleted

> k delete ns my-ns
namespace "my-ns" deleted
```

# Further reading

- [Resource Management for Pods and Containers](https://kubernetes.io/docs/concepts/configuration/manage-resources-containers/)
- [Configure Default Memory Requests and Limits for a Namespace](https://kubernetes.io/docs/tasks/administer-cluster/manage-resources/memory-default-namespace/)
- [Resource Quotas](https://kubernetes.io/docs/concepts/policy/resource-quotas/)