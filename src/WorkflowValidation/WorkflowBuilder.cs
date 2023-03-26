using System;

namespace WorkflowValidation
{
    public class WorkflowBuilder
    {
        public static IWorkflow StartWith(Action step)
        {
            var workflow = new Workflow();
            workflow.SetStep(new Step(step));

            return workflow;
        }

        public static IWorkflow StartWith(string description, Action work)
        {
            var workflow = new Workflow();
            workflow.SetStep(new Step(work)
                .SetName(description)
            );

            return workflow;
        }

        public static IWorkflow StartWith(Action<WorkflowContext> step)
        {
            var workflow = new Workflow();
            workflow.SetStep(new Step(step));

            return workflow;
        }

        public static IWorkflow StartWith(string description, Action<WorkflowContext> work)
        {
            var workflow = new Workflow();
            workflow.SetStep(new Step(work)
                .SetName(description)
            );

            return workflow;
        }
    }
}
