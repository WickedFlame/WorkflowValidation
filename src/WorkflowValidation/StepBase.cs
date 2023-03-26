
namespace WorkflowValidation
{
    /// <summary>
    /// Abstract baseclass for steps
    /// </summary>
    public abstract class StepBase : IStep
    {
        /// <summary>
        /// Gets the workflow that executes the <see cref="IStep"/>
        /// </summary>
        public IWorkflow Workflow { get; } = new Workflow();

        /// <summary>
        /// Gets the name of the step
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Executes the step
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        public abstract void Run(WorkflowContext context);
    }
}
