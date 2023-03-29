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
        /// <param name="assert"></param>
        public static IWorkflow SetStep(Action<StepBuilder> assert)
        {
            var builder = new StepBuilder();
            assert(builder);

            return builder.Build()
                .Run();
        }
    }
}
