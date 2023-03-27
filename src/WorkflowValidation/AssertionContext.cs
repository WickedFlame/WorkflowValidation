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

        public string Name { get; set; }

        public bool Assert(Func<bool> assert)
            => Assert(assert, null);

        public bool Assert(Func<bool> assert, string message)
        {
            var msg = message ?? Name;

            if (!assert())
            {

                _ctx.Log($"-> Verify: {msg} [Failed]");
                throw new WorkflowException($"Expected: {msg}");
            }

            _ctx.Log($"-> Verify: {msg} [Passed]");

            return true;
        }
    }
}
