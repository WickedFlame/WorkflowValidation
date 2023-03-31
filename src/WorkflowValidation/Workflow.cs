using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WorkflowValidation
{
    /// <summary>
    /// A workflow containing 1-n steps to run
    /// </summary>
    public class Workflow : IWorkflowStep, IWorkflow
    {
        private readonly List<IStep> _steps = new List<IStep>();

        /// <summary>
        /// 
        /// </summary>
        public Workflow()
        {
        }
        
        /// <summary>
        /// Creates a new workflow based on the Steps and context of the parent workflow
        /// </summary>
        /// <param name="parent"></param>
        public Workflow(IWorkflow parent)
        {
            _steps = parent.Steps as List<IStep> ?? parent.Steps.ToList();
            Context = parent.Context;
        }

        /// <summary>
        /// Gets all the steps that are contained in the workflow
        /// </summary>
        public IEnumerable<IStep> Steps => _steps;

        /// <summary>
        /// Gets the WorkflowContext used in the workflow execution
        /// </summary>
        public WorkflowContext Context { get; set; }

        /// <summary>
        /// Add a Step to the workflow
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public IStep SetStep(IStep step)
        {
            _steps.Add(step);

            return step;
        }

        /// <summary>
        /// Run all steps in the workflow
        /// </summary>
        public IWorkflow Run()
        {
            if (!_steps.Any())
            {
                return this;
            }

            Context ??= new WorkflowContext();
            
            foreach (var step in _steps)
            {
                Context.CurrentStep = step;
                Context.StepNumber++;

                step.Run(Context);
            }

            return this;
        }
    }
}
