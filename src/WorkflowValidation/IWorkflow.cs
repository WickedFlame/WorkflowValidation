using System.Collections.Generic;

namespace WorkflowValidation
{
    /// <summary>
    /// A workflow containing 1-n steps to run
    /// </summary>
    public interface IWorkflow
    {
        /// <summary>
        /// Gets all the steps that are contained in the workflow
        /// </summary>
        IEnumerable<IStep> Steps { get; }

        /// <summary>
        /// Gets the WorkflowContext used in the workflow execution
        /// </summary>
        WorkflowContext Context { get; set; }

        /// <summary>
        /// Add a Step to the workflow
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        IStep SetStep(IStep step);

        /// <summary>
        /// Run all steps in the workflow
        /// </summary>
        /// <returns></returns>
        IWorkflow Run();
    }
}
