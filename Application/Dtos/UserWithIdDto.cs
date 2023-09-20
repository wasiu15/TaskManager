using Domain.Entities;

namespace Application.Dtos;

public class UserWithIdDto
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string RegisteredDate { get; set; }

    public static explicit operator UserWithIdDto(User user)
    {
        return new UserWithIdDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            RegisteredDate = user.CreatedAt
        };
    }
}