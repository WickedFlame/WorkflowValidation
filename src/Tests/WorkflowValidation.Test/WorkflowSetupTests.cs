
namespace WorkflowValidation.Test
{
    public class WorkflowSetupTests
    {
        [Test]
        public void WorkflowSetup_SetDescription()
        {
            var setup = new WorkflowSetup();
            setup.SetDescription("Description");

            setup.Description.Should().Be("Description");
        }

        [Test]
        public void WorkflowSetup_SetDescription_ReturnType()
        {
            var setup = new WorkflowSetup();
            setup.SetDescription("Description").Should().BeOfType<WorkflowSetup>();
        }

        [Test]
        public void WorkflowSetup_Run_Description()
        {
            var setup = new WorkflowSetup
            {
                Description = "Description"
            };

            var ctx = new Mock<WorkflowContext>();
            setup.Run(ctx.Object);
            ctx.Verify(x => x.Log("Workflow: Description"));
        }
    }
}
