using Domain.Entities;

namespace Application.Dtos;
public class TestingDto
{
    public string NotificationId { get; set; }
    public string TaskId { get; set; }
    public string RecievedUserId { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }


    public static explicit operator TestingDto(Notification notification)
    {
        return new TestingDto
        {
            NotificationId = notification.NotificationId,
            TaskId = notification.TaskId,
            RecievedUserId = notification.RecievedUserId,
            Type = notification.Type,
            Message = notification.Message,
        };
    }
}