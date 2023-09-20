using Domain.Exceptions.BaseExceptions;

namespace Domain.Exceptions
{
    public sealed class LimitBadRequestException : BadRequestException
    {
        public LimitBadRequestException(string message) : base(message)
        {
        }
    }
}