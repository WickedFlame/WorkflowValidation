using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public interface IWorkflow
    {
        IEnumerable<IStep> Steps{ get; }

        IWorkflow SetStep(IStep step);


        //IWorkflow Then(Action action);

        void Run();
    }
}
