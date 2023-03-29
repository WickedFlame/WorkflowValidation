
namespace WorkflowValidation.Test
{
    public class StepBaseTests
    {
        [Test]
        public void StepBase_Workflow()
        {
            new TestStep().Workflow.Should().NotBeNull();
        }

        [Test]
        public void StepBase_Name()
        {
            new TestStep().Name.Should().BeNull();
        }

        [Test]
        public void StepBase_SetName()
        {
            new TestStep().SetName("name").Name.Should().Be("name");
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
