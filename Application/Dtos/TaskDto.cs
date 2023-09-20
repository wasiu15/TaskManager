using Domain.Entities;
using TaskmanagerV2.Domain.Enums;

namespace Application.Dtos;

public class TaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }

    public static explicit operator TaskDto(UserTask task)
    {
        return new TaskDto
        {
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Priority = task.Priority,
            Status = task.Status,
        };
    }
}