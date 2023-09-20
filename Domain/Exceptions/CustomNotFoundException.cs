using Domain.Exceptions.BaseExceptions;

namespace Domain.Exceptions;

public class CustomNotFoundException : NotFoundException
{
    //public UserNotFoundException(string userId) : base($"user with {userId} not found!")
    public CustomNotFoundException(string message) : base(message)
    {
    }
}