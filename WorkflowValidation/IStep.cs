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
        Workflow Workflow { get; }

        /// <summary>
        /// Executes the step
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        void Run(WorkflowContext context);
    }
}
