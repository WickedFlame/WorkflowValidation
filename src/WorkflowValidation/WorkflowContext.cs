using System;

namespace WorkflowValidation
{
    /// <summary>
    /// Context for executions of workflows
    /// </summary>
    public class WorkflowContext
    {
        /// <summary>
        /// Gets or sets the current step in the workflow
        /// </summary>
        public IStep CurrentStep { get; set; }

        /// <summary>
        /// Gets the number of the current step
        /// </summary>
        public int StepNumber { get; set; }

        /// <summary>
        /// Log a message to the console output
        /// </summary>
        /// <param name="message"></param>
        public virtual void Log(string message)
        {
            Console.Out.WriteLine(message);
        }
    }
}
