using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace WorkflowValidation
{
    /// <summary>
    /// Builder to verify or assert workflow steps.
    /// Adds a <see cref="AssertionStep"/> to the list of steps in the Workflow
    /// </summary>
    public class VerificationBuilder
    {
        private readonly IWorkflow _workflow = new Workflow();

        private string _name;

        private List<AssertionStepBuilder> _steps = new List<AssertionStepBuilder>();

        /// <summary>
        /// Set the <see cref="WorkflowContext"/> for the Verification Step
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public VerificationBuilder SetContext(WorkflowContext context)
        {
            _workflow.Context = context;
            return this;
        }

        /// <summary>
        /// Set the name of the step
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public VerificationBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Assert the result of a step in the workflow.
        /// Creates a <see cref="AssertionStep"/>.
        /// Throws <see cref="WorkflowException"/> if the assertion fails.
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public AssertionStepBuilder Assert(Func<bool> assert)
        {
            var builder = new AssertionStepBuilder()
                .Step(assert);

            _steps.Add(builder);

            return builder;
        }

        /// <summary>
        /// Assert the result of a step in the workflow.
        /// Creates a <see cref="AssertionStep"/>.
        /// Throws <see cref="WorkflowException"/> if the assertion fails.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="assert"></param>
        /// <returns></returns>
        public AssertionStepBuilder Assert(string name, Func<bool> assert)
        {
            var builder = new AssertionStepBuilder()
                .Step(assert)
                .SetName(name);

            _steps.Add(builder);

            return builder;
        }

        /// <summary>
        /// Build the workflow used to assert a step in the workflow
        /// </summary>
        /// <returns></returns>
        public IWorkflow Build()
        {
            var step = new VerificationStep();

            if (!string.IsNullOrEmpty(_name))
            {
                step.SetName(_name);
            }

            foreach (var builder in _steps)
            {
                builder.SetContext(_workflow.Context);
                foreach (var assert in builder.Build().Steps)
                {
                    if (string.IsNullOrEmpty(assert.Name))
                    {
                        assert.SetName(_name);
                    }

                    step.Workflow.SetStep(assert);
                }
            }

            _workflow.SetStep(step);

            return _workflow;
        }
    }
}
