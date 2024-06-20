using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;

namespace CrudDataApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ResponseModelDto> AddProductAsync(ProductDto productDto)
        {
            return await _productRepository.AddProductAsync(productDto);
        }

        public async Task<ResponseModelDto> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }

        public async Task<ResponseModelDto> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<ResponseModelDto> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<ResponseModelDto> UpdateProductAsync(ProductDto productDto)
        {
            return await _productRepository.UpdateProductAsync(productDto);
        }
    }
}
