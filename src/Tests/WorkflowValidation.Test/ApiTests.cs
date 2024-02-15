using FluentAssertions;
using NUnit.Framework.Internal.Commands;
using System.Xml.Linq;
using static WorkflowValidation.Tools.VerificationTools;
using static WorkflowValidation.Tools.StepTools;
using static WorkflowValidation.Tools.WorkflowTools;
using Polaroider;

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
            Workflow.StartWith(() => { })
                .Then(() => { })
                .Then(() => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Context()
        {
            Workflow.StartWith(() => { })
                .Then(c => { })
                .Then(() => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Message_Context()
        {
            Workflow.StartWith("Start", c => { })
                .Then("Then 1", c => { })
                .Then("Then 2", c => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Message_NoContext()
        {
            Workflow.StartWith("Start", () => { })
                .Then("Then 1", () => { })
                .Then("Then 2", () => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Setup_Description()
        {
            Workflow.SetupWorkflow(s => s.SetDescription("Description"))
                .StartWith("Start", () => { })
                .Then("Then 1", () => { })
                .Then("Then 2", () => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Setup_DescriptionOnly()
        {
            Workflow.SetupWorkflow("Description")
                .StartWith("Start", () => { })
                .Then("Then 1", () => { })
                .Then("Then 2", () => { })
                .Run();
        }







        [Test]
        public void WorkflowValidation_Api_Context_Verify()
        {
            Workflow.StartWith(() => { })
                .Then(c => { })
                .Verify(ac => { })
                .Then(() => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Message_Context_Verify()
        {
            Workflow.StartWith(() => { })
                .Then("Message", c => { })
                .Verify(ac => { })
                .Then(() => { })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_Verify_MultipleAsserts()
        {
            var cnt = 0;
            Workflow.StartWith(c =>
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
            Workflow.StartWith(() => { })
                .Verify(ac => { })
                .Then(() => { })
                .Run();
        }





        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Verify()
        {
            Workflow.StartWith(c =>
                {
                    c.Verify(() => true);
                })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Verify_Fail()
        {
            var act = () => Workflow.StartWith(c =>
                {
                    c.Verify(() => false);
                })
                .Run();

            act.Should().Throw<WorkflowException>();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Verify_Name()
        {
            Workflow.StartWith(c =>
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
            var act = () => Workflow.StartWith(c =>
                {
                    c.Verify("invalid", () => false);
                })
                .Run();

            act.Should().Throw<WorkflowException>();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Verify_Builder()
        {
            Workflow.StartWith(c =>
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
            var act = () => Workflow.StartWith(c =>
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
            Workflow.StartWith(() =>
                {
                    Verify(() => true);
                })
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Extension_Verify_Fail()
        {
            var act = () => Workflow.StartWith(() =>
                {
                    Verify(() => false);
                })
                .Run();

            act.Should().Throw<WorkflowException>();
        }

        [Test]
        public void WorkflowValidation_Api_SubStep_Extension_Verify_Action()
        {
            var act = () => Workflow.StartWith(() =>
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
        public void WorkflowValidation_Api_EachStepRun()
        {
            var cnt = 0;
            Workflow.StartWith("One", () => cnt++)
                .Then("Two", () => cnt++)
                .Wait(1)
                .Then("Three", () => cnt++)
                .Run();

            cnt.Should().Be(3);
        }

        [Test]
        public void WorkflowValidation_Api_Tool_EachStepRun()
        {
            var cnt = 0;
            StartWith("One", () => cnt++)
                .Then("Two", () => cnt++)
                .Wait(1)
                .Then("Three", () => cnt++)
                .Run();

            cnt.Should().Be(3);
        }



        [Test]
        public void WorkflowValidation_Api_SubStep_Context_Builder_FullSetup()
        {
            Workflow.StartWith("lorem ipsum", c =>
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
            Workflow.StartWith(c =>
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
            Workflow.StartWith(() =>
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
                SetStep(b => b
                    .SetName("Start Step")
                    .Step(() => System.Diagnostics.Debug.WriteLine("This is a step"))
                )
                .Verify(b => b
                    .SetName("Test")
                    .Assert(() => true)
                ))
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_WorkflowTools_WithContext_FullSetup()
        {
            Workflow<WorkflowTestContext>(ctx =>
                    StartWith(() =>
                            SetStep(b => b
                                .SetName("Start Step")
                                .Step(() => System.Diagnostics.Debug.WriteLine("This is a step"))
                            )
                            .Verify(b => b
                                .SetName("Test")
                                .Assert(() => true)
                            )
                        )
                        .Then(() => { })
                )
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_WorkflowTools_Simple_FullSetup()
        {
            Workflow<WorkflowTestContext>(ctx =>
                    StartWith("Start", () =>
                        {
                            SetStep("First step in the start", () =>
                            {
                                Console.WriteLine("Some action");
                            });
                        })
                        .Then("Then", () =>
                        {
                            SetStep("First step in the Then step", () =>
                            {
                                Console.WriteLine("Some action");
                            });
                        })
                )
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_WorkflowTools_Simple_FullSetup_PassContext()
        {
            Workflow<WorkflowTestContext>(ctx =>
                    StartWith("Start", c1 =>
                        {
                            SetStep("First step in the start", c1, () =>
                            {
                                Console.WriteLine("Some action");
                            });

                            Verify(v => v
                                .SetName("Verify of first check")
                                .SetContext(c1)
                                .Assert(() => true)
                            );
                        })
                        .Then("Then", c1 =>
                        {
                            SetStep("First step in the Then step", c1, () =>
                            {
                                Console.WriteLine("Some action");
                            });
                        })
                )
                .Run().Context.Logs.MatchSnapshot();
        }

        [Test]
        public void WorkflowValidation_Api_WorkflowTools_Workflow_SetupWorkflow_FullSetup()
        {
            Workflow<WorkflowTestContext>(ctx =>
                    SetupWorkflow(w => w.SetDescription("Test the full api of the workflow tools"))
                        .StartWith("Start", c => { })
                        .Then("Then", c => { })
                )
                .Run();
        }

        [Test]
        public void WorkflowValidation_Api_WorkflowTools_Workflow_Extended_FullSetup()
        {
            Workflow<WorkflowTestContext>((ctx, wf) =>
                    wf.SetupWorkflow(w => w.SetDescription("Test the full api of the workflow tools"))
                        .StartWith("Start", c => { })
                        .Then("Then", c => { })
                )
                .Run();
        }

        public class WorkflowTestContext
        {
        }
    }
}