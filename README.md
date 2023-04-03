[![Build status](https://img.shields.io/appveyor/build/chriswalpen/WorkflowValidation/main?label=Main&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/WorkflowValidation/branch/main)
[![Build status](https://img.shields.io/appveyor/build/chriswalpen/WorkflowValidation/dev?label=Dev&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/WorkflowValidation/branch/dev)
  
[![NuGet Version](https://img.shields.io/nuget/v/WorkflowValidation.svg?style=for-the-badge&label=Latest)](https://www.nuget.org/packages/WorkflowValidation/)
[![NuGet Version](https://img.shields.io/nuget/vpre/WorkflowValidation.svg?style=for-the-badge&label=RC)](https://www.nuget.org/packages/WorkflowValidation/)
  
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_WorkflowValidation&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=WickedFlame_WorkflowValidation)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_WorkflowValidation&metric=coverage)](https://sonarcloud.io/summary/new_code?id=WickedFlame_WorkflowValidation)

# WorkflowValidation
Run Workflows step by step with inegrated Test Assertions. 
Test Workflows in BDD style.


```csharp
Workflow.StartWith("Start step", c => { })
    .Then("Continue with step 2", c => { })
    .Then("And do step 3", c => { })
    .Run();
```
  
## Using Extensions and Tools
```csharp
using static WorkflowValidation.Tools.VerificationTools;
using static WorkflowValidation.Tools.StepTools;
using static WorkflowValidation.Tools.WorkflowTools;

Workflow<MyCustomContextType>(ctx =>
    StartWith(() =>
        {
            SetStep(b => b
                .SetName("Start Step")
                .Step(() => {
                    ctx.Message = "Workflow is started";
                })
            );

            Verify(b => b
                .SetName("Verify something")
                .Assert(() => !string.IsNullOrEmpty(ctx.Message))
            );

            SetStep(b => b
                .SetName("End Step")
                .Step(() => {
                    ctx.Message = "Workflow is ended";
                })
            );
        })
        .Then(() => { })
    )
    .Run();

public class MyCustomContextType
{
    public string Message { get; set; }
}
```