using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowValidation.Test
{
    public class AssertionStepBuilderTests
    {
        [Test]
        public void AssertionStepBuilder_SetContext()
        {
            var ctx = new WorkflowContext();
            var builder = new AssertionStepBuilder()
                .SetContext(ctx);

            builder.Build().Context.Should().BeSameAs(ctx);
        }

        [Test]
        public void AssertionStepBuilder_SetName()
        {
            var builder = new AssertionStepBuilder()
                .SetName("asserter");

            builder.Build().Steps.Single().Name.Should().Be("asserter");
        }

        [Test]
        public void AssertionStepBuilder_Step()
        {
            var called = false;
            var builder = new AssertionStepBuilder()
                .Step(() =>
                {
                    called = true;
                    return true;
                });

            builder.Build().Run();
            called.Should().BeTrue();
        }

        [Test]
        public void AssertionStepBuilder_Build()
        {
            var builder = new AssertionStepBuilder();
            builder.Build().Steps.Should().NotBeEmpty();
        }
    }
}
