

namespace WorkflowValidation.Test
{
    public class VerificationBuilderTests
    {
        [Test]
        public void VerificationBuilder_SetContext()
        {
            var ctx = new WorkflowContext();
            var builder = new VerificationBuilder()
                .SetContext(ctx);

            builder.Build().Context.Should().BeSameAs(ctx);
        }

        [Test]
        public void VerificationBuilder_SetName()
        {
            var builder = new VerificationBuilder()
                .SetName("verify");

            builder.Build().Steps.Single().Name.Should().Be("verify");
        }

        [Test]
        public void VerificationBuilder_Assert()
        {
            var builder = new VerificationBuilder()
                .Assert(() => true);

            builder.Build().Steps.Single().Should().BeOfType<AssertionStep>();
        }

        [Test]
        public void VerificationBuilder_Build_NoStep()
        {
            var builder = new VerificationBuilder();

            builder.Build().Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void VerificationBuilder_Build_NoName()
        {
            var builder = new VerificationBuilder();

            builder.Build().Steps.Single().Name.Should().BeEmpty();
        }
    }
}
