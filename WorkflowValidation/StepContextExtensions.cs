using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public static class StepContextExtensions
    {
        [AssertionMethod]
        public static WorkflowContext Verify(this WorkflowContext ctx, Func<bool> ensure)
        {
            ctx.CurrentStep.Workflow.SetStep(new AssertionStep(ensure));

            return ctx;
        }

        [AssertionMethod]
        public static WorkflowContext Verify(this WorkflowContext ctx, Action<AssertionContext> ensure)
        {
            //ctx.Context.CurrentStep.Workflow.Verify(ensure);

            //return ctx;
            throw new NotImplementedException();
        }

        [AssertionMethod]
        public static WorkflowContext Verify(this WorkflowContext ctx, Func<AssertionContext, bool> ensure)
        {
            ctx.CurrentStep.Workflow.SetStep(new AssertionStep(ensure));



            return ctx;
        }
    }
}
