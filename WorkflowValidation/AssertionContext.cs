using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public class AssertionContext : WorkflowContext
    {
        private readonly WorkflowContext _ctx;

        public AssertionContext(WorkflowContext ctx)
        {
            _ctx = ctx;
        }

        public void Assert(Func<bool> assert, string message)
        {
            if (!assert())
            {
                Log($"{message} [Failed]");
                throw new WorkflowException($"Expected: {message}");
            }

            Log($"{message} [Passed]");
        }

        public override void Log(string message)
        {
            //System.Diagnostics.Trace.WriteLine($"Verify: {message}");
            Console.Out.WriteLine($"Verify: {message}");
        }
    }
}
