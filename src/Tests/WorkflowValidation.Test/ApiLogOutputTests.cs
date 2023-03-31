using Polaroider;
using static WorkflowValidation.Tools.VerificationTools;
using static WorkflowValidation.Tools.StepTools;
using static WorkflowValidation.Tools.WorkflowTools;

namespace WorkflowValidation.Test
{
    public class ApiLogOutputTests
    {
        [Test]
        public void WorkflowValidation_Api_WorkflowTools_WithContext_FullSetup()
        {
            var stdOut = Console.Out;

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            Workflow<LogTestContext>(ctx =>
                    StartWith("0 Start", () =>
                        {
                            SetStep(b => b
                                .SetName("1 Start Step")
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
                            StartWith("13 Sub of the after start step", () =>
                            {
                                StartWith("14 Last start with", () =>
                                {
                                    Verify(v =>
                                    {
                                        v.SetName("15 last verify");
                                        v.Assert("16 Last assert of last verify", () => true);
                                    });
                                });
                            });
                        })
                )
                .Run();

            consoleOut.ToString().TrimEnd().MatchSnapshot();

            Console.SetOut(stdOut);
        }

        [Test]
        public void WorkflowValidation_Api_WithContext_FullSetup()
        {
            var stdOut = Console.Out;

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            Workflow<LogTestContext>(ctx =>
                    StartWith("0 Start", c =>
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
        }


        public class LogTestContext
        {
        }
    }
}
