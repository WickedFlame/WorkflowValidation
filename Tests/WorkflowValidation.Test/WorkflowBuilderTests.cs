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
            WorkflowBuilder.StartWith(() => { })
                .Then(() => { }).Should().BeOfType<Workflow>();
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
