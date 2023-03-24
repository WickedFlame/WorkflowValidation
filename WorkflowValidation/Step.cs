using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public abstract class Step : IStep
    {
        public IWorkflow Workflow { get; } = new Workflow();

        public abstract void Run(WorkflowContext context);
    }
}
