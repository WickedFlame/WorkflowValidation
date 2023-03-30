using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Builder to help create and configure a workflow
    /// </summary>
    public class WorkflowBuilder
    {
        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflowStep StartWith(Action step)
        {
            var workflow = new Workflow();
            workflow.SetStep(new Step(step));

            return workflow;
        }

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="description"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public static IWorkflowStep StartWith(string description, Action work)
        {
            var workflow = new Workflow();
            workflow.SetStep(new Step(work)
                .SetName(description)
            );

            return workflow;
        }

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflowStep StartWith(Action<WorkflowContext> step)
        {
            var workflow = new Workflow();
            workflow.SetStep(new Step(step));

            return workflow;
        }

        /// <summary>
        /// Start the workflow with the given step
        /// </summary>
        /// <param name="description"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public static IWorkflowStep StartWith(string description, Action<WorkflowContext> work)
        {
            var workflow = new Workflow();
            workflow.SetStep(new Step(work)
                .SetName(description)
            );

            return workflow;
        }
    }
}
