using System;

namespace WorkflowValidation.Tools
{
    /// <summary>
    /// Tools for <see cref="IStep"/>
    /// </summary>
    public static class StepTools
    {
        /// <summary>
        /// Setup the <see cref="IStep"/>
        /// This is invoked directly when the parent action gets invoked. Use SetContext with the WorkflowContext of the parent action to share the same context
        /// </summary>
        /// <param name="step"></param>
        public static WorkflowContext SetStep(Action<StepBuilder> step)
        {
            var builder = new StepBuilder();
            step(builder);

            return builder.Build()
                .Run().Context;
        }

        /// <summary>
        /// Setup the <see cref="IStep"/>
        /// This is invoked directly when the parent action gets invoked. Use SetContext with the WorkflowContext of the parent action to share the same context
        /// </summary>
        /// <param name="name"></param>
        /// <param name="step"></param>
        public static WorkflowContext SetStep(string name, Action<StepBuilder> step)
        {
            var builder = new StepBuilder()
                .SetName(name);

            step(builder);

            return builder.Build()
                .Run().Context;
        }

        /// <summary>
        /// Setup the <see cref="IStep"/>
        /// This is invoked directly when the parent action gets invoked.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="step"></param>
        public static WorkflowContext SetStep(string name, Action step)
        {
            var builder = new StepBuilder()
                .SetName(name)
                .Step(step);

            return builder.Build()
                .Run().Context;
        }

        /// <summary>
        /// Setup the <see cref="IStep"/>
        /// This is invoked directly when the parent action gets invoked.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        /// <param name="step"></param>
        public static WorkflowContext SetStep(string name, WorkflowContext context, Action step)
        {
            var builder = new StepBuilder()
                .SetName(name)
                .SetContext(context)
                .Step(step);

            return builder.Build()
                .Run().Context;
        }
    }
}
