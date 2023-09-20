namespace Application.Dtos;

public class EmailSenderRequestDto
{
    public string email { get; set; }
    public string subject { get; set; }
    public string message { get; set; }
}
