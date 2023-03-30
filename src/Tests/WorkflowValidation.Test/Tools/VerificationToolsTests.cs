using static WorkflowValidation.Tools.VerificationTools;

namespace WorkflowValidation.Test.Tools
{
    public class VerificationToolsTests
    {
        [Test]
        public void VerificationTools_Verify()
        {
            var executed = false;
            Verify(() =>
            {
                executed = true;
                return true;
            });

            executed.Should().BeTrue();
        }

        [Test]
        public void VerificationTools_Verify_Return()
        {
            Verify(() => true).Should().BeOfType<Workflow>();
        }

        [Test]
        public void VerificationTools_Verify_Return_VerificationSteps()
        {
            Verify(() => true).Steps.Single().Should().BeOfType<VerificationStep>();
        }

        [Test]
        public void VerificationTools_Verify_Return_Steps()
        {
            Verify(() => true).Steps.Single().Workflow.Steps.Single().Should().BeOfType<AssertionStep>();
        }




        [Test]
        public void VerificationTools_Verify_Builder()
        {
            var executed = false;
            Verify(b => b
                .SetName("assert")
                .Assert(() =>
                {
                    executed = true;
                    return true;
                })
            );

            executed.Should().BeTrue();
        }

        [Test]
        public void VerificationTools_Verify_Builder_Return()
        {
            Verify(b => b
                    .SetName("assert")
                    .Assert(() => true)
                )
                .Should().BeOfType<Workflow>();
        }

        [Test]
        public void VerificationTools_Verify_Builder_Return_VerificationStep()
        {
            Verify(b => b
                    .SetName("assert")
                    .Assert(() => true)
                )
                .Steps.Single().Should().BeOfType<VerificationStep>();
        }

        [Test]
        public void VerificationTools_Verify_Builder_Return_Steps()
        {
            Verify(b => b
                    .SetName("assert")
                    .Assert(() => true)
                )
                .Steps.Single().Workflow.Steps.Single().Should().BeOfType<AssertionStep>();
        }

        [Test]
        public void VerificationTools_Verify_Builder_Return_Steps_Name()
        {
            Verify(b => b
                    .SetName("assert")
                    .Assert(() => true)
                )
                .Steps.Single().Name.Should().Be("assert");
        }
    }
}
