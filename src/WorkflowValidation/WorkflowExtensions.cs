using System;
using System.Threading;

namespace WorkflowValidation
{
    /// <summary>
    /// Extensions for workflows
    /// </summary>
    public static class WorkflowExtensions
    {
        /// <summary>
        /// Add a follow step to the workflow
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflowStep Then(this IWorkflowStep workflow, Action step)
        {
            workflow.SetStep(new Step(step));
            return new Workflow(workflow);
        }

        /// <summary>
        /// Add a follow step to the workflow
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="name"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflowStep Then(this IWorkflowStep workflow,string name, Action step)
        {
            workflow.SetStep(new Step(step)
                .SetName(name)
            );

            return new Workflow(workflow);
        }

        /// <summary>
        /// Add a follow step to the workflow
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflowStep Then(this IWorkflowStep workflow, Action<WorkflowContext> step)
        {
            workflow.SetStep(new Step(step));
            return new Workflow(workflow);
        }

        /// <summary>
        /// Add a follow step to the workflow
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="message"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IWorkflowStep Then(this IWorkflowStep workflow, string message, Action<WorkflowContext> step)
        {
            workflow.SetStep(new Step(step)
                .SetName(message)
            );

            return new Workflow(workflow);
        }

        /// <summary>
        /// Add a verification step to the workflow. Throws a <see cref="WorkflowException"/> if the step does not assert to true.
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="ensure"></param>
        /// <returns></returns>
        [AssertionMethod]
        public static IWorkflowStep Verify(this IWorkflowStep workflow, Action<AssertionProvider> ensure)
        {
            workflow.SetStep(new AssertionStep(c =>
            {
                ensure(c);
                return true;
            }));

            return new Workflow(workflow);
        }

        [AssertionMethod]
        public static IWorkflowStep Verify(this IWorkflowStep workflow, string name, Func<bool> assert)
        {
            workflow.SetStep(new AssertionStep(assert)
                .SetName(name)
            );

            return new Workflow(workflow);
        }

        /// <summary>
        /// Waits on the thread for a given amount of milliseconds
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static IWorkflowStep Wait(this IWorkflowStep workflow, int milliseconds)
        {
            workflow.SetStep(new Step(() =>
                {
                    System.Threading.Tasks.Task.Delay(milliseconds).Wait();
                })
                .SetName($"Wait for {milliseconds} ms")
            );

            return workflow;
        }
    }
}
