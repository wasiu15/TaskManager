namespace Domain.Exceptions.BaseExceptions;

public class NotAuthorizedException : Exception
{
    public NotAuthorizedException(string message) : base(message)
    {

    }
}