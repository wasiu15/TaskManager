using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IProjectTaskService
    {
        Task<GenericResponse<ProjectTaskDto>> CreateProjectTask(ProjectUserTask projectTask);
        Task<GenericResponse<ProjectTaskDto>> DeleteProjectTask(ProjectTaskDto projectTask);
        Task<GenericResponse<ProjectTaskDto>> UpdateProjectTask(ProjectTaskDto projectTask);
        Task<GenericResponse<ProjectUserTask>> GetByProjectIdAndTaskId(ProjectTaskDto projectTask);
    }
}
