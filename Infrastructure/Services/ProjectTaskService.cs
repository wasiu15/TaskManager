using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Exceptions;

namespace Infrastructure.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IRepositoryManager _repository;

        public ProjectTaskService(IRepositoryManager repositoryManager)
        {
            _repository = repositoryManager;
        }

        public async Task<GenericResponse<ProjectTaskDto>> CreateProjectTask(ProjectUserTask projectTask)
        {

            //  CHECK IF REQUIRED INPUTS ARE ENTERED
            if (string.IsNullOrEmpty(projectTask.ProjectId) || string.IsNullOrEmpty(projectTask.UserTaskId))
                throw new CustomBadRequestException("Kindly enter all field");

            //  CHECK IF PROJECT ALREADY EXIST
            var isProjectTaskExist = await _repository.ProjectTaskRepository.GetByProjectIdAndTaskId(projectTask.ProjectId, projectTask.UserTaskId, false);
            if (isProjectTaskExist != null)
                throw new CustomDuplicateRequestException("Foreign key alreay exist");

            _repository.ProjectTaskRepository.CreateProjectTask(projectTask);
            await _repository.SaveAsync();

            return new GenericResponse<ProjectTaskDto>
            {
                IsSuccessful = true,
                ResponseCode = "201",
                ResponseMessage = "Foreign key added successfully",
            };
        }

        public async Task<GenericResponse<ProjectTaskDto>> DeleteProjectTask(ProjectTaskDto projectTask)
        {

            //  CHECK IF REQUIRED INPUTS ARE ENTERED
            if (string.IsNullOrEmpty(projectTask.ProjectId) || string.IsNullOrEmpty(projectTask.UserTaskId))
                throw new CustomBadRequestException("Kindly enter all field");

            //  CHECK IF PROJECT ALREADY EXIST
            var isProjectTaskExist = await _repository.ProjectTaskRepository.GetByProjectIdAndTaskId(projectTask.ProjectId, projectTask.UserTaskId, true);
            if (isProjectTaskExist != null)
                throw new CustomDuplicateRequestException("Foreign key alreay exist");


            _repository.ProjectTaskRepository.DeleteProjectTask(isProjectTaskExist);
            await _repository.SaveAsync();

            return new GenericResponse<ProjectTaskDto>
            {
                IsSuccessful = true,
                ResponseCode = "200",
                ResponseMessage = "Foreign key deleted successfully",
            };
        }

        public async Task<GenericResponse<ProjectUserTask>> GetByProjectIdAndTaskId(ProjectTaskDto projectTask)
        {
            //  CHECK IF REQUIRED INPUTS ARE ENTERED
            if (string.IsNullOrEmpty(projectTask.ProjectId) || string.IsNullOrEmpty(projectTask.UserTaskId))
                throw new CustomBadRequestException("Kindly enter all field");

            var responseFromDb = await _repository.ProjectTaskRepository.GetByProjectIdAndTaskId(projectTask.ProjectId, projectTask.UserTaskId, false);

            if (responseFromDb == null)
                throw new CustomNotFoundException("Foreign key not found");
                
            return new GenericResponse<ProjectUserTask>
            {
                IsSuccessful = true,
                ResponseCode = "200",
                ResponseMessage = "Successfully fetched foreign key from the database",
                Data = responseFromDb
            };
        }

        public async Task<GenericResponse<ProjectTaskDto>> UpdateProjectTask(ProjectTaskDto request)
        {
            var responseFromDb = await _repository.ProjectTaskRepository.GetByProjectIdAndTaskId(request.ProjectId, request.UserTaskId, false);
            if (responseFromDb == null)
                throw new CustomNotFoundException("Foreign key not found");

            _repository.ProjectTaskRepository.UpdateProjectTask(responseFromDb);
            await _repository.SaveAsync();

            return new GenericResponse<ProjectTaskDto>
            {
                IsSuccessful = true,
                ResponseCode = "200",
                ResponseMessage = "Successfully updated foreign key in the database",
            };
        }
    }
}