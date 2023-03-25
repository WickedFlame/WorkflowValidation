using FluentAssertions;
using static WorkflowValidation.Test.StepTools;

namespace WorkflowValidation.Test
{
    public class ApiTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WorkflowValidation_Api_Basic()
        {
            WorkflowBuilder.StartWith(() => { })
                .Then(() => { })
                .Then( () => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Context()
        {
            WorkflowBuilder.StartWith(() => { })
                .Then(c => { })
                .Then(() => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Message_Context()
        {
            WorkflowBuilder.StartWith(() => { })
                .Then("Message", c => { })
                .Then(() => { })
                .Run();
        }







        [Test]
        public void WorkflowValidation_Api_Context_Verify()
        {
            WorkflowBuilder.StartWith(() => { })
                .Then(c => { })
                .Verify(ac => { })
                .Then(() => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Message_Context_Verify()
        {
            WorkflowBuilder.StartWith(() => { })
                .Then("Message", c => { })
                .Verify(ac => { })
                .Then(() => { })
                .Run();
        }





        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Verify()
        {
            WorkflowBuilder.StartWith(c =>
                {
                    c.Verify(() => true);
                })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Verify_Fail()
        {
            var act = () => WorkflowBuilder.StartWith(c =>
                {
                    c.Verify(() => false);
                })
                .Run();

            act.Should().Throw<WorkflowException>();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Extension_Verify()
        {
            WorkflowBuilder.StartWith(() =>
                {
                    Verify(() => true);
                })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Extension_Verify_Fail()
        {
            var act = () => WorkflowBuilder.StartWith(() =>
                {
                    Verify(() => false);
                })
                .Run();

            act.Should().Throw<WorkflowException>();
        }
    }

    public class WorkflowBuilder
    {
        public static IWorkflow StartWith(Action step)
        {
            var workflow = new Workflow();
            workflow.SetStep(new ActionStep(step));

            return workflow;
        }

        public static IWorkflow StartWith(Action<StepContext> step)
        {
            var workflow = new Workflow();
            workflow.SetStep(new ActionStep(step));
            
            return workflow;
        }
    }

    public static class StepTools
    {
        [AssertionMethod]
        public static StepContext Verify(Func<bool> assert)
        {
            throw new NotImplementedException();
        }
    }
}