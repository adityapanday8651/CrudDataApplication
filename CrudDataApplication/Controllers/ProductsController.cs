using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILoggerRepository<ProductsController> _loggerRepository;
        private readonly IBaseRepository<Product> _repository;

        public ProductsController(IProductService productService, ILoggerRepository<ProductsController> loggerRepository, IBaseRepository<Product> repository)
        {
            _productService = productService;
            _loggerRepository = loggerRepository;
            _repository = repository;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("GetAllProductsAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductByIdAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
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

        [HttpPost("AddProductAsync")]
        public async Task<ActionResult<ResponseModelDto>> AddProductAsync(ProductDto productDto)
        {
            try
            {
                return await _productService.AddProductAsync(productDto);

            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProductAsync")]
        public async Task<ActionResult<ResponseModelDto>> UpdateProductAsync(int id, ProductDto productDto)
        {
            try
            {
                if (id != productDto.Id)
                {
                    return BadRequest();
                }
                return await _productService.UpdateProductAsync(productDto);

            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteProductAsync")]
        public async Task<ActionResult<ResponseModelDto>> DeleteProductAsync(int id)
        {
            try
            {
                return await _productService.DeleteProductAsync(id);

            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TruncateProductAsync")]
        public async Task<ActionResult<ResponseModelDto>> TruncateProductAsync()
        {
            try
            {
                ResponseModelDto result = await _productService.TruncateProductAsync();
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


        [HttpGet("GetProductPagedAsync")]
        public async Task<IActionResult> GetProductPagedAsync(int pageNumber, int pageSize)
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
