﻿using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILoggerRepository<CategoryController> _loggerRepository;
        private readonly IBaseRepository<Category> _repository;

        public CategoryController(ICategoryService categoryService, ILoggerRepository<CategoryController> loggerRepository,IBaseRepository<Category> repository)
        {
            _categoryService = categoryService;
            _loggerRepository = loggerRepository;
            _repository = repository;
        }

        [Authorize(Policy = "RequireAdminRole")]
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
                _loggerRepository.ErrorMessage(ex);
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
                return await _categoryService.DeleteCategoryAsync(id);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TruncateCategoriesAsync")]
        public async Task<ActionResult<ResponseModelDto>> TruncateCategoriesAsync()
        {
            try
            {
                ResponseModelDto result = await _categoryService.TruncateCategoriesAsync();
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


        [HttpGet("GetCategoriesPagedAsync")]
        public async Task<IActionResult> GetCategoriesPagedAsync(int pageNumber = 1, int pageSize = 10)
        {
            var (items, totalCount, totalPages) = await _repository.GetPagedAsync(pageNumber, pageSize);

            var response = new
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };

            return Ok(response);
        }
    }
}
