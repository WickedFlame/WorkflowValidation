using System;

namespace WorkflowValidation.Tools
{
    /// <summary>
    /// tatic Tools and extensions to verify conditions in a workflow
    /// </summary>
    public static class VerificationTools
    {
        /// <summary>
        /// Verify a condition of a step in the workflow
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        [AssertionMethod]
        public static IWorkflow Verify(Func<bool> assert)
        {
            var builder = new VerificationBuilder();
            builder.Assert(assert);

            return builder.Build()
                .Run();
        }

        /// <summary>
        /// Verify a condition of a step in the workflow
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        [AssertionMethod]
        public static IWorkflow Verify(Action<VerificationBuilder> assert)
        {
            var builder = new VerificationBuilder();
            assert(builder);

            return builder.Build()
                .Run();
        }
    }
}
