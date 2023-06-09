﻿using System;
using System.Text;

namespace WorkflowValidation
{
    /// <summary>
    /// Extensions for WorkflowContext
    /// </summary>
    public static class WorkflowContextExtensions
    {
        /// <summary>
        /// Setup the <see cref="IStep"/>
        /// The step is automatically executed because the step is added to a running workflow
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="step"></param>
        public static WorkflowContext SetStep(this WorkflowContext ctx, Action<StepBuilder> step)
        {
            var builder = new StepBuilder()
                .SetContext(ctx);

            step(builder);

            builder.Build()
                .Run();

            return ctx;
        }

        /// <summary>
        /// Setup the <see cref="IStep"/>
        /// The step is automatically executed because the step is added to a running workflow
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="name"></param>
        /// <param name="step"></param>
        public static WorkflowContext SetStep(this WorkflowContext ctx, string name, Action<StepBuilder> step)
        {
            var builder = new StepBuilder()
                .SetContext(ctx)
                .SetName(name);

            step(builder);

            builder.Build()
                .Run();

            return ctx;
        }

        [AssertionMethod]
        public static WorkflowContext Verify(this WorkflowContext ctx, Func<bool> assert)
        {
            return ctx.Verify(b => b
                .Assert(assert)
            );
        }

        [AssertionMethod]
        public static WorkflowContext Verify(this WorkflowContext ctx, string name, Func<bool> assert)
        {
            return ctx.Verify(b => b
                .Assert(assert)
                .SetName(name)
            );
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
        /// Write all logs to a string
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static string TraceLogs(this WorkflowContext ctx)
        {
            var sb = new StringBuilder();
            foreach (var log in ctx.Logs)
            {
                sb.AppendLine(log);
            }

            return sb.ToString();
        }
    }
}
