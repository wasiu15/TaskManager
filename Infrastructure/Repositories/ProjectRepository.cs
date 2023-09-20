using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateProject(Project project) => Create(project);
        public void UpdateProject(Project project) => Update(project);
        public void DeleteProject(Project project) => Delete(project);
        public async Task<Project> GetProjectByProjectId(string projectId, bool trackChanges) => await FindByCondition(x => x.Id.Equals(projectId), trackChanges).FirstOrDefaultAsync();
        public async Task<Project> GetProjectByNameAndDescription(string projectName, string projectDescription, bool trackChanges) => await FindByCondition(x => x.Name.Equals(projectName) && x.Description.Equals(projectDescription), trackChanges).FirstOrDefaultAsync();
        public async Task<List<Project>> GetProjects() => await FindAll(false).ToListAsync();
    }
}
