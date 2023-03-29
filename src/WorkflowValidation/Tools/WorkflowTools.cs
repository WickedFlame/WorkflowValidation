using System;

namespace WorkflowValidation.Tools
{
    /// <summary>
    /// Static Tools and extensions to create workflows
    /// </summary>
    public static class WorkflowTools
    {
        /// <summary>
        /// Create a context that is used within the workflow execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workflow"></param>
        /// <returns></returns>
        public static IWorkflow Workflow<T>(Func<T, IWorkflow> workflow) where T : class, new()
        {
            var context = new T();
            return workflow(context);
        }

        /// <summary>
        /// Start a new workflow with the step
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflow StartWith(Action step)
        {
            return WorkflowBuilder.StartWith(step);
        }

        /// <summary>
        /// Start a new workflow with the step
        /// </summary>
        /// <param name="description"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflow StartWith(string description, Action step)
        {
            return WorkflowBuilder.StartWith(description, step);
        }

        /// <summary>
        /// Start a new workflow with the step
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflow StartWith(Action<WorkflowContext> step)
        {
            return WorkflowBuilder.StartWith(step);
        }

        /// <summary>
        /// Start a new workflow with the step
        /// </summary>
        /// <param name="description"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflow StartWith(string description, Action<WorkflowContext> step)
        {
            return WorkflowBuilder.StartWith(description, step);
        }

        
    }
}
