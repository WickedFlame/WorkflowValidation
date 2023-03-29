

namespace WorkflowValidation.Test
{
    public class WorkflowExceptionTests
    {
        [Test]
        public void WorkflowException_Message()
        {
            new WorkflowException("test message").Message.Should().Be("test message");
        }

        [Test]
        public void WorkflowException_Throw_Message()
        {
            try
            {
                throw new WorkflowException("test message");
            }
            catch (WorkflowException e)
            {
                e.Message.Should().Be("test message");
            }
        }
    }
}
