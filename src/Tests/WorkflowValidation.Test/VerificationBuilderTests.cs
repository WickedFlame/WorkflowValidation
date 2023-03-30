

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
        public void VerificationBuilder_Type()
        {
            new VerificationBuilder()
                .Build().Steps.Single().Should().BeOfType<VerificationStep>();
        }

        [Test]
        public void VerificationBuilder_Assert()
        {
            var builder = new VerificationBuilder()
                .Assert(() => true);

            builder.Build().Steps.Single().Should().BeOfType<AssertionStep>();
        }

        [Test]
        public void VerificationBuilder_Assert_Type()
        {
            new VerificationBuilder()
                .Assert(() => true)
                .Should().BeOfType<AssertionStepBuilder>();
        }

        [Test]
        public void VerificationBuilder_Assert_Name()
        {
            new VerificationBuilder()
                .Assert("assert", () => true)
                .Build().Steps.Single().Name.Should().Be("assert");
        }

        [Test]
        public void VerificationBuilder_Assert_Name_Type()
        {
            new VerificationBuilder()
                .Assert("assert", () => true)
                .Should().BeOfType<AssertionStepBuilder>();
        }

        [Test]
        public void VerificationBuilder_Build_NoStep()
        {
            var builder = new VerificationBuilder();

            builder.Build().Steps.Single().Should().BeOfType<VerificationStep>();
        }

        [Test]
        public void VerificationBuilder_Build_NoName()
        {
            var builder = new VerificationBuilder();

            builder.Build().Steps.Single().Name.Should().BeEmpty();
        }
    }
}
