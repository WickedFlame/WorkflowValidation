using Moq;

namespace WorkflowValidation.Test
{
    public class AssertionProviderTests
    {
        [Test]
        public void AssertionProvider_ctor()
        {
            var act = () => new AssertionProvider(new WorkflowContext());
            act.Should().NotThrow();
        }

        [Test]
        public void AssertionProvider_Name()
        {
            new AssertionProvider(new WorkflowContext()).Name.Should().BeNullOrEmpty();
        }

        [Test]
        public void AssertionProvider_Assert()
        {
            var ctx = new AssertionProvider(new WorkflowContext());
            var act = () => ctx.Assert(() => true);
            act.Should().NotThrow();
        }

        [Test]
        public void AssertionProvider_Assert_False()
        {
            var ctx = new AssertionProvider(new WorkflowContext());
            var act = () => ctx.Assert(() => false);
            act.Should().Throw<WorkflowException>();
        }

        [Test]
        public void AssertionProvider_Assert_Name()
        {
            var ctx = new AssertionProvider(new WorkflowContext());
            var act = () => ctx.Assert(() => true, "test");
            act.Should().NotThrow();
        }

        [Test]
        public void AssertionProvider_Assert_Name_False()
        {
            var ctx = new AssertionProvider(new WorkflowContext());
            var act = () => ctx.Assert(() => false, "test");
            act.Should().Throw<WorkflowException>();
        }





        [Test]
        public void AssertionProvider_Assert_Log()
        {
            var wf = new Mock<WorkflowContext>();
            var ctx = new AssertionProvider(wf.Object);

            ctx.Assert(() => true, "test");

            wf.Verify(x => x.Log("-> Verify: test [Passed]"));
        }

        [Test]
        public void AssertionProvider_Assert_Log_False()
        {
            var wf = new Mock<WorkflowContext>();
            var ctx = new AssertionProvider(wf.Object);
            try
            {
                ctx.Assert(() => false, "test");
            }
            catch
            {
                // do nothing
            }

            wf.Verify(x => x.Log("-> Verify: test [Failed]"));
        }

        [Test]
        public void AssertionProvider_Assert_Log_Name()
        {
            var wf = new Mock<WorkflowContext>();
            var ctx = new AssertionProvider(wf.Object)
            {
                Name = "name"
            };

            ctx.Assert(() => true);

            wf.Verify(x => x.Log("-> Verify: name [Passed]"));
        }

        [Test]
        public void AssertionProvider_Assert_Log_Name_False()
        {
            var wf = new Mock<WorkflowContext>();
            var ctx = new AssertionProvider(wf.Object)
            {
                Name = "name"
            };

            try
            {
                ctx.Assert(() => false);
            }
            catch
            {
                // do nothing
            }

            wf.Verify(x => x.Log("-> Verify: name [Failed]"));
        }

        [Test]
        public void AssertionProvider_Assert_Log_Param_OverProperty()
        {
            var wf = new Mock<WorkflowContext>();
            var ctx = new AssertionProvider(wf.Object)
            {
                Name = "property"
            };

            ctx.Assert(() => true, "param");

            wf.Verify(x => x.Log("-> Verify: param [Passed]"));
        }

        [Test]
        public void AssertionProvider_Assert_Log_Param_OverProperty_False()
        {
            var wf = new Mock<WorkflowContext>();
            var ctx = new AssertionProvider(wf.Object)
            {
                Name = "property"
            };

            try
            {
                ctx.Assert(() => false, "param");
            }
            catch
            {
                // do nothing
            }

            wf.Verify(x => x.Log("-> Verify: param [Failed]"));
        }

        [Test]
        public void AssertionProvider_Assert_Message()
        {
            var ctx = new AssertionProvider(new Mock<WorkflowContext>().Object);
            try
            {
                ctx.Assert(() => false, "this is a test");
            }
            catch(WorkflowException e)
            {
                e.Message.Should().Be($"The workflowstep Verify: this is a test [Failed]{Environment.NewLine}");
            }
        }
    }
}
