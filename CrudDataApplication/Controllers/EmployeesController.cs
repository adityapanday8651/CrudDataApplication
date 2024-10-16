using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly ILoggerRepository<EmployeesController> _loggerRepository;

        public EmployeesController(IEmployeesService employeesService, ILoggerRepository<EmployeesController> loggerRepository)
        {
            _employeesService = employeesService;
            _loggerRepository = loggerRepository;
        }

        [HttpGet("GetAllEmployeesAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeesService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddEmployeesAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddEmployeesAsync(EmployeesDto employeesDto)
        {
            try
            {
                return await _employeesService.AddEmployeesAsync(employeesDto);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
