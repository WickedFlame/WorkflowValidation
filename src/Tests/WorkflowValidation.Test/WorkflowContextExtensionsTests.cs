
using FluentAssertions.Execution;
using Polaroider;

namespace WorkflowValidation.Test
{
    public class WorkflowContextExtensionsTests
    {
        [Test]
        public void WorkflowContextExtensions_SetStep()
        {
            var executed = false;
            new WorkflowContext()
                .SetStep(a => a.Step(() => executed = true));

            executed.Should().BeTrue();
        }

        [Test]
        public void WorkflowContextExtensions_Verify_Assert()
        {
            var executed = false;
            new WorkflowContext()
                .Verify(() =>
                {
                    executed = true;
                    return executed;
                });

            executed.Should().BeTrue();
        }

        [Test]
        public void WorkflowContextExtensions_Verify_Assert_StepInContext()
        {
            var ctx = new WorkflowContext()
                .Verify("assert", () => true);

            ctx.CurrentStep.Name.Should().Be("assert");
        }

        [Test]
        public void WorkflowContextExtensions_Verify_Assert_Throws()
        {
            var act = () => new WorkflowContext()
                .Verify(() => false);

            act.Should().Throw<WorkflowException>();
        }

        [Test]
        public void WorkflowContextExtensions_Verify_Assert_Name()
        {
            var executed = false;
            new WorkflowContext()
                .Verify("assert", () =>
                {
                    executed = true;
                    return executed;
                });

            executed.Should().BeTrue();
        }

        [Test]
        public void WorkflowContextExtensions_Verify_Assert_Name_Throws()
        {
            var act = () => new WorkflowContext()
                .Verify("assert", () => false);

            act.Should().Throw<WorkflowException>();
        }

        [Test]
        public void WorkflowContextExtensions_Verify_Builder()
        {
            var executed = false;
            new WorkflowContext()
                .Verify(b => b
                    .Assert(() =>
                    {
                        executed = true;
                        return executed;
                    })
                    .SetName("builder")
                );

            executed.Should().BeTrue();
        }

        [Test]
        public void WorkflowContextExtensions_Verify_Builder_Throws()
        {
            var act = () => new WorkflowContext()
                .Verify(b => b
                    .Assert(() => false)
                );

            act.Should().Throw<WorkflowException>();
        }

        [Test]
        public void WorkflowContextExtensions_TraceLog()
        {
            var context = new WorkflowContext()
                .Verify("assert", () => true);

            context.TraceLogs().Trim().Should().Be("-> Verify: assert [Passed]");
        }
    }
}
