using System;

namespace WorkflowValidation
{
    public class WorkflowException : Exception
    {
        public WorkflowException(string message) : base(message) { }
    }
}
