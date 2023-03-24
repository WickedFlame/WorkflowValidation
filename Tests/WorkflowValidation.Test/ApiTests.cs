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
        public void WorkflowValidation_Api_SubStep_Verify()
        {
            WorkflowBuilder.StartWith(() => { })
                //.Then("Message", c => c.)
                .Run();
        }
    }

    public class WorkflowBuilder
    {
        public static IWorkflow StartWith(Action action)
        {
            throw new NotImplementedException();
        }
    }
}