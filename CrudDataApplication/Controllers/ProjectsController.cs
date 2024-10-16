using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        private readonly ILoggerRepository<ProjectsController> _loggerRepository;

        public ProjectsController(IProjectsService projectsService, ILoggerRepository<ProjectsController> loggerRepository)
        {
            _projectsService = projectsService;
            _loggerRepository = loggerRepository;
        }

        [HttpGet("GetAllProjectsAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllProjectsAsync()
        {
            try
            {
                var projects = await _projectsService.GetAllProjectsAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddProjectsAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddProjectsAsync(ProjectsDto projectsDto)
        {
            try
            {
                return await _projectsService.AddProjectsAsync(projectsDto);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetProjectsByIdAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetProjectsByIdAsync(int projectId)
        {
            try
            {
                var projects = await _projectsService.GetProjectsByIdAsync(projectId);
                if (projects == null)
                {
                    return NotFound();
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProjectsAsync")]
        public async Task<ActionResult<ResponseModelDto>> UpdateProjectsAsync(int projectId, ProjectsDto projectsDto)
        {
            try
            {
                if (projectId != projectsDto.ProjectId)
                {
                    return BadRequest();
                }
                return await _projectsService.UpdateProjectsAsync(projectsDto);

            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
