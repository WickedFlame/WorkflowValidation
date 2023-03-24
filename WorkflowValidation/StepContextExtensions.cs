using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public static class StepContextExtensions
    {
        [AssertionMethod]
        public static StepContext Verify(this StepContext ctx, Action<AssertionContext> ensure)
        {
            ctx.Context.CurrentStep.Workflow.Verify(ensure);

            return ctx;
        }

        [AssertionMethod]
        public static StepContext Verify(this StepContext ctx, Func<AssertionContext, bool> ensure)
        {
            ctx.Context.CurrentStep.Workflow.Step(new AssertionStep(ensure));

            return ctx;
        }
    }
}
