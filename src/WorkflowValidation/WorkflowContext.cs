using System;
using System.Collections.Generic;

namespace WorkflowValidation
{
    /// <summary>
    /// Context for executions of workflows
    /// </summary>
    public class WorkflowContext
    {
        private readonly List<string> _logs = new List<string>();

        /// <summary>
        /// Gets or sets the current step in the workflow
        /// </summary>
        public IStep CurrentStep { get; set; }

        /// <summary>
        /// Gets the number of the current step
        /// </summary>
        public int StepNumber { get; set; }

        public IEnumerable<string> Logs => _logs;

        /// <summary>
        /// Log a message to the console output
        /// </summary>
        /// <param name="message"></param>
        public virtual void Log(string message)
        {
            _logs.Add(message);

            Console.Out.WriteLine(message);
        }
    }
}
