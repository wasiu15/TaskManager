using Application.Dtos;
using Domain.Entities;
using TaskmanagerV2.Domain.Enums;

namespace Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<GenericResponse<IEnumerable<ProjectResponse>>> GetAllProjects();
        Task<GenericResponse<List<TaskResponse>>> GetProjectByProjectId(string ProjectId);
        Task<GenericResponse<Project>> CreateProject(CreateProjectRequest task);
        Task<GenericResponse<ProjectResponse>> AssignTask(string projectId, AddOrDelete operation, string taskId);
        Task<GenericResponse<ProjectResponse>> UpdateProject(string ProjectIdString, CreateProjectRequest request);
        Task<GenericResponse<ProjectResponse>> DeleteProject(string ProjectId);
    }
}
