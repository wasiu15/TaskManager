using Domain.Exceptions.BaseExceptions;

namespace Domain.Exceptions;

public sealed class CustomNotAuthorizedException : NotAuthorizedException
{
    public CustomNotAuthorizedException(string message) : base(message)
    {
    }
}