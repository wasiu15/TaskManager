using Domain.Exceptions.BaseExceptions;

namespace Domain.Exceptions;

public sealed class CustomBadRequestException : BadRequestException
{
    public CustomBadRequestException(string message) : base(message)
    {
    }
}