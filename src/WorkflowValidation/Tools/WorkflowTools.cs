using System;

namespace WorkflowValidation.Tools
{
    /// <summary>
    /// Static Tools and extensions to create workflows
    /// </summary>
    public static class WorkflowTools
    {
        /// <summary>
        /// Create workflow that a context that is used within the workflow execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workflow"></param>
        /// <returns></returns>
        public static IWorkflow Workflow<T>(Func<T, IWorkflow> workflow) where T : class, new()
        {
            var context = Activator.CreateInstance(typeof(T)) as T;
            return workflow(context);
        }

        /// <summary>
        /// Create workflow that a context that is used within the workflow execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflow Workflow<T>(Action<T, IWorkflowBuilder> step) where T : class, new()
        {
            var workflow = new Workflow();
            workflow.SetStep(new Step(() => step(Activator.CreateInstance(typeof(T)) as T, new WorkflowBuilder())));

            return workflow;
        }

        /// <summary>
        /// Start a new workflow.
        /// Workflows that are nested will not be run automaticaly. Use Run() to run a Workflow.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflow StartWith(Action step)
        {
            return WorkflowValidation.Workflow.StartWith(step);
        }

        /// <summary>
        /// Start a new workflow.
        /// Workflows that are nested will not be run automaticaly. Use Run() to run a Workflow.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflow StartWith(string description, Action step)
        {
            return WorkflowValidation.Workflow.StartWith(description, step);
        }

        /// <summary>
        /// Start a new workflow.
        /// Workflows that are nested will not be run automaticaly. Use Run() to run a Workflow.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflow StartWith(Action<WorkflowContext> step)
        {
            return WorkflowValidation.Workflow.StartWith(step);
        }

        /// <summary>
        /// Start a new workflow with the step
        /// </summary>
        /// <param name="description"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflow StartWith(string description, Action<WorkflowContext> step)
        {
            return WorkflowValidation.Workflow.StartWith(description, step);
        }

        public static IWorkflowBuilder SetupWorkflow(Action<IWorkflowSetupBuilder> setup)
        {
            var builder = new WorkflowBuilder();
            return builder.SetupWorkflow(setup);
        }
        
    }
}
