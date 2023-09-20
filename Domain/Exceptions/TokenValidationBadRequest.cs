using Domain.Exceptions.BaseExceptions;

namespace Domain.Exceptions;

public sealed class TokenValidationBadRequest : BadRequestException
{
    public TokenValidationBadRequest(string message) : base(message)
    {
    }
}