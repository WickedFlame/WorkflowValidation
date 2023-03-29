
namespace WorkflowValidation.Test
{
    public class AssertionStepTests
    {
        [Test]
        public void AssertionStep_ctor()
        {
            var act = () => new AssertionStep(() => true);
            act.Should().NotThrow();
        }

        [Test]
        public void AssertionStep_ctor_2()
        {
            var act = () => new AssertionStep(c => true);
            act.Should().NotThrow();
        }

        [Test]
        public void AssertionStep_Run()
        {
            var step = new AssertionStep(() => true);
            var act = () => step.Run(new WorkflowContext());
            act.Should().NotThrow();
        }

        [Test]
        public void AssertionStep_Run_Fail()
        {
            var step = new AssertionStep(() => false);
            var act = () => step.Run(new WorkflowContext());
            act.Should().Throw<WorkflowException>();
        }
    }
}
