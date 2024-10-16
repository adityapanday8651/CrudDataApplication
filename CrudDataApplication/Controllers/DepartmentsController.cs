using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;
        public readonly ILoggerRepository<DepartmentsController> _loggerRepository;

        public DepartmentsController(IDepartmentsService departmentsService, ILoggerRepository<DepartmentsController> loggerRepository)
        {
            _departmentsService = departmentsService;
            _loggerRepository = loggerRepository;
        }

        [HttpGet("GetAllDepartmentsAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _departmentsService.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddDepartmentsAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddDepartmentsAsync(DepartmentsDto departmentsDto)
        {
            try
            {
                return await _departmentsService.AddDepartmentsAsync(departmentsDto);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
