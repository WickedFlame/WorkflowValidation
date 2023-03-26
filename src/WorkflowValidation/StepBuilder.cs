
namespace WorkflowValidation
{
    public class StepBuilder
    {
        private string _name;

        public StepBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public IWorkflow Build()
        {
            var workflow = new Workflow();

            workflow.SetStep(new Step(() => { })
                .SetName(_name)
            );

            return workflow;
        }


    }
}
