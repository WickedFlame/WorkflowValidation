using static WorkflowValidation.Tools.WorkflowTools;

namespace WorkflowValidation.Test.Tools
{
    public class WorkflowToolsTests
    {
        [Test]
        public void WorkflowTools_WithContext()
        {
            WithContext<WorkflowToolsTestContext>(ctx => new Workflow()).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowTools_WithContext_CheckContext()
        {
            WithContext<WorkflowToolsTestContext>(ctx =>
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
