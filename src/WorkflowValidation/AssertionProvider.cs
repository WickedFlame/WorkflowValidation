using System;

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
                throw new WorkflowException($"Expected: {msg}");
            }

            _ctx.Log($"-> Verify: {msg} [Passed]");

            return true;
        }
    }
}
