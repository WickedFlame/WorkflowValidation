
using System.Diagnostics;

namespace WorkflowValidation.Test
{
    public class WorkflowExtensionsTests
    {
        [Test]
        public void WorkflowExtensions_Then()
        {
            var workflow = new Workflow()
                .Then(() => { });
            
            workflow.Steps.Should().HaveCount(1);
        }

        [Test]
        public void WorkflowExtensions_Then_StepsReference()
        {
            var workflow = new Workflow();
            
            // creates a new copy of the workflow containing a
            workflow.Then(() => { })
                .Then(() => { });

            workflow.Steps.Should().HaveCount(2);
        }

        [Test]
        public void WorkflowExtensions_Then_NewWorkflowReference()
        {
            var workflow = new Workflow();
            workflow.Should().NotBeSameAs(workflow.Then(() => { }));
        }

        [Test]
        public void WorkflowExtensions_Then_Name()
        {
            var workflow = new Workflow()
                .Then("then", () => { });

            workflow.Steps.Single().Name.Should().Be("then");
        }

        [Test]
        public void WorkflowExtensions_Then_Type()
        {
            var workflow = new Workflow()
                .Then(() => { });

            workflow.Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void WorkflowExtensions_Then_WorkflowContext()
        {
            var workflow = new Workflow()
                .Then(c => { });

            workflow.Steps.Should().HaveCount(1);
        }

        [Test]
        public void WorkflowExtensions_Then_WorkflowContext_Name()
        {
            var workflow = new Workflow()
                .Then("then", c => { });

            workflow.Steps.Single().Name.Should().Be("then");
        }

        [Test]
        public void WorkflowExtensions_Then_WorkflowContext_Type()
        {
            var workflow = new Workflow()
                .Then(c => { });

            workflow.Steps.Single().Should().BeOfType<Step>();
        }


        [Test]
        public void WorkflowExtensions_Verify_AssertionProvider()
        {
            var workflow = new Workflow()
                .Verify(a => { });

            workflow.Steps.Should().HaveCount(1);
        }
        
        [Test]
        public void WorkflowExtensions_Verify_AssertionProvider_Type()
        {
            var workflow = new Workflow()
                .Verify(a => { });

            workflow.Steps.Single().Should().BeOfType<AssertionStep>();
        }

        [Test]
        public void WorkflowExtensions_Wait()
        {
            var workflow = new Workflow()
                .Wait(1000);

            workflow.Steps.Should().HaveCount(1);
        }

        [Test]
        public void WorkflowExtensions_Wait_Type()
        {
            var workflow = new Workflow()
                .Wait(1000);

            workflow.Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void WorkflowExtensions_Wait_Run()
        {
            var workflow = new Workflow()
                .Wait(500);

            var sw = Stopwatch.StartNew();

            workflow.Run();

            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeGreaterThan(500);
        }
    }
}
