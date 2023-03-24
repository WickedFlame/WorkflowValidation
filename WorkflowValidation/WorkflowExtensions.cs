using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public static class WorkflowExtensions
    {
        public static Workflow Step(this Workflow workflow, Action<StepContext> step)
        {
            return workflow.Step(new ActionStep(step));
        }

        public static Workflow Step<T>(this Workflow workflow, Func<StepContext, T> step)
        {
            return workflow.Step(new FuncStep<T>(step));
        }

        public static Workflow Step(this Workflow workflow, string message, Action<StepContext> step)
        {
            Action<StepContext> exp = c =>
            {
                c.Log(message);
                step(c);
            };

            return workflow.Step(new ActionStep(exp));
        }

        public static Workflow Verify(this Workflow workflow, Func<AssertionContext, bool> ensure)
        {
            return workflow.Step(new AssertionStep(ensure));
        }

        public static Workflow Verify(this Workflow workflow, Action<AssertionContext> ensure)
        {
            return workflow.Step(new AssertionStep2(ensure));
        }

        public static Workflow Wait(this Workflow workflow, int milliseconds)
        {
            return workflow.Step(new ActionStep(c =>
            {
                c.Log($"Wait for {milliseconds} ms");
                Thread.Sleep(milliseconds);
            }));
        }
    }
}
