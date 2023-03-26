using FluentAssertions;
using NUnit.Framework.Internal.Commands;
using System.Xml.Linq;
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
    }

    public class WorkflowBuilder
    {
        public static IWorkflow StartWith(Action step)
        {
            var workflow = new Workflow();
            workflow.SetStep(new ActionStep(step, null));

            return workflow;
        }

        public static IWorkflow StartWith(Action<WorkflowContext> step)
        {
            var workflow = new Workflow();
            workflow.SetStep(new ActionStep(step, null));
            
            return workflow;
        }
    }

    public static class StepTools
    {
        [AssertionMethod]
        public static void Verify(Func<bool> assert)
        {
            var builder = new VerificationBuilder();
            builder.Assert(assert);

            builder.Build()
                .Run();
        }

        public static void Verify(Action<VerificationBuilder> assert)
        {
            var builder = new VerificationBuilder();
            assert(builder);

            builder.Build()
                .Run();
        }

        public class VerificationBuilder
        {
            private IWorkflow _workflow = new Workflow();

            private string? _name;
            private IStep? _step;

            public VerificationBuilder SetName(string name)
            {
                _name = name;
                return this;
            }

            public VerificationBuilder Assert(Func<bool> assert)
            {
                _step = new AssertionStep(assert);
                return this;
            }

            public IWorkflow Build()
            {
                if (_step == null)
                {
                    return _workflow;
                }

                _step.Name = string.IsNullOrEmpty(_name) ? string.Empty : _name;

                _workflow.SetStep(_step);

                return _workflow;
            }
        }
    }
}