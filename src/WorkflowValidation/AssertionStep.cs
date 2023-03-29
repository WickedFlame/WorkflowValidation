using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Represents a <see cref="IStep"/> to assert workflow steps
    /// </summary>
    public class AssertionStep : StepBase, IStep
    {
        private readonly Func<AssertionProvider, bool> _step;

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
        public AssertionStep(Func<AssertionProvider, bool> step)
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
            _step(new AssertionProvider(context) { Name = Name });
        }
    }
}
