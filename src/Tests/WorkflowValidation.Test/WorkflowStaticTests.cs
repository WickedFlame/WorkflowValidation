
namespace WorkflowValidation.Test
{
    public class WorkflowStaticTests
    {
        [Test]
        public void WorkflowBuilder_Basic()
        {
            Workflow.StartWith(() => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Basic_Then()
        {
            Workflow.StartWith(() => { })
                .Then(() => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Basic_Step_Type()
        {
            Workflow.StartWith("Step", () => { }).Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void WorkflowBuilder_Basic_Step_Title()
        {
            Workflow.StartWith("Step", () => { }).Steps.Single().Name.Should().Be("Step");
        }

        [Test]
        public void WorkflowBuilder_Context()
        {
            Workflow.StartWith(c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Context_Step_Type()
        {
            Workflow.StartWith("Step", c => { }).Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void WorkflowBuilder_Context_Step_Title()
        {
            Workflow.StartWith("Step", c => { }).Steps.Single().Name.Should().Be("Step");
        }

        [Test]
        public void WorkflowBuilder_Then_Context_Type()
        {
            Workflow
                .StartWith(() => { })
                .Then(c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Then_Message_Context_Type()
        {
            Workflow.StartWith(() => { })
                .Then("Message", c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Count_Steps()
        {
            Workflow.StartWith(() => { })
                .Then("Message", c => { })
                .Verify(c => {})
                
                .Steps.Should().HaveCount(3);
        }
    }
}
