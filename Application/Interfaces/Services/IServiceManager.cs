namespace Application.Interfaces.Services
{
    public interface IServiceManager
    {
        ITaskService TaskService { get; }
        IProjectService ProjectService { get; }
        IUserService UserService { get; }
        INotificationService NotificationService { get; }
        IProjectTaskService ProjectTaskService { get; }
    }
}
