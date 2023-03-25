using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public static class StepContextExtensions
    {
        [AssertionMethod]
        public static StepContext Verify(this StepContext ctx, Func<bool> ensure)
        {
            ctx.Context.CurrentStep.Workflow.SetStep(new AssertionStep(ensure));

            return ctx;
        }

        [AssertionMethod]
        public static StepContext Verify(this StepContext ctx, Action<AssertionContext> ensure)
        {
            //ctx.Context.CurrentStep.Workflow.Verify(ensure);

            //return ctx;
            throw new NotImplementedException();
        }

        [AssertionMethod]
        public static StepContext Verify(this StepContext ctx, Func<AssertionContext, bool> ensure)
        {
            ctx.Context.CurrentStep.Workflow.SetStep(new AssertionStep(ensure));



            return ctx;
        }
    }
}
