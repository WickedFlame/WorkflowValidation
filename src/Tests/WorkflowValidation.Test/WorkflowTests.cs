﻿using Polaroider;
using static WorkflowValidation.Tools.WorkflowTools;

namespace WorkflowValidation.Test
{
    public class WorkflowTests
    {
        [Test]
        public void Workflow_ctor()
        {
            var act = () => new Workflow();
            act.Should().NotThrow();
        }

        [Test]
        public void Workflow_ctor_Workflow_Steps()
        {
            var step = new Step(() => { });
            var workflow = new Workflow();
            workflow.SetStep(step);

            new Workflow(workflow).Steps.Single().Should().BeSameAs(step);
        }

        [Test]
        public void Workflow_ctor_Workflow_Context()
        {
            var workflow = new Workflow
            {
                Context = new WorkflowContext()
            };

            new Workflow(workflow).Context.Should().BeSameAs(workflow.Context);
        }

        [Test]
        public void Workflow_ctor_Workflow_WorkflowSetup()
        {
            var workflow = new Workflow
            {
                WorkflowSetup = new WorkflowSetup()
            };

            new Workflow(workflow).WorkflowSetup.Should().BeSameAs(workflow.WorkflowSetup);
        }

        [Test]
        public void Workflow_Steps_Initial()
        {
            new Workflow().Steps.Should().NotBeNull();
        }

        [Test]
        public void Workflow_SetSteps()
        {
            var step = new Step(() => { });
            var workflow = new Workflow();
            workflow.SetStep(step);

            workflow.Steps.Single().Should().BeSameAs(step);
        }

        [Test]
        public void Workflow_WorkflowSetup()
        {
            var workflow = new Workflow();
            workflow.WorkflowSetup.Should().BeNull();
        }

        [Test]
        public void Workflow_Context_Initial()
        {
            new Workflow().Context.Should().BeNull();
        }

        [Test]
        public void Workflow_Run_Steps()
        {
            var executed = false;
            var step = new Step(() => { executed = true; });
            var workflow = new Workflow();
            workflow.SetStep(step);

            workflow.Run();

            executed.Should().BeTrue();
        }

        [Test]
        public void Workflow_Run_SubSteps()
        {
            var executed = false;
            var step = new Step(() => { });
            var workflow = new Workflow();
            workflow.SetStep(step);

            step.Workflow.SetStep(new Step(() => { executed = true; }));

            workflow.Run();

            executed.Should().BeTrue();
        }

        [Test]
        public void Workflow_Run_AssertionStep()
        {
            var wf = Workflow.StartWith("Start", () => { })
                .Then("Then", () => { })
                .Verify("Exception", () => false)
                .Then("Never reached", () => { });

            try
            {
                wf.Run();
            }
            catch (WorkflowException e)
            {
                e.MatchSnapshot();
            }
        }

        [Test]
        public void Workflow_Run_Exception()
        {
            var wf = Workflow.StartWith("Start", () => { })
                .Then("Then", () => { })
                .Then("Exception", () => throw new Exception("exception"))
                .Then("Never reached", () => { });

            try
            {
                wf.Run();
            }
            catch (Exception e)
            {
                e.MatchSnapshot();
            }
        }

        [Test]
        public void Workflow_Run_Exception_Output()
        {
            var stdOut = Console.Out;

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            try
            {
                StartWith("Start", () => { })
                    .Then("Then", () => { })
                    .Then("Exception", () => throw new Exception("exception"))
                    .Then("Never reached", () => { })
                    .Run();
            }
            catch
            {
                // do nothing
            }

            consoleOut.ToString().TrimEnd().MatchSnapshot();

            Console.SetOut(stdOut);
        }
    }
}
