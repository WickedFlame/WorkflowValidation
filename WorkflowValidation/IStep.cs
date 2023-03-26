using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    /// <summary>
    /// Defines a step that will be run
    /// </summary>
    public interface IStep
    {
        IWorkflow Workflow { get; }

        /// <summary>
        /// Gets the name of the step
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Executes the step
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        void Run(WorkflowContext context);
    }
}
