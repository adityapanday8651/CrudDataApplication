using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILoggerRepository<ProductsController> _loggerRepository;

        public ProductsController(IProductService productService, ILoggerRepository<ProductsController> loggerRepository)
        {
            _productService = productService;
            _loggerRepository = loggerRepository;
        }

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
    }
}
