using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Defines a step receiving a ExecutionContext that will be run
    /// </summary>
    public class ActionStep : Step, IStep
    {
        private readonly Action<WorkflowContext> _step;

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public ActionStep(Action step, string name) 
            : this(a => step.Invoke(), name)
        {
        }

        /// <summary>
        /// Defines a step that will be run
        /// </summary>
        public ActionStep(Action<WorkflowContext> step, string name)
        {
            _step = step;
            Name = name;
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
