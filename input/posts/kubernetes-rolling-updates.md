Title: Kubernetes rolling updates
Published: 6/12/2023
Tags: 
- kubernetes
- kubectl
- minikube
- docker
- docker desktop

---

# What is a rolling update in Kubernetes?

In Kubernetes, a rolling update is a strategy for updating or upgrading applications running in a cluster without causing downtime. It ensures that the application remains available to users throughout the update process by gradually replacing instances of the old version with instances of the new version.

Here's how a rolling update typically works in Kubernetes:

1. The desired changes, such as updating the application image or modifying configuration, are defined in a new version of the application's Kubernetes Deployment or StatefulSet object.
2. Kubernetes creates new instances of the updated application, typically referred to as "pods," using the updated configuration or image.
3. The new pods are gradually introduced into the cluster, while the old pods are gradually terminated.
4. Kubernetes monitors the health of the new pods and ensures they are ready to serve traffic before terminating the old pods.
5. This process continues until all the old pods have been replaced by the new pods.

By updating the application in a controlled and gradual manner, a rolling update minimizes disruption to the application's availability. It allows users to continue accessing the application while it is being updated, and if any issues are detected with the new version, Kubernetes can automatically roll back the update and revert to the previous version.

Rolling updates can be managed and configured using various strategies and parameters, such as the number of new pods to be created at a time, the maximum number of pods unavailable during the update, and the readiness and liveness probes to determine the health of the new pods.

Overall, rolling updates are a key feature of Kubernetes that help ensure seamless application updates and maintain high availability in a cluster.

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

In this example I will be making use of a custom image created from this repo [https://github.com/reggieray/dotnet-health-checks](https://github.com/reggieray/dotnet-health-checks). 

The two images I created from this example are: 

- `matthewregis/public:dotnet-health-checks` -  taken from the source code above.
- `matthewregis/public:dotnet-health-checks-v2` - modified with a route path mapped (So it's easy to visually see the difference).

1. Create deployment

Create a file `healthz-deployment.yaml` with the following:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  labels:
    app: healthz-deployment
  name: healthz-deployment
  namespace: default
spec:
  progressDeadlineSeconds: 600
  replicas: 1
  revisionHistoryLimit: 10
  selector:
    matchLabels:
      app: healthz-deployment
  strategy:
    rollingUpdate:
      maxSurge: 25%
      maxUnavailable: 25%
    type: RollingUpdate
  template:
    metadata:
      labels:
        app: healthz-deployment
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
      dnsPolicy: ClusterFirst
      restartPolicy: Always
      schedulerName: default-scheduler
      securityContext: {}
      terminationGracePeriodSeconds: 30
status: {}
```

Apply the deployment

```bash
> k apply -f .\healthz-deployment.yaml
deployment.apps/healthz-deployment created
```
2. Verify deployment

```bash
> k rollout status deploy healthz-deployment
deployment.apps/healthz-deployment created

> k get po
NAME                                 READY   STATUS    RESTARTS   AGE
healthz-deployment-d964c8c57-bg8fd   1/1     Running   0          15s

> k describe po
### output shortened for brevity ### 
Controlled By:  ReplicaSet/healthz-deployment-d964c8c57
Containers:
  healthz:
    Container ID:   docker://51e5a54f4f63c9689512e296e08429c2b298723500ce41a8648edec68f0e39e0
    Image:          matthewregis/public:dotnet-health-checks
### output shortened for brevity ### 
```

Pay attention to the image as this will be updated in the next step.

3. Perform Update

Update deployment by updating the image.

```bash
> k set image deploy/healthz-deployment healthz=matthewregis/public:dotnet-health-checks-v2
deployment.apps/healthz-deployment image updated
```

4. Verify Update

```bash
> k rollout status deploy healthz-deployment
deployment "healthz-deployment" successfully rolled out

> k get po
NAME                                 READY   STATUS    RESTARTS   AGE
healthz-deployment-fd88d9ffd-44ntq   1/1     Running   0          2m36s

> k describe po
### output shortened for brevity ### 
Controlled By:  ReplicaSet/healthz-deployment-fd88d9ffd
Containers:
  healthz:
    Container ID:   docker://01508100321b60aa26f7ddd326bd726cb6600fb0df6a46ec9a80f30b676efa0a
    Image:          matthewregis/public:dotnet-health-checks-v2
### output shortened for brevity ### 
```
Notice how the image has changed.

5. Perform Rollback

```bash
> k rollout undo deploy healthz-deployment
deployment.apps/healthz-deployment rolled back
```
Repeat the previous step to verify the deployment and pod, except this time the pod's image should now have the '-v2' suffix removed.

6. Clean up

```bash
> k delete deploy healthz-deployment
deployment.apps "healthz-deployment" deleted
```

## Optional:

If you would like to see the update visually then create a service to view the application. Create a file `healthz-service.yaml` file with the following and run it before step 3:

```yaml
apiVersion: v1
kind: Service
metadata:
  name: healthz-service
spec:
  selector:
    app: healthz-deployment
  ports:
  - name: http
    port: 80
    targetPort: 80
  type: NodePort
```

Then apply the service and create a tunnel so you can view the application.

```bash
> k apply -f .\healthz-service.yaml
service/healthz-service created

> minikube service healthz-service
|-----------|-----------------|-------------|---------------------------|
| NAMESPACE |      NAME       | TARGET PORT |            URL            |
|-----------|-----------------|-------------|---------------------------|
| default   | healthz-service | http/80     | http://192.168.49.2:30305 |
|-----------|-----------------|-------------|---------------------------|
🏃  Starting tunnel for service healthz-service.
|-----------|-----------------|-------------|------------------------|
| NAMESPACE |      NAME       | TARGET PORT |          URL           |
|-----------|-----------------|-------------|------------------------|
| default   | healthz-service |             | http://127.0.0.1:54450 |
|-----------|-----------------|-------------|------------------------|
🎉  Opening service default/healthz-service in default browser...
❗  Because you are using a Docker driver on windows, the terminal needs to be open to run it.
✋  Stopping tunnel for service healthz-service.
```

You should not see anything being served from the route path.

In another terminal perform an update as described in step 3 and wait a few seconds or verify until the deployment has successfully run.

Now refresh the URL opened earlier, you should now see a page served with 'Healthz API' being returned.

Clean up

```bash
> k delete svc healthz-service
service "healthz-service" deleted
```

# Further Reading

- [Performing a Rolling Update](https://kubernetes.io/docs/tutorials/kubernetes-basics/update/update-intro/)