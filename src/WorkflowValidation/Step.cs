using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Defines a step receiving a ExecutionContext that will be run
    /// </summary>
    public class Step : StepBase, IStep
    {
        private readonly Action<WorkflowContext> _step;

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public Step(Action step) 
            : this(a => step.Invoke())
        {
        }

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public Step(Action<WorkflowContext> step)
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
            if (!string.IsNullOrEmpty(Name))
            {
                context.Log($"-> Step: {Name}");
            }

            _step(context);
        }
    }
}
