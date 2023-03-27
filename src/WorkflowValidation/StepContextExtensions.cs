using System;

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
        public static WorkflowContext Verify(this WorkflowContext ctx, string name, Func<bool> ensure)
        {
            ctx.CurrentStep.Workflow.SetStep(new AssertionStep(ensure) { Name = name });

            return ctx;
        }

        /// <summary>
        /// Creates a AssertionStep to verify a condition on the test with the help of a <see cref="VerificationBuilder"/>. 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="assert"></param>
        /// <returns></returns>
        [AssertionMethod]
        public static WorkflowContext Verify(this WorkflowContext ctx, Action<VerificationBuilder> assert)
        {
            var builder = new VerificationBuilder()
                .SetContext(ctx);

            assert(builder);

            builder.Build()
                .Run();

            return ctx;
        }

        /// <summary>
        /// Setup the <see cref="IStep"/>
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="assert"></param>
        public static WorkflowContext SetStep(this WorkflowContext ctx, Action<StepBuilder> assert)
        {
            var builder = new StepBuilder()
                .SetContext(ctx);

            assert(builder);

            builder.Build()
                .Run();

            return ctx;
        }

        //[AssertionMethod]
        //public static WorkflowContext Verify(this WorkflowContext ctx, Action<AssertionContext> ensure)
        //{
        //    //ctx.Context.CurrentStep.Workflow.Verify(ensure);

        //    //return ctx;
        //    throw new NotImplementedException();
        //}

        //[AssertionMethod]
        //public static WorkflowContext Verify(this WorkflowContext ctx, Func<AssertionContext, bool> ensure)
        //{
        //    ctx.CurrentStep.Workflow.SetStep(new AssertionStep(ensure));



        //    return ctx;
        //}
    }
}
