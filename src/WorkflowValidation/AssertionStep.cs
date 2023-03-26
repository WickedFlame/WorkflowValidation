using System;

namespace WorkflowValidation
{
    public class AssertionStep : StepBase, IStep
    {
        private readonly Func<AssertionContext, bool> _step;

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public AssertionStep(Func<bool> step) 
            : this(c => c.Assert(step))
        {
        }

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public AssertionStep(Func<AssertionContext, bool> step)
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
            _step(new AssertionContext(context) { Name = Name });
        }
    }
}
