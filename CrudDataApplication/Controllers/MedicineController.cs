using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly ILoggerRepository<MedicineController> _loggerRepository;

        public MedicineController(IMedicineService medicineService, ILoggerRepository<MedicineController> loggerRepository)
        {
            _medicineService = medicineService;
            _loggerRepository = loggerRepository;
        }

        [HttpGet("GetAllMedicinesAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllMedicinesAsync()
        
        {
            try
            {

                var lstMedicines = await _medicineService.GetAllMedicinesAsync();
                return Ok(lstMedicines);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddMedicineAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddMedicineAsync(MedicineDto MedicineDto)
        {
            try
            {
                return await _medicineService.AddMedicineAsync(MedicineDto);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMedicineByIdAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetMedicineByIdAsync(int id)
        {
            try
            {
                var medicine = await _medicineService.GetMedicineByIdAsync(id);
                if (medicine == null)
                {
                    return NotFound();
                }
                return Ok(medicine);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteMedicineAsync")]
        public async Task<ActionResult<ResponseModelDto>> DeleteMedicineAsync(int id)
        {
            try
            {
                return await _medicineService.DeleteMedicineAsync(id);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TruncateMedicineAsync")]
        public async Task<ActionResult<ResponseModelDto>> TruncateMedicineAsync()
        {
            try
            {
                ResponseModelDto result = await _medicineService.TruncateMedicineAsync();
                if (result.Status == true)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
