

namespace WorkflowValidation.Test
{
    public class StepExtensionsTests
    {
        [Test]
        public void Step_SetName()
        {
            var step = new TestStep();
            step.SetName("Test");

            step.Name.Should().Be("Test");
        }

        [Test]
        public void Step_SetName_ReturnType()
        {
            var step = new TestStep();
            step.SetName("Test").Should().BeSameAs(step);
        }

        public class TestStep : StepBase
        {
            public override void Run(WorkflowContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
