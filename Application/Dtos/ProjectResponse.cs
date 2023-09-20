using Domain.Entities;

namespace Application.Dtos;

public class ProjectResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public static explicit operator ProjectResponse(Project project)
    {
        return new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
        };
    }
}
