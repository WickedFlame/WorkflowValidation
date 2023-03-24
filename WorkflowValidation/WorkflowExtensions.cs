using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WorkflowValidation
{
    public static class WorkflowExtensions
    {
        public static IWorkflow Then(this IWorkflow workflow, Action step)
        {
            //return workflow.SetStep(new ActionStep(step));
            throw new NotImplementedException();
        }

        public static IWorkflow Then(this IWorkflow workflow, Action<StepContext> step)
        {
            return workflow.SetStep(new ActionStep(step));
        }

        //public static IWorkflow Step<T>(this IWorkflow workflow, Func<StepContext, T> step)
        //{
        //    return workflow.SetStep(new FuncStep<T>(step));
        //}

        public static IWorkflow Then(this IWorkflow workflow, string message, Action<StepContext> step)
        {
            Action<StepContext> exp = c =>
            {
                c.Log(message);
                step(c);
            };

            return workflow.SetStep(new ActionStep(exp));
        }

        public static IWorkflow Verify(this IWorkflow workflow, Func<AssertionContext, bool> ensure)
        {
            return workflow.SetStep(new AssertionStep(ensure));
        }

        public static IWorkflow Verify(this IWorkflow workflow, Action<AssertionContext> ensure)
        {
            return workflow.SetStep(new AssertionStep2(ensure));
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
