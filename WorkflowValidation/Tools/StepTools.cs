using System;
using System.Xml.Linq;

namespace WorkflowValidation.Tools
{
    public static class StepTools
    {
        public static void Step(Action<StepBuilder> assert)
        {
            var builder = new StepBuilder();
            assert(builder);

            builder.Build()
                .Run();
        }
    }
}
