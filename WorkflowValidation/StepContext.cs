using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public class StepContext
    {
        public StepContext(WorkflowContext ctx)
        {
            Context = ctx;
        }

        public WorkflowContext Context { get; }

        public virtual void Log(string message)
        {
            //System.Diagnostics.Trace.WriteLine($"Step: {message}");
            Console.Out.WriteLine($"Step: {message}");
        }
    }
}
