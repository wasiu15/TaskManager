using TaskmanagerV2.Domain.Enums;

namespace Application.Dtos;
public class AssignTaskRequest
{
    public AddOrDelete Operation { get; set; }
    public string TaskId { get; set; }
}