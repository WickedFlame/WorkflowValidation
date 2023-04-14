using System;
using System.Text;

namespace WorkflowValidation
{
    /// <summary>
    /// Provides validations and asserts expressions
    /// </summary>
    public class AssertionProvider
    {
        private readonly WorkflowContext _ctx;

        /// <summary>
        /// Create a new AssertionContext
        /// </summary>
        /// <param name="ctx"></param>
        public AssertionProvider(WorkflowContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Gets the name of the Step
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Assert an expression. Throws a <see cref="WorkflowException"/> if the Assertion fails.
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public bool Assert(Func<bool> assert)
            => Assert(assert, null);

        /// <summary>
        /// Assert an expression. Throws a <see cref="WorkflowException"/> if the Assertion fails.
        /// </summary>
        /// <param name="assert"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        public bool Assert(Func<bool> assert, string name)
        {
            var msg = name ?? Name;

            if (!assert())
            {
                _ctx.Log($"-> Verify: {msg} [Failed]");

                var sb = new StringBuilder();
                sb.AppendLine($"The workflowstep Verify: {msg} [Failed]");
                foreach (var log in _ctx.Logs)
                {
                    sb.AppendLine(log);
                }

                throw new WorkflowException(sb.ToString());
            }

            _ctx.Log($"-> Verify: {msg} [Passed]");

            return true;
        }
    }
}
