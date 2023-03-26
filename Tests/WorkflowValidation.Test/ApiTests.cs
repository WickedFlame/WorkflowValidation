using FluentAssertions;
using NUnit.Framework.Internal.Commands;
using System.Xml.Linq;
using static WorkflowValidation.Tools.VerificationTools;
using static WorkflowValidation.Tools.StepTools;

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
        public void WorkflowValidation_Api_SubStep_Context_Verify_Name()
        {
            WorkflowBuilder.StartWith(c =>
                {
                    c.Verify(v => v
                        .SetName("valid")
                        .Assert(() => true)
                    );
                })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Verify_Fail_Name()
        {
            var act = () => WorkflowBuilder.StartWith(c =>
                {
                    c.Verify("invalid", () => false);
                })
                .Run();

            act.Should().Throw<WorkflowException>();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Verify_Builder()
        {
            WorkflowBuilder.StartWith(c =>
                {
                    c.Verify(v => v
                        .SetName("valid")
                        .Assert(() => true)
                    );
                })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Verify_Fail_Builder()
        {
            var act = () => WorkflowBuilder.StartWith(c =>
                {
                    c.Verify(v => v
                        .SetName("invalid")
                        .Assert(() => false));
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

        [Test]
        public void WorkflowValidation_Api_SubStep_Extension_Verify_Action()
        {
            var act = () => WorkflowBuilder.StartWith(() =>
                {
                    Verify(b => b
                        .SetName("Test")
                        .Assert(() => false)
                    );
                })
                .Run();

            act.Should().Throw<WorkflowException>();
        }



        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Builder_FullSetup()
        {
            WorkflowBuilder.StartWith("lorem ipsum", c =>
                {
                    c.Verify(v => v
                        .SetName("Validation step")
                        .Assert(() => true));

                })
                .Then("lorem ipsum then", () =>
                {
                    System.Diagnostics.Debug.WriteLine("This is a step");
                })
                .Run();
        }



        [Test]
        public void WorkflowValidation_Api_SubStep_Extension_FullSetup()
        {
            WorkflowBuilder.StartWith(() =>
                {
                    Step(b => b
                        .SetName("Start Step")
                    );

                    System.Diagnostics.Debug.WriteLine("This is a step");

                    Verify(b => b
                        .SetName("Test")
                        .Assert(() => true)
                    );
                })
                .Run();
        }
    }
}