using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WorkflowValidation
{
    public static class WorkflowExtensions
    {
        public static IWorkflowStep Then(this IWorkflow workflow, Action step)
        {
            workflow.SetStep(new Step(step));
            return new Workflow(workflow);
        }

        public static IWorkflowStep Then(this IWorkflow workflow,string name, Action step)
        {
            workflow.SetStep(new Step(step)
                .SetName(name)
            );

            return new Workflow(workflow);
        }

        public static IWorkflowStep Then(this IWorkflow workflow, Action<WorkflowContext> step)
        {
            workflow.SetStep(new Step(step));
            return new Workflow(workflow);
        }

        //public static IWorkflow Step<T>(this IWorkflow workflow, Func<StepContext, T> step)
        //{
        //    return workflow.SetStep(new FuncStep<T>(step));
        //}

        public static IWorkflowStep Then(this IWorkflow workflow, string message, Action<WorkflowContext> step)
        {
            workflow.SetStep(new Step(step)
                .SetName(message)
            );

            return new Workflow(workflow);
        }

        //public static IWorkflowStep Verify(this IWorkflowStep workflow, Func<AssertionContext, bool> ensure)
        //{
        //    workflow.SetStep(new AssertionStep(ensure));
        //    return new Workflow(workflow);
        //}

        public static IWorkflowStep Verify(this IWorkflowStep workflow, Action<AssertionContext> ensure)
        {
            workflow.SetStep(new AssertionStep(c =>
            {
                ensure(c);
                return true;
            }));

            return new Workflow(workflow);
        }

        public static IWorkflow Wait(this IWorkflow workflow, int milliseconds)
        {
            workflow.SetStep(new Step(c =>
                {
                    Thread.Sleep(milliseconds);
                })
                .SetName($"Wait for {milliseconds} ms"));

            return workflow;
        }
    }
}
