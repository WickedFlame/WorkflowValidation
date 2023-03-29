
using System.Text;

namespace WorkflowValidation.Test
{
    public class WorkflowContextTests
    {
        [Test]
        public void WorkflowContext_CurrentStep()
        {
            new WorkflowContext().CurrentStep.Should().BeNull();
        }

        [Test]
        public void WorkflowContext_StepNumber()
        {
            new WorkflowContext().StepNumber.Should().Be(0);
        }

        [Test]
        public void WorkflowContext_Log()
        {
            var stdOut = Console.Out;

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            new WorkflowContext().Log("this is a test log");

            consoleOut.ToString().TrimEnd().Should().Be("this is a test log");

            Console.SetOut(stdOut);
            
        }
    }
}
