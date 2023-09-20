namespace Domain.Exceptions.BaseExceptions;
public abstract class NoContentException : Exception
{
    public NoContentException(string message) : base(message)
    {

    }
}
