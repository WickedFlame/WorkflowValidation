using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowValidation
{
    public static class StepExtensions
    {
        public static IStep SetName(this IStep step, string name)
        {
            step.Name = name;

            return step;
        }
    }
}
