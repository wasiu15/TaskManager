namespace Application.Dtos;

public class ProjectDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<TaskDto> AssociatedTasks { get; set; }
}