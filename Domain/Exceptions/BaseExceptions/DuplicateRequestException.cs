namespace Domain.Exceptions.BaseExceptions;

public class DuplicateRequestException : Exception
{
    public DuplicateRequestException(string message) : base(message)
    {

    }
}