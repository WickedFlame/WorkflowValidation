using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Builder to verify or assert workflow steps.
    /// Adds a <see cref="AssertionStep"/> to the list of steps in the Workflow
    /// </summary>
    public class VerificationBuilder
    {
        private readonly IWorkflow _workflow = new Workflow();

        private string _name;
        private IStep _step;

        /// <summary>
        /// Set the <see cref="WorkflowContext"/> for the Verification Step
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public VerificationBuilder SetContext(WorkflowContext context)
        {
            _workflow.Context = context;
            return this;
        }

        /// <summary>
        /// Set the name of the step
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public VerificationBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Assert the result of a step in the workflow.
        /// Creates a <see cref="AssertionStep"/>.
        /// Throws <see cref="WorkflowException"/> if the assertion fails.
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public VerificationBuilder Assert(Func<bool> assert)
        {
            _step = new AssertionStep(assert);
            return this;
        }

        /// <summary>
        /// Build the workflow used to assert a step in the workflow
        /// </summary>
        /// <returns></returns>
        public IWorkflow Build()
        {
            if (_step == null)
            {
                _step = new Step(() => { });
            }

            _step.SetName(string.IsNullOrEmpty(_name) ? string.Empty : _name);

            _workflow.SetStep(_step);

            return _workflow;
        }
    }
}
