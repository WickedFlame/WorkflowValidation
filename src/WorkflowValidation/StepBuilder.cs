using System;

namespace WorkflowValidation
{
    public class StepBuilder
    {
        private readonly IWorkflow _workflow = new Workflow();
        private string _name;
        private Action _step = () => { };


        public StepBuilder SetContext(WorkflowContext context)
        {
            _workflow.Context = context;

            return this;
        }

        public StepBuilder SetName(string name)
        {
            _name = name;

            return this;
        }

        public StepBuilder Step(Action step)
        {
            _step = step;

            return this;
        }

        public IWorkflow Build()
        {
            _workflow.SetStep(new Step(_step)
                .SetName(_name)
            );

            return _workflow;
        }
    }
}
