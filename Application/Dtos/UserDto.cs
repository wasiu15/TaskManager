namespace Application.Dtos;

public class UserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<TaskDto> AssociatedTasks { get; set; }
}