using static WorkflowValidation.Tools.StepTools;

namespace WorkflowValidation.Test.Tools
{
    public class StepToolsTests
    {
        [Test]
        public void StepTools_SetStep_Return()
        {
            SetStep(b => { }).Should().BeOfType<WorkflowContext>();
        }
    }
}
