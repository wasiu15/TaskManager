using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Services.TokenManager;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.HttpServices.Services;

namespace Infrastructure.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITaskService> _taskService;
        private readonly Lazy<IProjectService> _projectService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<INotificationService> _notificationService;
        private readonly Lazy<IProjectTaskService> _projectTaskService;


        public ServiceManager(IRepositoryManager repositoryManager, ITokenManager tokenManager, IHttpContextAccessor httpContext, IConfiguration configuration, IHttpService httpClient)
        {
            _taskService = new Lazy<ITaskService>(() => new TaskService(repositoryManager, httpContext, configuration, httpClient));
            _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, tokenManager));
            _notificationService = new Lazy<INotificationService>(() => new NotificationService(repositoryManager, httpContext, configuration, httpClient));
            _projectTaskService = new Lazy<IProjectTaskService>(() => new ProjectTaskService(repositoryManager));
        }

        public ITaskService TaskService => _taskService.Value;
        public IProjectService ProjectService => _projectService.Value;
        public IUserService UserService => _userService.Value;
        public INotificationService NotificationService => _notificationService.Value;
        public IProjectTaskService ProjectTaskService => _projectTaskService.Value;
    }
}
