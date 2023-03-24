using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public class WorkflowException : Exception
    {
        public WorkflowException(string message) : base(message) { }
    }
}
