namespace Application.Dtos;
public class NotificationDto
{
    public string TaskId { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public string ReadStatus { get; set; }
    public DateTime Time { get; set; }
}