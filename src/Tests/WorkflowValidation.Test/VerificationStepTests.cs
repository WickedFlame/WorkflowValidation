
namespace WorkflowValidation.Test
{
    public class VerificationStepTests
    {
        [Test]
        public void VerificationStep_Run_Context()
        {
            var ctx = new WorkflowContext();
            var step = new VerificationStep();
            step.Run(ctx);

            step.Workflow.Context.Should().BeSameAs(ctx);
        }
    }
}
