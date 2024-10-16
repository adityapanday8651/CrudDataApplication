using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _taskService;
        private readonly ILoggerRepository<TasksController> _loggerRepository;
        public TasksController(ITasksService taskService, ILoggerRepository<TasksController> loggerRepository)
        {
            _taskService = taskService;
            _loggerRepository = loggerRepository;
        }

        [HttpGet("GetAllTasksAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllTasksAsync()
        {
            try
            {
                var tasks = await _taskService.GetAllTasksAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddTasksAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddTasksAsync(TasksDto tasksDto)
        {
            try
            {
                return await _taskService.AddTasksAsync(tasksDto);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTasksByIdAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetTasksByIdAsync(int taskId)
        {
            try
            {
                var task = await _taskService.GetTasksByIdAsync(taskId);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateTasksAsync")]
        public async Task<ActionResult<ResponseModelDto>> UpdateTasksAsync(int taskId, TasksDto tasksDto)
        {
            try
            {
                if (taskId != tasksDto.TaskId)
                {
                    return BadRequest();
                }
                return await _taskService.UpdateTasksAsync(tasksDto);

            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
