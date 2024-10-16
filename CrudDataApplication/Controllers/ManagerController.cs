using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        private readonly ILoggerRepository<ManagerController> _loggerRepository;
        public ManagerController(IManagerService managerService, ILoggerRepository<ManagerController> loggerRepository)
        {
            _managerService = managerService;
            _loggerRepository = loggerRepository;
        }

        [HttpGet("GetAllManagerAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllManagerAsync()
        {
            try
            {
                var managers = await _managerService.GetAllManagerAsync();
                return Ok(managers);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddManagerAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddManagerAsync(ManagerDto managerDto)
        {
            try
            {
                return await _managerService.AddManagerAsync(managerDto);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
