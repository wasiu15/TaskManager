using Domain.Exceptions.BaseExceptions;

namespace Domain.Exceptions;

public class ValidateTokenBadRequestException : BadRequestException
{
    public ValidateTokenBadRequestException(string message) : base(message)
    {
    }
}