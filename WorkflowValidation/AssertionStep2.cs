using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public class AssertionStep2 : Step, IStep
    {
        private readonly Action<AssertionContext> _step;

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public AssertionStep2(Action<AssertionContext> step)
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
            _step(new AssertionContext(context));
        }
    }
}
