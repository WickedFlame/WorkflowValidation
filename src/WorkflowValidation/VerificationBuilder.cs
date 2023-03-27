using System;

namespace WorkflowValidation
{
    public class VerificationBuilder
    {
        private readonly IWorkflow _workflow = new Workflow(new WorkflowOptions
        {
            TraceSteps = false
        });

        private string _name;
        private IStep _step;

        public VerificationBuilder SetContext(WorkflowContext context)
        {
            _workflow.Context = context;
            return this;
        }

        public VerificationBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public VerificationBuilder Assert(Func<bool> assert)
        {
            _step = new AssertionStep(assert);
            return this;
        }

        public IWorkflow Build()
        {
            if (_step == null)
            {
                return _workflow;
            }

            _step.Name = string.IsNullOrEmpty(_name) ? string.Empty : _name;

            _workflow.SetStep(_step);

            return _workflow;
        }
    }
}
