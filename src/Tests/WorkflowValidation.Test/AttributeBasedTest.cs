using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WorkflowValidation.Tools.StepTools;
using static WorkflowValidation.Tools.WorkflowTools;

namespace WorkflowValidation.Test
{
    public class AttributeBasedTest
    {
        [Test]
        public void Start()
        {
            Workflow<MyTestContext>(ctx =>
                StartWith(() => StepOne(ctx))
                .Then(() => StepOneTwo(ctx))
            );
        }

        [Step("Step 1 starts all")]
        public void StepOne(MyTestContext context)
        {
        }

        [Step("Step 2 starts all")]
        public void StepOneTwo(MyTestContext context)
        {
        }

        public class MyTestContext { }
    }

    public class StepAttribute : Attribute
    {
        public StepAttribute(string description) { }
    }
}
