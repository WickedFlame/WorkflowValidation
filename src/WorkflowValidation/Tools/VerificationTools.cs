using System;

namespace WorkflowValidation.Tools
{
    public static class VerificationTools
    {
        [AssertionMethod]
        public static void Verify(Func<bool> assert)
        {
            var builder = new VerificationBuilder();
            builder.Assert(assert);

            builder.Build()
                .Run();
        }

        [AssertionMethod]
        public static void Verify(Action<VerificationBuilder> assert)
        {
            var builder = new VerificationBuilder();
            assert(builder);

            builder.Build()
                .Run();
        }
    }
}
