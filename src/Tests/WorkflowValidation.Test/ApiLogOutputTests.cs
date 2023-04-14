using Polaroider;
using static WorkflowValidation.Tools.VerificationTools;
using static WorkflowValidation.Tools.StepTools;
using static WorkflowValidation.Tools.WorkflowTools;

namespace WorkflowValidation.Test
{
    [UpdateSnapshot]
    public class ApiLogOutputTests
    {
        [Test]
        public void WorkflowValidation_Api_WorkflowTools_WithContext_FullSetup()
        {
            var stdOut = Console.Out;

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            var wf = Workflow<LogTestContext>(ctx =>
                    SetupWorkflow(s => s.SetDescription("This is a Workflow"))
                        .StartWith("0 Start", s =>
                        {
                            SetStep(b => b
                                .SetName("1 Start Step")
                                .SetContext(s)
                                .Step(c =>
                                {
                                    SetStep("2 substep of start step", s =>
                                    {
                                        Console.WriteLine("->       3 Content of substep of start step");

                                        SetStep("4 startwith of substep of start step", ss =>
                                        {
                                        });
                                    });

                                    SetStep("5 Sub of start step", s =>
                                    {
                                        s.SetContext(c);
                                    });
                                })
                            );

                            Verify(b => b
                                .SetName("10 Verify after start step")
                                .Assert("11 Assert of verify after start step", () => true)
                            );
                        })
                        .Then("12 Then after start step", () =>
                        {
                            // don't use a startwith inside a already started workflow.
                            // new workflows have to be executed with Run()
                            StartWith("13 Sub of the after start step", () =>
                            {
                                // don't use a startwith inside a already starte workflow.
                                // new workflows have to be executed with Run()
                                StartWith("14 Last start with", () =>
                                {
                                    Verify(v =>
                                    {
                                        v.SetName("15 last verify");
                                        v.Assert("16 Last assert of last verify", () => true);
                                    });

                                    // this will never be reached!
                                    throw new Exception();
                                });
                            });
                        })
                )
                .Run();

            consoleOut.ToString().TrimEnd().MatchSnapshot();

            Console.SetOut(stdOut);

            wf.Context.Logs.MatchSnapshot(() => new { Name = "logs" });
        }

        [Test]
        public void WorkflowValidation_Api_WithContext_FullSetup()
        {
            var stdOut = Console.Out;

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            var wf = Workflow<LogTestContext>(ctx =>
                    SetupWorkflow(s => s.SetDescription("This is a Workflow"))
                        .StartWith("0 Start", c =>
                        {
                            c.SetStep(b => b
                                .SetName("1 Start Step")
                                .Step(d =>
                                {
                                    d.SetStep("2 substep of start step", s =>
                                        s.Step(r => Console.WriteLine("->       3 Content of substep of start step"))
                                    );

                                    d.SetStep("4 Sub of start step", s => { });
                                })
                            );

                            c.Verify(b => b
                                .SetName("5 Verify after start step")
                                .Assert("6 Assert of verify after start step", () => true)
                            );
                        })
                        .Then("7 Then after start step", r =>
                        {
                            r.SetStep("8 Sub of the after start step", s =>
                            {
                                s.SetName("9 overrides 8");
                                s.Step(t =>
                                {
                                    //t.
                                    r.Verify(v =>
                                    {
                                        v.SetName("10 last verify");
                                        v.Assert("11 Last assert of last verify", () => true);
                                    });
                                });
                            });
                        })
                )
                .Run();

            consoleOut.ToString().TrimEnd().MatchSnapshot();

            Console.SetOut(stdOut);

            wf.Context.Logs.MatchSnapshot(() => new { Name = "logs" });
        }

        [Test]
        public void LogOutput_Default()
        {
            var stdOut = Console.Out;

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            var wf = Workflow<LogTestContext>(ctx =>
                    SetupWorkflow(s => s.SetDescription("This is a Workflow"))
                        .StartWith("1 Start", c =>
                        {
                            c.SetStep(b => b
                                .SetName("2 Start Step")
                                .Step(d =>
                                {
                                    d.SetStep("3 substep of start step", s =>
                                        s.Step(r => { })
                                    );

                                    d.SetStep("4 Sub of start step", s => { });
                                })
                            );

                            c.Verify(b => b
                                .SetName("5 Verify after start step")
                                .Assert("6 Assert of verify after start step", () => true)
                            );
                        })
                        .Then("7 Then after start step", ()=> { })
                )
                .Run();

            consoleOut.ToString().TrimEnd().MatchSnapshot();

            Console.SetOut(stdOut);

            wf.Context.Logs.MatchSnapshot(() => new { Name = "logs" });
        }

        [Test]
        public void LogOutput_Validation_Fail()
        {
            var stdOut = Console.Out;

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);
            
            var wf = Workflow<LogTestContext>(ctx =>
                    SetupWorkflow(s => s.SetDescription("This is a Workflow"))
                        .StartWith("1 Start", c =>
                        {
                            c.SetStep(b => b
                                .SetName("2 Start Step")
                                .Step(d =>
                                {
                                    d.SetStep("3 substep of start step", s =>
                                        s.Step(r => { })
                                    );

                                    d.SetStep("4 Sub of start step", s => { });
                                })
                            );

                            c.Verify(b => b
                                .SetName("5 Verify after start step")
                                .Assert("6 Assert of verify after start step", () => false)
                            );
                        })
                        .Then("7 Then after start step", () => { })
                );
            try
            {
                wf.Run();
            }
            catch(WorkflowException e)
            {
                e.MatchSnapshot();
            }

            consoleOut.ToString().TrimEnd().MatchSnapshot(() => new { Name = "output" });

            Console.SetOut(stdOut);

            wf.Context.Logs.MatchSnapshot(() => new { Name = "logs" });
        }


        public class LogTestContext
        {
        }
    }
}
