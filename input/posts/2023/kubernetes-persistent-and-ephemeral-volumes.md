Title: Kubernetes persistent and ephemeral volumes
Published: 6/3/2023
Tags: 
- kubernetes
- kubectl
- minikube
- docker
- docker desktop

---

# What are persistent and ephemeral volumes in Kubernetes? 

## Persistent Volumes (PVs):
Persistent Volumes are Kubernetes resources that provide storage for stateful applications. They are independent of any specific Pod and have a longer lifespan than Pods. PVs are provisioned by administrators and can be dynamically or statically provisioned.

Key characteristics of Persistent Volumes:

- They have a lifecycle independent of Pods, which means they persist data even if the Pod using them is terminated or deleted.
- They are bound to a specific storage backend, such as network-attached storage (NAS), block devices, or cloud storage.
- PVs can be dynamically provisioned by using Storage Classes, which define the provisioning mechanism and characteristics.
- PVs can be manually claimed by Persistent Volume Claims (PVCs) in Pods, allowing Pods to request and use the persistent storage.


## Ephemeral Volumes:

Ephemeral Volumes, also known as emptyDir volumes, are temporary storage volumes created by Kubernetes for individual Pods. They have a shorter lifespan and are typically used for temporary or transient data within a Pod.

Key characteristics of Ephemeral Volumes:

- Ephemeral Volumes are created and deleted along with the lifecycle of the Pod that uses them.
- They are ideal for storing non-persistent and short-lived data, such as caching, temporary files, or inter-pod communication.
- Ephemeral Volumes do not require manual provisioning or configuration.
- They are mounted into a specific Pod and are not accessible by other Pods.

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

### Persistent Volumes (PVs):

In this example a persistent volume (PV) will be created and then a persistent volume claim (PVC) will be created for that PV and then finally a pod will be created using that PVC. We will prepare the volume to contain a html file which will be used to verify that the PV has been mounted correctly.

1. Prepare the node on minikube

```ps
> minikube ssh
Last login: Tue Jun  3 22:27:57 2023 from 192.168.49.1
docker@minikube:~$

--
> docker@minikube:~$ sudo mkdir /mnt/data

--
> docker@minikube:~$ sudo sh -c "echo 'Hello from Kubernetes storage' > /mnt/data/index.html"

--
> docker@minikube:~$ cat /mnt/data/index.html
Hello from Kubernetes storage

--
> docker@minikube:~$ exit
```

2. For brevity the PV, PVC and pod will be created in one file. Create a file `pv-pvc-example.yaml` with the contents below.

```yaml
apiVersion: v1
kind: PersistentVolume
metadata:
  name: task-pv-volume
  labels:
    type: local
spec:
  storageClassName: manual
  capacity:
    storage: 3Gi
  accessModes:
    - ReadWriteOnce
  hostPath:
    path: "/mnt/data"

---

apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: task-pv-claim
spec:
  storageClassName: manual
  accessModes:
    - ReadWriteOnce
  resources:
    requests:
      storage: 1Gi

---

apiVersion: v1
kind: Pod
metadata:
  name: task-pv-pod
spec:
  volumes:
    - name: task-pv-storage
      persistentVolumeClaim:
        claimName: task-pv-claim
  containers:
    - name: task-pv-container
      image: nginx
      ports:
        - containerPort: 80
          name: "http-server"
      volumeMounts:
        - mountPath: "/usr/share/nginx/html"
          name: task-pv-storage
```

3. Apply the file created in step one.

```bash
> k apply -f .\pv-pvc-example.yaml
persistentvolume/task-pv-volume created
persistentvolumeclaim/task-pv-claim created
pod/task-pv-pod created
```

4. Observe and verify

```bash
> k get pod task-pv-pod
NAME          READY   STATUS    RESTARTS   AGE
task-pv-pod   1/1     Running   0          61s

--
> kubectl exec -it task-pv-pod -- /bin/bash
root@task-pv-pod:/#

--
> root@task-pv-pod:/# curl http://localhost/
Hello from Kubernetes storage

--
> root@task-pv-pod:/# exit
exit
```

5. Clean up

```bash
k delete pod task-pv-pod
k delete pvc task-pv-claim
k delete pv task-pv-volume

--
> minikube ssh
Last login: Tue Jun  3 22:32:45 2023 from 192.168.49.1

> docker@minikube:~$ sudo rm -r /mnt/data

> docker@minikube:~$ exit
```


### Ephemeral Volumes:

In this example a volume will created only for the lifetime of the pod. To verify we will shell into the pod and verify the mounted folder path exists.

1. Create a file `ephemeral-example.yaml` with the following.

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: test-pd
spec:
  containers:
  - image: nginx
    name: test-container
    volumeMounts:
    - mountPath: /volume
      name: my-volume
  volumes:
  - name: my-volume
    emptyDir:
      sizeLimit: 500Mi
```

2. Apply the pod

```bash
> k apply -f .\ephemeral-example.yaml
pod/test-pd created
```

3. Observe and verify

```bash
> k get po test-pd
NAME      READY   STATUS    RESTARTS   AGE
test-pd   1/1     Running   0          12s

> kubectl exec -it test-pd -- /bin/bash

> root@test-pd:/# ls |grep vol
volume
```

4. Clean up

```bash
k delete po test-pd
```

# References

- [Persistent Volumes (PVs)](https://kubernetes.io/docs/concepts/storage/persistent-volumes/)
- [Ephemeral Volumes](https://kubernetes.io/docs/concepts/storage/ephemeral-volumes/)
- [Create a PersistentVolume](https://kubernetes.io/docs/tasks/configure-pod-container/configure-persistent-volume-storage/#create-a-persistentvolume)