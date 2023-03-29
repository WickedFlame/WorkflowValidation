
namespace WorkflowValidation
{
    /// <summary>
    /// Extensions for IStep
    /// </summary>
    public static class StepExtensions
    {
        /// <summary>
        /// Set the name of the <see cref="IStep"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IStep SetName(this IStep step, string name)
        {
            step.Name = name;

            return step;
        }
    }
}
