using Domain.Entities;
using TaskmanagerV2.Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        void CreateTask(UserTask task);
        void UpdateTask(UserTask task);
        void DeleteTask(UserTask task);
        Task<UserTask> GetTaskByTaskId(string taskId, bool trackChanges);
        Task<IEnumerable<UserTask>> GetTasksDueThisWeek(DateTime todayDate, DateTime lastDayOfCurrentWeek, bool trackChanges);
        Task<IEnumerable<UserTask>> GetTasks();
        Task<IEnumerable<UserTask>> GetTasksByStatusOrPriority(Status status, Priority priority, bool trackChanges);
        //Task<IEnumerable<UserTask>> GetTasksByProjectId(string projectId, bool trackChanges);
        Task<IEnumerable<UserTask>> GetTasksByUserId(string userId, bool trackChanges);
        Task<IEnumerable<UserTask>> GetAnyUncompletedTaskToDueInTwoDaysWithNotificationStatusOffFalse(bool trackChanges);
        Task<IEnumerable<UserTask>> GetTasksByArrayOfTaskIds(List<string> taskId, bool trackChanges);
    }
}
