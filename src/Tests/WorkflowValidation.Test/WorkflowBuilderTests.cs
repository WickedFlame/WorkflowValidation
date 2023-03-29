using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowValidation.Test
{
    public class WorkflowBuilderTests
    {
        [Test]
        public void WorkflowBuilder_Basic()
        {
            WorkflowBuilder.StartWith(() => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Basic_Then()
        {
            WorkflowBuilder.StartWith(() => { })
                .Then(() => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Basic_Step_Type()
        {
            WorkflowBuilder.StartWith("Step", () => { }).Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void WorkflowBuilder_Basic_Step_Title()
        {
            WorkflowBuilder.StartWith("Step", () => { }).Steps.Single().Name.Should().Be("Step");
        }

        [Test]
        public void WorkflowBuilder_Context()
        {
            WorkflowBuilder.StartWith(c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Context_Step_Type()
        {
            WorkflowBuilder.StartWith("Step", c => { }).Steps.Single().Should().BeOfType<Step>();
        }

        [Test]
        public void WorkflowBuilder_Context_Step_Title()
        {
            WorkflowBuilder.StartWith("Step", c => { }).Steps.Single().Name.Should().Be("Step");
        }

        [Test]
        public void WorkflowBuilder_Then_Context_Type()
        {
            WorkflowBuilder
                .StartWith(() => { })
                .Then(c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Then_Message_Context_Type()
        {
            WorkflowBuilder.StartWith(() => { })
                .Then("Message", c => { }).Should().BeOfType<Workflow>();
        }

        [Test]
        public void WorkflowBuilder_Count_Steps()
        {
            WorkflowBuilder.StartWith(() => { })
                .Then("Message", c => { })
                .Verify(c => {})
                
                .Steps.Should().HaveCount(3);
        }
    }
}
