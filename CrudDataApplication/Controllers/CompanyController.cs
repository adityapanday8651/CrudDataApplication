using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILoggerRepository<CompanyController> _loggerRepository;

        public CompanyController(ICompanyService companyService, ILoggerRepository<CompanyController> loggerRepository)
        {
            _companyService = companyService;
            _loggerRepository = loggerRepository;
        }
        [HttpGet("GetAllCompanyAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllCompanyAsync()
        {
            try
            {
                var companies = await _companyService.GetAllCompanyAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCompanyAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddCompanyAsync(CompanyDto companyDto)
        {
            try
            {
                return await _companyService.AddCompanyAsync(companyDto);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
