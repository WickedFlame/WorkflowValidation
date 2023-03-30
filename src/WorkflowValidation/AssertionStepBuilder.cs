using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Builder for AssertionSteps
    /// </summary>
    public class AssertionStepBuilder
    {
        private readonly IWorkflow _workflow = new Workflow();
        private string _name;
        private Func<bool> _assert = () => true;

        /// <summary>
        /// Set the <see cref="WorkflowContext"/> to the workflow of the <see cref="IStep"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public AssertionStepBuilder SetContext(WorkflowContext context)
        {
            _workflow.Context = context;

            return this;
        }

        /// <summary>
        /// Set the name to the <see cref="IStep"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AssertionStepBuilder SetName(string name)
        {
            _name = name;

            return this;
        }

        /// <summary>
        /// Set the workflow step that will be executed
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public AssertionStepBuilder Step(Func<bool> assert)
        {
            _assert = assert;

            return this;
        }

        /// <summary>
        /// Build the step and return the Workflow to run the step in
        /// </summary>
        /// <returns></returns>
        public IWorkflow Build()
        {
            _workflow.SetStep(new AssertionStep(_assert)
                .SetName(_name)
            );

            return _workflow;
        }
    }
}
