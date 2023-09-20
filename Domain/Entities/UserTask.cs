using System.ComponentModel.DataAnnotations;
using TaskmanagerV2.Domain.Enums;

namespace Domain.Entities;

//  I NAMED THIS USERTASK AND NOT JUST TASK BECAUSE TASK IS A RESERVED WORD
public class UserTask
{
    [Key]
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public bool IsReminderSent { get; set; }
    public Status Status { get; set; }
    public string UserId { get; set; }
    public ICollection<Project> Projects { get; set; }
}