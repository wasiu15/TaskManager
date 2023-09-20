using Domain.Entities;

namespace Application.Dtos;

public class TaskResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string DueDate { get; set; }
    public string Priority { get; set; }
    public string Status { get; set; }

    public static explicit operator TaskResponse(UserTask userTask)
    {
        return new TaskResponse
        {
            Id = userTask.Id,
            Title = userTask.Title,
            Description = userTask.Description,
            DueDate = userTask.DueDate.ToString(),
            Priority = userTask.Priority.ToString(),
            Status = userTask.Status.ToString(),
        };
    }
}