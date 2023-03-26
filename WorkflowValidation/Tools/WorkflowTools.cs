using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation.Tools
{
    public static class WorkflowTools
    {
        public static IWorkflow StartWith(Action step)
        {
            return WorkflowBuilder.StartWith(step);
        }

        public static IWorkflow WithContext<T>(Func<T, IWorkflow> workflow) where T : class, new()
        {
            var context = new T();
            return workflow(context);
        }
    }
}
