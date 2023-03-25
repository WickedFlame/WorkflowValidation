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
            return new Workflow(workflow.SetStep(new ActionStep(step)));
        }

        public static IWorkflowStep Then(this IWorkflow workflow, Action<StepContext> step)
        {
            return new Workflow(workflow.SetStep(new ActionStep(step)));
        }

        //public static IWorkflow Step<T>(this IWorkflow workflow, Func<StepContext, T> step)
        //{
        //    return workflow.SetStep(new FuncStep<T>(step));
        //}

        public static IWorkflowStep Then(this IWorkflow workflow, string message, Action<StepContext> step)
        {
            Action<StepContext> exp = c =>
            {
                c.Log(message);
                step(c);
            };

            return new Workflow(workflow.SetStep(new ActionStep(exp)));
        }

        public static IWorkflowStep Verify(this IWorkflowStep workflow, Func<AssertionContext, bool> ensure)
        {
            return new Workflow(workflow.SetStep(new AssertionStep(ensure)));
        }

        public static IWorkflowStep Verify(this IWorkflowStep workflow, Action<AssertionContext> ensure)
        {
            return new Workflow(workflow.SetStep(new AssertionStep2(ensure)));
        }

        public static IWorkflow Wait(this IWorkflow workflow, int milliseconds)
        {
            return workflow.SetStep(new ActionStep(c =>
            {
                c.Log($"Wait for {milliseconds} ms");
                Thread.Sleep(milliseconds);
            }));
        }
    }
}
