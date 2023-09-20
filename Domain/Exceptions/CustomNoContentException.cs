using Domain.Exceptions.BaseExceptions;

namespace Domain.Exceptions
{
    public class CustomNoContentException : NoContentException
    {
        public CustomNoContentException(string message) : base(message)
        {

        }
    }
}
