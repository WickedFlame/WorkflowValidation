using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Builder to help create and configure steps in a workflow
    /// </summary>
    public class StepBuilder
    {
        private readonly IWorkflow _workflow = new Workflow();
        private string _name;
        private IStep _step = new Step(() => { });
        
        /// <summary>
        /// Set the <see cref="WorkflowContext"/> to the workflow of the <see cref="IStep"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public StepBuilder SetContext(WorkflowContext context)
        {
            _workflow.Context = context;

            return this;
        }

        /// <summary>
        /// Set the name to the <see cref="IStep"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public StepBuilder SetName(string name)
        {
            _name = name;

            return this;
        }

        /// <summary>
        /// Set the workflow step that will be executed
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public StepBuilder Step(Action step)
        {
            _step = new Step(step);

            return this;
        }

        /// <summary>
        /// Set the workflow step that will be executed
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public StepBuilder Step(Action<WorkflowContext> step)
        {
            _step = new Step(step);

            return this;
        }

        /// <summary>
        /// Build the step and return the Workflow to run the step in
        /// </summary>
        /// <returns></returns>
        public IWorkflow Build()
        {
            if (!string.IsNullOrEmpty(_name))
            {
                _step.SetName(_name);
            }

            _workflow.SetStep(_step);

            return _workflow;
        }
    }
}
