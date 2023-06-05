Title: Kubernetes Jobs and CronJobs
Published: 6/2/2023
Tags: 
- kubernetes
- kubectl
- minikube
- docker
- docker desktop

---

# What are Jobs & CronJobs in Kubernetes? 

## Jobs:

A job in Kubernetes is a unit of work that runs to completion. It's used for tasks like batch processing or data analysis. When you create a job, you specify the number of instances (pods) to run in parallel. The job ensures that the specified number of instances complete successfully before considering the job done. It handles things like restarting failed instances and tracks progress. Jobs are useful for short-lived tasks, while long-running tasks are handled by other resources like deployments or stateful sets. Overall, jobs manage batch or one-time tasks reliably in Kubernetes.

## CronJobs:

A cronjob in Kubernetes is a resource that automates recurring tasks based on a defined schedule. It uses the familiar cron syntax to specify when and how often the tasks should run. Kubernetes automatically creates jobs for each scheduled task, making it easy to automate periodic operations within the cluster.

# Examples

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

### Job:

1. Create a file called `job.yaml` and the following:

```yaml
apiVersion: batch/v1
kind: Job
metadata:
  name: pi
spec:
  template:
    spec:
      containers:
      - name: pi
        image: perl:5.34.0
        command: ["perl",  "-Mbignum=bpi", "-wle", "print bpi(2000)"]
      restartPolicy: Never
  backoffLimit: 4
```

2. Run the job

```bash
> k apply -f job.yaml
job.batch/pi created
```

3. Observe and verify job.
```bash
> k get jobs 
NAME   COMPLETIONS   DURATION   AGE
pi     1/1           7s         118s

> k get job pi
NAME   COMPLETIONS   DURATION   AGE
pi     1/1           7s         2m23s

> k describe job pi
Name:             pi
Namespace:        default
Selector:         controller-uid=46c45948-8935-4898-b1ce-a6a03704829c
### output shortened for brevity ### 

> k logs jobs/pi
3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679821480865132823066470938446095505822317253594081284811174502841027019385211055596446229489549303819644288109756659334461284756482337867831652712019091456485669234603486104543266482133936072602491412737245870066063155881748815209209628292540917153643678925903600113305305488204665213841469519415116094
### output shortened for brevity ### 
```

4. Delete job

```bash
> k delete job pi
job.batch "pi" deleted
```

### CronJob:

1. Create a file called `cron-job.yaml` with the following contents:

```yaml
apiVersion: batch/v1
kind: CronJob
metadata:
  name: hello
spec:
  schedule: "* * * * *"
  jobTemplate:
    spec:
      template:
        spec:
          containers:
          - name: hello
            image: busybox:1.28
            imagePullPolicy: IfNotPresent
            command:
            - /bin/sh
            - -c
            - date; echo Hello from the Kubernetes cluster
          restartPolicy: OnFailure
```

2. Run the cron job

```bash
> k apply -f cron-job.yaml
cronjob.batch/hello created
```

3. Observe verify the cron job.
```bash
> k get cronjob 
NAME    SCHEDULE    SUSPEND   ACTIVE   LAST SCHEDULE   AGE
hello   * * * * *   False     0        25s             36s

> k get cronjob hello
NAME    SCHEDULE    SUSPEND   ACTIVE   LAST SCHEDULE   AGE
hello   * * * * *   False     0        48s             59s

> k describe cronjob hello
Name:                          hello
Namespace:                     default
Labels:                        <none>
Annotations:                   <none>
Schedule:                      * * * * *
### output shortened for brevity ### 

> k get pods
NAME                   READY   STATUS              RESTARTS      AGE
hello-28099978-l64cn   0/1     Completed           0             2m
hello-28099979-z9j7d   0/1     Completed           0             60s
hello-28099980-xmlds   0/1     ContainerCreating   0             0s

> k logs hello-28099978-l64cn
Mon Jun  5 20:58:00 UTC 2023
Hello from the Kubernetes cluster
```

4. Delete cron job.
```bash
> k delete cronjob hello
cronjob.batch "hello" deleted
```

# References

- [Jobs](https://kubernetes.io/docs/concepts/workloads/controllers/job/) 
- [CronJobs](https://kubernetes.io/docs/concepts/workloads/controllers/cron-jobs/)