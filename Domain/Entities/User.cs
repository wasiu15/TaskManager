using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class User
{
    [Key]
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string CreatedAt { get; set; }
    public DateTime TokenGenerationTime { get; set; }
    public virtual ICollection<UserTask> UserTasks { get; set; }
}