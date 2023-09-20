namespace Application.Dtos;
public class LoginResponse
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string CreatedAt { get; set; }
    public DateTime? TokenGenerationTime { get; set; }
}