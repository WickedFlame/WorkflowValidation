using System;
using System.Linq.Expressions;

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
        public IWorkflowStep StartWith(Expression<Action> step)
        {
        
           
        //TODO: get data from expression




            _workflow.SetStep(new Step(step.Compile()));

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
        public IWorkflowStep StartWith(Expression<Action<WorkflowContext>> step)
        {
            //TODO: get data from expression

            _workflow.SetStep(new Step(step.Compile()));

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
            var ws = new WorkflowSetup();
            setup(ws);

            _workflow.WorkflowSetup = ws;

            return this;
        }
        
        /// <summary>
        /// Setup for a new workflow
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public IWorkflowBuilder SetupWorkflow(string description)
        {
            SetupWorkflow(s => s.SetDescription(description));

            return this;
        }
    }
}
