using static WorkflowValidation.Tools.WorkflowTools;

namespace WorkflowValidation.Test.Tools
{
    public class WorkflowToolsTests
    {
        [Test]
        public void WorkflowTools_WithContext()
        {
            Workflow<WorkflowToolsTestContext>(ctx => new Workflow()).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowTools_WithContext_CheckContext()
        {
            Workflow<WorkflowToolsTestContext>(ctx =>
            {
                ctx.Should().BeOfType<WorkflowToolsTestContext>();
                return new Workflow();
            });
        }

        [Test]
        public void WorkflowTools_StartWith()
        {
            var workflow = StartWith(() => { });
            workflow.Steps.Should().HaveCount(1);
        }

        [Test]
        public void WorkflowTools_StartWith_Type()
        {
            var workflow = StartWith(() => { });
            workflow.Should().BeOfType<Workflow>();
        }


        [Test]
        public void WorkflowTools_StartWith_Name()
        {
            var workflow = StartWith("start", () => { });
            workflow.Steps.Single().Name.Should().Be("start");
        }

        [Test]
        public void WorkflowTools_StartWith_Name_Type()
        {
            var workflow = StartWith("start", () => { });
            workflow.Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowTools_StartWith_Context()
        {
            var workflow = StartWith(c => { });
            workflow.Steps.Should().HaveCount(1);
        }

        [Test]
        public void WorkflowTools_StartWith_Context_Type()
        {
            var workflow = StartWith(c => { });
            workflow.Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowTools_StartWith_Context_Name()
        {
            var workflow = StartWith("start", c => { });
            workflow.Steps.Single().Name.Should().Be("start");
        }

        [Test]
        public void WorkflowTools_StartWith_Context_Name_Type()
        {
            var workflow = StartWith("start", c => { });
            workflow.Should().BeOfType<Workflow>();
        }


        public class WorkflowToolsTestContext
        {
        }
    }
}
