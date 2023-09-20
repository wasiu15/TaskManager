namespace Domain.Entities;

public class Notification
{
    public string NotificationId { get; set; }
    public string TaskId { get; set; }
    public string RecievedUserId { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public string ReadStatus { get; set; }
    public DateTime Time { get; set; }
}

