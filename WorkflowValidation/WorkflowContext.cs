using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WorkflowValidation
{
    public class WorkflowContext
    {
        public IStep CurrentStep { get; set; }
    }
}
