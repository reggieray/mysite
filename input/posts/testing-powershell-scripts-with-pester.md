Title: Testing powershell scripts with Pester
Published: 02/17/2024
Tags: 
- powershell
- pester

---

# Introduction

In the realm of PowerShell scripting, ensuring that your scripts function as expected is crucial. With the introduction of Pester, a testing framework for PowerShell, developers can automate the testing process, ensuring code reliability and reducing the likelihood of bugs in their scripts. In this guide, we'll explore the basics of Pester and demonstrate its usage through a practical example.

# What is Pester?

[Pester](https://pester.dev/) is a testing framework for PowerShell that allows developers to write and execute unit tests for their scripts. Inspired by other testing frameworks like RSpec and Mocha, Pester provides a simple syntax for defining tests and assertions.

# Getting Started with Pester:

To begin using Pester, you'll first need to install it. 

```ps
Install-Module Pester -Force
```
And follow it by
```ps
Import-Module Pester -PassThru
```

Once installed, you can start writing tests using the Describe, Context, and It blocks provided by Pester.

# Example: Testing a PowerShell Script with Pester

Consider a simple PowerShell script named `HelloWorld.ps1` with the following contents:

```ps
[CmdletBinding()]
param (
    [Parameter(Mandatory=$true)]
    [string]$name, 
    [Parameter(Mandatory=$true)]
    [DateTime]$dob
)

$today = Get-Date
$span = $today - $dob
$age = New-Object DateTime -ArgumentList $Span.Ticks

Write-Host "Hello, $name. Your age is $($age.Year -1)."
```

Let's write tests for this script using Pester. It's worth noting if creating a test for `HelloWorld.ps1` the naming convention would be `HelloWorld.Tests.ps1`.

```ps
Describe 'HelloWorld' {
    
    #Mock example
    It 'Given a valid name and dob parameter, it returns message' {
        Mock Write-Host {}
        $date = Get-Date -Format "yyyy-MM-dd"

        . $PSScriptRoot/HelloWorld.ps1 "John" $date

        Assert-MockCalled Write-Host -Exactly 1 -Scope It -ParameterFilter { $Object -eq "Hello, John. Your age is 0." }
    }

    #Should -Throw example
    It 'Given no name parameter, it throws' {
        {. $PSScriptRoot/HelloWorld.ps1 "" "2023-01-01"} | Should -Throw "Cannot bind argument to parameter 'name' because it is an empty string."
    }

    #-TestCases with mocking example
    It 'Given a valid name and dob parameter, it returns message' -TestCases @(
        @{ Name = "Mark"; DOB = "2024-01-01"; ExpectedAge = 0; }
        @{ Name = "Luke "; DOB = "2010-01-01"; ExpectedAge = 14; }
        @{ Name = "John "; DOB = "2000-01-01"; ExpectedAge = 24; }
        @{ Name = "Matthew "; DOB = "1990-01-01"; ExpectedAge = 34; }
    ) {
        Mock Write-Host {}
        $mockedGetDate = [DateTime] "2024-02-17"
        Mock Get-Date { return $mockedGetDate }

        . $PSScriptRoot/HelloWorld.ps1 $Name $DOB

        Assert-MockCalled Write-Host -Exactly 1 -Scope It -ParameterFilter { $Object -eq "Hello, $Name. Your age is $ExpectedAge." }
    }
}
```

## Explanation:

- The `Describe` block defines the scope of our tests, grouping related tests together.
- Inside the `Describe` block, we have multiple `It` blocks, each representing an individual test case.
- We use `Mock` to mock certain cmdlets or functions to control their behavior during testing.
- The `Should -Throw` assertion checks if the script throws the expected error under specific conditions.
- The `-TestCases` attribute allows us to run the same test with different input parameters, enhancing test coverage.

## Demo:

Here I'm installing a pester and invoking the tests from a terminal. Notice the additional flag `-SkipPublisherCheck` I added on. This was because I had pester previously installed and I wanted to install the latest. To invoke pester I used `Invoke-Pester -Output Detailed .\HelloWorld.Tests.ps1`.

> <img src="/posts/images/pester.gif" style="max-width: 100%">

Pester has good support in [VScode](https://code.visualstudio.com/). Here I demo running the tests and also the ability to debug tests using VScode.

> <img src="/posts/images/pester-2.gif" style="max-width: 100%">

# Summary:
In this guide, we've explored how to use Pester, a testing framework for PowerShell, to write unit tests for PowerShell scripts. By incorporating testing into your development workflow, you can ensure the reliability and robustness of your PowerShell scripts, leading to more stable and maintainable codebases.

# References

- [Pester GitHub Repository](https://github.com/pester/Pester)
- [Pester Documentation](https://pester.dev/)
- [Microsoft Blog - Getting Started with Pester](https://devblogs.microsoft.com/scripting/getting-started-with-pester/)