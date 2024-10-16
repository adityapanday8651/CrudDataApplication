using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ILoggerRepository<AddressController> _loggerRepository;
        public AddressController(IAddressService addressService, ILoggerRepository<AddressController> loggerRepository)
        {
            _addressService = addressService;
            _loggerRepository = loggerRepository;
        }
        [HttpGet("GetAllAddressAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllAddressAsync()
        {
            try
            {
                var address = await _addressService.GetAllAddressAsync();
                return Ok(address);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCategoryAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddAddressAsync(AddressDto addressDto)
        {
            try
            {
                return await _addressService.AddAddressAsync(addressDto);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
