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


        public class WorkflowToolsTestContext
        {
        }
    }
}
