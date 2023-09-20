using Domain.Exceptions.BaseExceptions;

namespace Domain.Exceptions;

public sealed class UserTokenBadRequestException : BadRequestException
{
    public UserTokenBadRequestException(string message) : base(message)
    {
    }
}