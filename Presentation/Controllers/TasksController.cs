using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskmanagerV2.Domain.Enums;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public TasksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("getall")]
        public async Task<ActionResult> GetAll()
        {
            var response = await _serviceManager.TaskService.GetAllTasks();
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("getAllTasksForUser")]
        public async Task<ActionResult> GetAllUserTasks()
        {
            var response = await _serviceManager.TaskService.GetAllTasksByUserId();
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("getTaskById")]
        public async Task<ActionResult> GetTaskById([FromQuery] string taskId)
        {
            var response = await _serviceManager.TaskService.GetTaskByTaskId(taskId);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("getByStatusOrPriority")]
        public async Task<ActionResult> GetByStatusOrPriority([FromQuery] Status status, [FromQuery] Priority priority)
        {
            StatusAndPriorityRequest request = new StatusAndPriorityRequest()
            {
                TaskStatus = status,
                TaskPriority = priority
            };
            var response = await _serviceManager.TaskService.GetTaskByPriorityOrStatus(request);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("getTasksDueThisWeek")]
        public async Task<ActionResult> GetTasksDueThisWeek()
        {
            var response = await _serviceManager.TaskService.GetTasksDueThisWeek();
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("createTask")]
        public async Task<ActionResult> AddTask(CreateTaskRequest task)
        {
            var response = await _serviceManager.TaskService.CreateTask(task);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPatch("UpdateTask")]
        public async Task<ActionResult> UpdateTask([FromQuery] string taskId, StatusAndPriorityRequest request)
        {
            var response = await _serviceManager.TaskService.UpdateTask(taskId, request);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("DeleteTask")]
        public async Task<ActionResult> DeleteTask([FromQuery] string taskId)
        {
            var response = await _serviceManager.TaskService.DeleteTask(taskId);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
