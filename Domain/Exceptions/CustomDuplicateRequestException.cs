using Domain.Exceptions.BaseExceptions;

namespace Domain.Exceptions
{
    public class CustomDuplicateRequestException : DuplicateRequestException
    {
        public CustomDuplicateRequestException(string message) : base(message)
        {

        }
    }
}
