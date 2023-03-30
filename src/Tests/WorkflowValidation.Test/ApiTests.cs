using FluentAssertions;
using NUnit.Framework.Internal.Commands;
using System.Xml.Linq;
using static WorkflowValidation.Tools.VerificationTools;
using static WorkflowValidation.Tools.StepTools;
using static WorkflowValidation.Tools.WorkflowTools;

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
                .Then(() => { })
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
            WorkflowBuilder.StartWith("Start", c => { })
                .Then("Then 1", c => { })
                .Then("Then 2", c => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Message_NoContext()
        {
            WorkflowBuilder.StartWith("Start", () => { })
                .Then("Then 1", () => { })
                .Then("Then 2", () => { })
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
        public void WorkflowValidation_Api_Verify_MultipleAsserts()
        {
            var cnt = 0;
            WorkflowBuilder.StartWith(c =>
                {
                    c.Verify(v =>
                    {
                        v.SetName("verify name");
                        v.Assert("one", () =>
                        {
                            cnt++;
                            return true;
                        });
                        v.Assert(() =>
                        {
                            cnt++;
                            return true;
                        }).SetName("two");
                    });
                })
                .Run();

            cnt.Should().Be(2);
        }

        [Test]
        public void WorkflowValidation_Api_Verify_MultipleAsserts_RootVerify()
        {
            WorkflowBuilder.StartWith(() => { })
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
                    c.SetStep(s => s
                        .SetName("lorem ipsum sub config")
                    );
                    c.Verify(v => v
                        .SetName("Validation step")
                        .Assert(() => true)
                    );

                })
                .Then("lorem ipsum then", () =>
                {
                    System.Diagnostics.Debug.WriteLine("This is a step");
                })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Fluent_FullSetup()
        {
            WorkflowBuilder.StartWith(c =>
                    c.SetStep(s => s
                            .SetName("lorem ipsum")
                            .Step(() => System.Diagnostics.Debug.WriteLine("This is a step"))
                        )
                        .Verify(v => v
                            .SetName("Validation step")
                            .Assert(() => true)
                        )
                )
                .Then(c =>
                    c.SetStep(s => s
                            .SetName("lorem ipsum then")
                            .Step(() => System.Diagnostics.Debug.WriteLine("This is a step"))
                        )
                        .Verify(v => v
                            .SetName("Validation step")
                            .Assert(() => true)
                        )
                )
                .Run();
        }


        [Test]
        public void WorkflowValidation_Api_StepTools_FullSetup()
        {
            WorkflowBuilder.StartWith(() =>
                {
                    SetStep(b => b
                        .SetName("Start Step")
                        .Step(() => System.Diagnostics.Debug.WriteLine("This is a step"))
                    );

                    System.Diagnostics.Debug.WriteLine("Some info");

                    Verify(b => b
                        .SetName("Test")
                        .Assert(() => true)
                    );
                })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_WorkflowTools_FullSetup()
        {
            StartWith(() =>
                {
                    SetStep(b => b
                        .SetName("Start Step")
                        .Step(() => System.Diagnostics.Debug.WriteLine("This is a step"))
                    );

                    Verify(b => b
                        .SetName("Test")
                        .Assert(() => true)
                    );
                })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_WorkflowTools_WithContext_FullSetup()
        {
            Workflow<WorkflowTestContext>(ctx =>
                    StartWith(() =>
                        {
                            SetStep(b => b
                                .SetName("Start Step")
                                .Step(() => System.Diagnostics.Debug.WriteLine("This is a step"))
                            );

                            Verify(b => b
                                .SetName("Test")
                                .Assert(() => true)
                            );
                        })
                        .Then(() => { })
                )
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_WorkflowTools_Simple_FullSetup()
        {
            Workflow<WorkflowTestContext>(ctx =>
                    StartWith("Start", () => { })
                        .Then("Then", () => { })
                )
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_WorkflowTools_Simple_Context_FullSetup()
        {
            Workflow<WorkflowTestContext>(ctx =>
                    StartWith("Start", c => { })
                        .Then("Then", c => { })
                )
                .Run();
        }

        public class WorkflowTestContext
        {
        }
    }
}