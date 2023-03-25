using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Defines a step receiving a ExecutionContext that will be run
    /// </summary>
    public class ActionStep : Step, IStep
    {
        private readonly Action<StepContext> _step;

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public ActionStep(Action step) 
            : this(a => step.Invoke())
        {
        }

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public ActionStep(Action<StepContext> step)
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
            _step(new StepContext(context));
        }
    }
}
