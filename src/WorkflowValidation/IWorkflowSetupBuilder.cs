
namespace WorkflowValidation
{
    /// <summary>
    /// Builder to setup workflows
    /// </summary>
    public interface IWorkflowSetupBuilder
    {
        /// <summary>
        /// Set the description of the workflow
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        IWorkflowSetupBuilder SetDescription(string description);
    }
}
