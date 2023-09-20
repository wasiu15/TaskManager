namespace Domain.Entities;

public class ProjectUserTask
{
    public string ProjectId { get; set; }
    public Project Project { get; set; }

    public string UserTaskId { get; set; }
    public UserTask UserTask { get; set; }
}

