using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILoggerRepository<CategoryController> _loggerRepository;

        public CategoryController(ICategoryService categoryService, ILoggerRepository<CategoryController> loggerRepository)
        {
            _categoryService = categoryService;
            _loggerRepository = loggerRepository;
        }

        [HttpGet("GetAllCategoriesAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllCategoriesAsync()
        {
            try
            {
               
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddCategoryAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddCategoryAsync(CategoryDto category)
        {
            try
            {
                return await _categoryService.AddCategoryAsync(category);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCategoryByIdAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var product = await _categoryService.GetCategoryByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCategoryAsync")]
        public async Task<ActionResult<ResponseModelDto>> UpdateCategoryAsync(int id, CategoryDto categoryDto)
        {
            try
            {
                if (id != categoryDto.Id)
                {
                    return BadRequest();
                }
                return await _categoryService.UpdateCategoryAsync(categoryDto);

            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCategoryAsync")]
        public async Task<ActionResult<ResponseModelDto>> DeleteCategoryAsync(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
