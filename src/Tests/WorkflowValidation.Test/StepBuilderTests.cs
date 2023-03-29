using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowValidation.Test
{
    public class StepBuilderTests
    {
        [Test]
        public void StepBuilder_SetContext()
        {
            var ctx = new WorkflowContext();
            var builder = new StepBuilder()
                .SetContext(ctx);

            builder.Build().Context.Should().BeSameAs(ctx);
        }

        [Test]
        public void StepBuilder_SetName()
        {
            var builder = new StepBuilder()
                .SetName("stepname");

            builder.Build().Steps.Single().Name.Should().Be("stepname");
        }

        [Test]
        public void StepBuilder_Step()
        {
            var executed = false;
            var act = () => { executed = true; };
            var builder = new StepBuilder()
                .Step(act);

            builder.Build()
                .Run();

            executed.Should().BeTrue();
        }

        [Test]
        public void StepBuilder_Build()
        {
            var builder = new StepBuilder();
            builder.Build().Should().BeOfType<Workflow>();
        }
    }
}
