using TaskmanagerV2.Domain.Enums;

namespace Application.Dtos;

public class CreateNotificationRequest
{
    public string TaskId { get; set; }
    public NotificationType Type { get; set; }
    public string Message { get; set; }
}