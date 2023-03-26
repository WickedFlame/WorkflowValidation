using System;

namespace WorkflowValidation
{
    public class AssertionContext
    {
        private readonly WorkflowContext _ctx;

        public AssertionContext(WorkflowContext ctx)
        {
            _ctx = ctx;
        }

        public bool Assert(Func<bool> assert, string message)
        {
            if (!assert())
            {
                _ctx.Log($"Verify: {message} [Failed]");
                throw new WorkflowException($"Expected: {message}");
            }

            _ctx.Log($"Verify: {message} [Passed]");

            return true;
        }
    }
}
