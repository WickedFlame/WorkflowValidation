using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowValidation.Test
{
    public class StepTests
    {
        [Test]
        public void Step_ctor()
        {
            var act = () => new Step(() => { });
            act.Should().NotThrow();
        }

        [Test]
        public void Step_ctor_Ctx()
        {
            var act = () => new Step(c => { });
            act.Should().NotThrow();
        }

        [Test]
        public void Step_Run()
        {
            var set = false;
            var step = new Step(() =>
            {
                set = true;
            });
            step.Run(new WorkflowContext());

            set.Should().BeTrue();
        }

        [Test]
        public void Step_Run_LogName()
        {
            var ctx = new Mock<WorkflowContext>();
            var step = new Step(() => { })
            {
                Name = "step name"
            };
            step.Run(ctx.Object);

            ctx.Verify(x => x.Log("-> Step: step name"));
        }

        [Test]
        public void Step_Run_Log_NoName()
        {
            var ctx = new Mock<WorkflowContext>();
            var step = new Step(() => { });
            step.Run(ctx.Object);

            ctx.Verify(x => x.Log(It.IsAny<string>()), Times.Never);
        }
    }
}
