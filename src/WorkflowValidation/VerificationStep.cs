
namespace WorkflowValidation
{
    /// <summary>
    /// Represents a <see cref="IStep"/> to verify workflow steps
    /// </summary>
    public class VerificationStep : StepBase    
    {
        /// <summary>
        /// Run the step
        /// </summary>
        /// <param name="context"></param>
        public override void Run(WorkflowContext context)
        {
            Workflow.Context = context;
            Workflow.Run();
        }
    }
}
