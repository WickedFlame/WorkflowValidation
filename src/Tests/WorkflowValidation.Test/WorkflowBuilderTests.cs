
namespace WorkflowValidation.Test
{
    public class WorkflowBuilderTests
    {
        [Test]
        public void WorkflowBuilder_Basic()
        {
            var builder = new WorkflowBuilder();
            builder.StartWith(() => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Basic_Then()
        {
            var builder = new WorkflowBuilder();
            builder.StartWith(() => { })
                .Then(() => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Basic_Step_Type()
        {
            var builder = new WorkflowBuilder();
            builder.StartWith("Step", () => { }).Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void WorkflowBuilder_Basic_Step_Title()
        {
            var builder = new WorkflowBuilder();
            builder.StartWith("Step", () => { }).Steps.Single().Name.Should().Be("Step");
        }

        [Test]
        public void WorkflowBuilder_Context()
        {
            var builder = new WorkflowBuilder();
            builder.StartWith(c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Context_Step_Type()
        {
            var builder = new WorkflowBuilder();
            builder.StartWith("Step", c => { }).Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void WorkflowBuilder_Context_Step_Title()
        {
            var builder = new WorkflowBuilder();
            builder.StartWith("Step", c => { }).Steps.Single().Name.Should().Be("Step");
        }

        [Test]
        public void WorkflowBuilder_Then_Context_Type()
        {
            var builder = new WorkflowBuilder();
            builder.StartWith(() => { })
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
            var builder = new WorkflowBuilder();
            builder.StartWith(() => { })
                .Then("Message", c => { })
                .Verify(c => {})
                
                .Steps.Should().HaveCount(3);
        }

        [Test]
        public void WorkflowBuilder_SetupWorkflow_Reference()
        {
            var builder = new WorkflowBuilder();
            builder.SetupWorkflow(s => { }).Should().BeSameAs(builder);
        }

        [Test]
        public void WorkflowBuilder_SetupWorkflow_NotSet()
        {
            var builder = new WorkflowBuilder();
            builder.StartWith(() => { })
                .WorkflowSetup.Should().BeNull();
        }

        [Test]
        public void WorkflowBuilder_SetupWorkflow_Description()
        {
            var builder = new WorkflowBuilder();
            builder.SetupWorkflow(s => s.SetDescription("description"))
                .StartWith(() => { })
                .WorkflowSetup.Description.Should().Be("description");
        }

        [Test]
        public void WorkflowBuilder_SetupWorkflow_Description_Direct()
        {
            var builder = new WorkflowBuilder();
            builder.SetupWorkflow("description")
                .StartWith(() => { })
                .WorkflowSetup.Description.Should().Be("description");
        }
    }
}
