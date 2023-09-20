namespace Domain.Exceptions.BaseExceptions;

public class BadRequestException : Exception
{
    protected BadRequestException(string message) : base(message) { }
}
