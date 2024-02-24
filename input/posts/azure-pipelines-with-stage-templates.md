Title: Azure Pipelines with Stage Templates
Published: 02/17/2024
Tags: 
- azure
- yml
- yaml
- azure pipelines

---

# Introduction

Optimizing pipelines for efficiency and scalability is paramount. Azure Pipelines, with its YAML-based configuration, offers a robust framework for orchestrating complex workflows. In this blog post, we'll explore the advantages of harnessing YAML templates specifically for defining stages in Azure Pipelines, using a practical example to illustrate their effectiveness.

# Example

Let's begin by examining a sample YAML configuration for Azure Pipelines, utilizing a stage template:

```yaml
# azure-pipelines.yml

stages:
- template: stages/per_stage.yml  
  parameters:
    environment: Dev

- template: stages/per_stage.yml  
  parameters:
    environment: Staging

- template: stages/per_stage.yml  
  parameters:
    environment: Prod
```

This YAML file defines stages for three different environments: Development (`Dev`), `Staging`, and Production (`Prod`). Each stage references a YAML template located at stages/per_stage.yml, passing a specific environment parameter to configure the stage accordingly.

Now, let's delve into the `per_stage.yml`:

```yaml
# stages/per_stage.yml

parameters:
  environment: ''

stages:
- stage: Stage_${{ parameters.environment }}
  jobs:
  - job: RunScript
    displayName: 'Execute Powershell Script'
    steps:
      - task: PowerShell@2
        displayName: 'Run Powershell Build'
        inputs:
          targetType: 'inline'
          script: |
            Write-Host "Script Ran For ${{ parameters.environment }}"
```

This template defines a job `RunScript` within a stage. The job executes a PowerShell script tailored to the specified environment. The environment parameter dynamically determines the configuration of the stage, ensuring flexibility and adaptability across different environments.

This is what pipeline stages should look like when run.

> <img src="/posts/images/azure-stage-template.png" style="max-width: 100%">

### Unlocking Benefits with Stage Templates

1. Modularity: Stage templates encapsulate stage configurations, promoting code reuse and reducing duplication across pipelines.
2. Flexibility: Parameters enable customization of stage configurations, making it easy to adapt stages for different environments or scenarios.
3. Maintainability: Centralized template management simplifies updates and maintenance, ensuring consistency and reliability across pipelines.

# Demo

Here is what it looks like when it's running in Azure Pipelines:

> <img src="/posts/images/azure-stage-template.gif" style="max-width: 100%">

I go into each job so you can see the parameters that get passed is used in the powershell task.

# References

- [Microsoft docs: stages.template definition](https://learn.microsoft.com/en-us/azure/devops/pipelines/yaml-schema/stages-template?view=azure-pipelines)