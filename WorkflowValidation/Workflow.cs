using System;
using System.Collections.Generic;

namespace WorkflowValidation
{
    //https://github.com/TestStack/TestStack.BDDfy
    public class Workflow
    {
        private readonly List<IStep> _steps = new List<IStep>();

        public IEnumerable<IStep> Steps => _steps;

        public Workflow Step(IStep step)
        {
            _steps.Add(step);

            return this;
        }

        public void Start()
        {
            var ctx = new WorkflowContext();
            System.Diagnostics.Trace.WriteLine(string.Empty);
            System.Diagnostics.Trace.WriteLine("Starting new Test-Workflow");

            foreach (var step in _steps)
            {
                ctx.CurrentStep = step;
                step.Run(ctx);

                foreach (var child in step.Workflow.Steps)
                {
                    child.Run(ctx);
                }
            }

            System.Diagnostics.Trace.WriteLine("End of Test-Workflow");
            System.Diagnostics.Trace.WriteLine(string.Empty);
        }
    }
}
