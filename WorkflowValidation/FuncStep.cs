using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public class FuncStep<T> : Step, IStep
    {
        private readonly Func<WorkflowContext, T> _step;

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public FuncStep(Func<WorkflowContext, T> step)
        {
            _step = step;
        }

        /// <summary>
        /// Executes the step
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        public override void Run(WorkflowContext context)
        {
            _step(context);
        }
    }
}
