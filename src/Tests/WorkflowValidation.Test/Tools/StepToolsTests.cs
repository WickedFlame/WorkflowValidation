using static WorkflowValidation.Tools.StepTools;

namespace WorkflowValidation.Test.Tools
{
    public class StepToolsTests
    {
        [Test]
        public void StepTools_SetStep_Return()
        {
            SetStep(b => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void StepTools_Verify_Return_Steps()
        {
            SetStep(b => { }).Steps.Single().Should().BeOfType<Step>();
        }
    }
}
