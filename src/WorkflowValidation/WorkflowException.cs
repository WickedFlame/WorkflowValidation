using System;
using System.Runtime.Serialization;

namespace WorkflowValidation
{
    /// <summary>
    /// Exception is thrown when a validation or assertion in a workflow fails
    /// </summary>
    [Serializable]
    public class WorkflowException : Exception
    {
        /// <summary>
        /// Creates a new WorkflowException to show a error in the workflow
        /// </summary>
        /// <param name="message"></param>
        public WorkflowException(string message) : base(message) { }

        /// <summary>
        /// Creates a new WorkflowException to show a error in the workflow
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected WorkflowException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
