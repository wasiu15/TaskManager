using TaskmanagerV2.Domain.Enums;

namespace Application.Dtos;

public class StatusAndPriorityRequest
{
    public Status TaskStatus { get; set; }
    public Priority TaskPriority { get; set; }
}