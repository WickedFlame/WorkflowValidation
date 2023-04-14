
namespace WorkflowValidation
{
    /// <summary>
    /// Setup for the workflow
    /// </summary>
    public class WorkflowSetup : IWorkflowSetupBuilder, IWorkflowSetup
    {
        /// <summary>
        /// Gets or sets the desctiption of the workflow
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Set the description of the workflow
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public IWorkflowSetupBuilder SetDescription(string description)
        {
            Description = description;
            return this;
        }

        /// <summary>
        /// Executes the step
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        public void Run(WorkflowContext context)
        {
            if (!string.IsNullOrEmpty(Description))
            {
                context.Log("----------------------------------------");
                context.Log($"Workflow: {Description}");
                context.Log("----------------------------------------");
            }
        }
    }
}
