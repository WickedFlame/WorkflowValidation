using System;

namespace WorkflowValidation.Tools
{
    public static class WorkflowTools
    {
        public static IWorkflow StartWith(Action step)
        {
            return WorkflowBuilder.StartWith(step);
        }

        public static IWorkflow StartWith(string description, Action step)
        {
            return WorkflowBuilder.StartWith(description, step);
        }

        public static IWorkflow StartWith(Action<WorkflowContext> step)
        {
            return WorkflowBuilder.StartWith(step);
        }

        public static IWorkflow StartWith(string description, Action<WorkflowContext> step)
        {
            return WorkflowBuilder.StartWith(description, step);
        }

        public static IWorkflow Workflow<T>(Func<T, IWorkflow> workflow) where T : class, new()
        {
            var context = new T();
            return workflow(context);
        }
    }
}
