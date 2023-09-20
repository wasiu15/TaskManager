using Application.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ITaskRepository> _taskRepository;
        private readonly Lazy<IProjectRepository> _projectRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<INotificationRepository> _notificationRepository;
        private readonly Lazy<IProjectTaskRepository> _projectTaskRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _taskRepository = new Lazy<ITaskRepository>(() => new TaskRepository(repositoryContext));
            _projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(repositoryContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
            _notificationRepository = new Lazy<INotificationRepository>(() => new NotificationRepository(repositoryContext));
            _projectTaskRepository = new Lazy<IProjectTaskRepository>(() => new ProjectTaskRepository(repositoryContext));
        }

        public ITaskRepository TaskRepository => _taskRepository.Value;
        public IProjectRepository ProjectRepository => _projectRepository.Value;
        public IUserRepository UserRepository => _userRepository.Value;
        public INotificationRepository NotificationRepository => _notificationRepository.Value;
        public IProjectTaskRepository ProjectTaskRepository => _projectTaskRepository.Value;

        public async Task SaveAsync() => _repositoryContext.SaveChanges();
    }
}
