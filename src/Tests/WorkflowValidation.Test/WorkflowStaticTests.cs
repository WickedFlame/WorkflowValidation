
namespace WorkflowValidation.Test
{
    public class WorkflowStaticTests
    {
        [Test]
        public void Workflow_Basic()
        {
            Workflow.StartWith(() => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void Workflow_Basic_Then()
        {
            Workflow.StartWith(() => { })
                .Then(() => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void Workflow_Basic_Step_Type()
        {
            Workflow.StartWith("Step", () => { }).Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void Workflow_Basic_Step_Title()
        {
            Workflow.StartWith("Step", () => { }).Steps.Single().Name.Should().Be("Step");
        }

        [Test]
        public void Workflow_Context()
        {
            Workflow.StartWith(c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void Workflow_Context_Step_Type()
        {
            Workflow.StartWith("Step", c => { }).Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void Workflow_Context_Step_Title()
        {
            Workflow.StartWith("Step", c => { }).Steps.Single().Name.Should().Be("Step");
        }

        [Test]
        public void Workflow_Then_Context_Type()
        {
            Workflow.StartWith(() => { })
                .Then(c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void Workflow_Then_Message_Context_Type()
        {
            Workflow.StartWith(() => { })
                .Then("Message", c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void Workflow_Count_Steps()
        {
            Workflow.StartWith(() => { })
                .Then("Message", c => { })
                .Verify(c => {})
                
                .Steps.Should().HaveCount(3);
        }
        
        [Test]
        public void Workflow_SetupWorkflow_NotSet()
        {
            Workflow.StartWith(() => { })
                .WorkflowSetup.Should().BeNull();
        }

        [Test]
        public void Workflow_SetupWorkflow_Description()
        {
            Workflow.SetupWorkflow(s => s.SetDescription("description"))
                .StartWith(() => { })
                .WorkflowSetup.Description.Should().Be("description");
        }

        [Test]
        public void Workflow_SetupWorkflow_Description_Direct()
        {
            Workflow.SetupWorkflow("description")
                .StartWith(() => { })
                .WorkflowSetup.Description.Should().Be("description");
        }
    }
}
