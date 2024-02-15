using System;
using System.Linq.Expressions;

namespace WorkflowValidation
{
    /// <summary>
    /// Builder to help create and configure a workflow
    /// </summary>
    public interface IWorkflowBuilder
    {
        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        IWorkflowStep StartWith(Expression<Action> step);

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="description"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        IWorkflowStep StartWith(string description, Action work);

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        IWorkflowStep StartWith(Action<WorkflowContext> step);

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="description"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        IWorkflowStep StartWith(string description, Action<WorkflowContext> work);

        /// <summary>
        /// Setup for a new workflow
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
        IWorkflowBuilder SetupWorkflow(Action<IWorkflowSetupBuilder> setup);
    }
}
