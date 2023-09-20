using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ProjectsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("getall")]
        public async Task<ActionResult> GetAll()
        {
            var response = await _serviceManager.ProjectService.GetAllProjects();
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("getProjectById")]
        public async Task<ActionResult> GetProjectById([FromQuery] string projectId)
        {
            var response = await _serviceManager.ProjectService.GetProjectByProjectId(projectId);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("addProject")]
        public async Task<ActionResult> AddProject(CreateProjectRequest project)
        {
            var response = await _serviceManager.ProjectService.CreateProject(project);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPatch("updateProject")]
        public async Task<ActionResult> UpdateProject([FromQuery] string projectId, [FromBody] CreateProjectRequest project)
        {
            var response = await _serviceManager.ProjectService.UpdateProject(projectId, project);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPatch("assignTask")]
        public async Task<ActionResult> AssignProject([FromQuery] string projectId, [FromBody] AssignTaskRequest request)
        {
            var response = await _serviceManager.ProjectService.AssignTask(projectId, request.Operation, request.TaskId);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("DeleteProject")]
        public async Task<ActionResult> DeleteProject([FromQuery] string projectId)
        {
            var response = await _serviceManager.ProjectService.DeleteProject(projectId);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }
    }
}