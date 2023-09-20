namespace Application.Interfaces.Repositories
{
    public interface IRepositoryManager
    {
        ITaskRepository TaskRepository { get; }
        IProjectRepository ProjectRepository { get; }
        IUserRepository UserRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IProjectTaskRepository ProjectTaskRepository { get; }
        Task SaveAsync();
    }
}
