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
    }
}
