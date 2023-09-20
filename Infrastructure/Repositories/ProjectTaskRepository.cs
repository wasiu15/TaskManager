using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectTaskRepository : RepositoryBase<ProjectUserTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(RepositoryContext context) : base(context)
        {

        }
        public async Task<IEnumerable<ProjectUserTask>> GetAll() => await FindAll(false).ToListAsync();

        public void CreateProjectTask(ProjectUserTask projectTaskDto) => Create(projectTaskDto);

        public void DeleteProjectTask(ProjectUserTask projectTaskDto) => Delete(projectTaskDto);
        public void DeleteProjectUserTasks(IEnumerable<ProjectUserTask> listOfProjectTasks)
        {
            foreach (var item in listOfProjectTasks)
            {
                Delete(item);
            }
        }
        public void UpdateProjectTask(ProjectUserTask projectTaskDto) => Update(projectTaskDto);

        public async Task<IEnumerable<ProjectUserTask>> GetByTaskId(string taskId, bool trackChanges) => await FindByCondition(x => x.UserTaskId.Equals(taskId), trackChanges).ToListAsync();
        public async Task<IEnumerable<ProjectUserTask>> GetByProjectId(string projectId, bool trackChanges) => await FindByCondition(x => x.ProjectId.Equals(projectId), trackChanges).ToListAsync();
        public async Task<ProjectUserTask> GetByProjectIdAndTaskId(string projectId, string taskId, bool trackChanges) => await FindByCondition(x => x.ProjectId.Equals(projectId) && x.UserTaskId.Equals(taskId), trackChanges).FirstOrDefaultAsync();

        public async void DeleteProjectTaskByProjectId(string projectId)
        {
            var data = await FindByCondition(x => x.ProjectId.Equals(projectId), true).ToListAsync();
            data.ForEach(x => Delete(x));
        }

        public async void DeleteProjectTaskByTaskId(string taskId)
        {
            var data = await FindByCondition(x => x.UserTaskId.Equals(taskId), true).ToListAsync();
            data.ForEach(x => Delete(x));
        }

        public async Task<IEnumerable<string>> GetTaskIdsFromProjectId(string projectId)
        {
            var data = await FindByCondition(x => x.ProjectId.Equals(projectId), true).Select(x => x.UserTaskId).ToListAsync();
            return data;
        }
    }
}
