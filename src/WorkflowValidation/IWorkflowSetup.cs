
namespace WorkflowValidation
{
    /// <summary>
    /// Setup for the workflow
    /// </summary>
    public interface IWorkflowSetup
    {
        /// <summary>
        /// Gets or sets the desctiption of the workflow
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Executes the step
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        void Run(WorkflowContext context);
    }
}
