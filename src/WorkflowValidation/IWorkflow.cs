using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public interface IWorkflow
    {
        IEnumerable<IStep> Steps{ get; }

        WorkflowOptions Options { get; }

        WorkflowContext Context { get; set; }

        IStep SetStep(IStep step);

        void Run();
    }
}
