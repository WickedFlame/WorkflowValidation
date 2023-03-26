using System;
using System.Collections.Generic;

namespace WorkflowValidation
{
    //https://github.com/TestStack/TestStack.BDDfy
    public class Workflow : IWorkflowStep, IWorkflow
    {
        private readonly List<IStep> _steps = new List<IStep>();
        private readonly WorkflowOptions _options;

        public Workflow()
        {
            _options = new WorkflowOptions();
        }

        public Workflow(WorkflowOptions options)
        {
            _options = options;
        }

        public Workflow(IWorkflow parent)
        {
            _steps.AddRange(parent.Steps);
            _options = parent.Options;
        }

        public IEnumerable<IStep> Steps => _steps;

        public WorkflowOptions Options => _options;

        public IStep SetStep(IStep step)
        {
            _steps.Add(step);

            return step;
        }

        public void Run()
        {
            var ctx = new WorkflowContext();
            System.Diagnostics.Trace.WriteLine(string.Empty);
            System.Diagnostics.Trace.WriteLine("Starting new Test-Workflow");
            
            foreach (var step in _steps)
            {
                ctx.CurrentStep = step;
                ctx.StepNumber++;

                //if(_options.TraceSteps)
                //{
                //    var name = string.IsNullOrEmpty(step.Name) ? nbr.ToString() : step.Name;
                //    ctx.Log($"Step: {name}");
                //}

                step.Run(ctx);

                foreach (var child in step.Workflow.Steps)
                {
                    //if (_options.TraceSteps)
                    //{
                    //    var name = string.IsNullOrEmpty(step.Name) ? nbr.ToString() : step.Name;
                    //    ctx.Log($"Step: {name}");
                    //}

                    child.Run(ctx);
                }
            }

            System.Diagnostics.Trace.WriteLine("End of Test-Workflow");
            System.Diagnostics.Trace.WriteLine(string.Empty);
        }
    }
}
