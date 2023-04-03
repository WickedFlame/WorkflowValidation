using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Builder to help create and configure a workflow
    /// </summary>
    public class WorkflowBuilder : IWorkflowBuilder
    {
        private readonly IWorkflowStep _workflow = new Workflow();

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public IWorkflowStep StartWith(Action step)
        {
            _workflow.SetStep(new Step(step));

            return _workflow;
        }

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="description"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public IWorkflowStep StartWith(string description, Action work)
        {
            _workflow.SetStep(new Step(work)
                .SetName(description)
            );

            return _workflow;
        }

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public IWorkflowStep StartWith(Action<WorkflowContext> step)
        {
            _workflow.SetStep(new Step(step));

            return _workflow;
        }

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="description"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public IWorkflowStep StartWith(string description, Action<WorkflowContext> work)
        {
            _workflow.SetStep(new Step(work)
                .SetName(description)
            );

            return _workflow;
        }

        /// <summary>
        /// Setup for a new workflow
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
        public IWorkflowBuilder SetupWorkflow(Action<IWorkflowSetupBuilder> setup)
        {
            var description = new WorkflowSetup();
            setup(description);

            _workflow.WorkflowSetup = description;

            return this;
        }
    }
}
