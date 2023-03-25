using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WorkflowValidation
{
    public class WorkflowContext
    {
        public IStep CurrentStep { get; set; }

        public virtual void Log(string message)
        {
            //System.Diagnostics.Trace.WriteLine($"Step: {message}");
            Console.Out.WriteLine($"Step: {message}");
        }
    }
}
