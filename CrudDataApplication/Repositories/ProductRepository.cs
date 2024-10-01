using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;
using Microsoft.EntityFrameworkCore;

namespace CrudDataApplication.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IBaseRepository<Product> _repository;

        private readonly AppDbContext _context;

        public ProductRepository(IBaseRepository<Product> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        protected DbSet<Category> DbSetCategory() => _context.Category;
        public async Task<ResponseModelDto> GetAllProductsAsync()
        {
            IEnumerable<Product> lstProducts = await _repository.GetAllAsync();
            var lstProduct = lstProducts.Select(x => new ProductDto
            {
                Name = x.Name,
                Id = x.Id,
                CategoryId = x.CategoryId,
                CategoryName = DbSetCategory().AsNoTracking().FirstOrDefault(y => y.Id == x.CategoryId).Name,
                Price = x.Price
            }).OrderByDescending(x => x.Id);
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Product", lstProduct);
        }

        public async Task<ResponseModelDto> GetProductByIdAsync(int id)
        {
            var productById = await _repository.GetByIdAsync(id);
            var productDtoById = new ProductDto()
            {
                Id = id,
                Name = productById.Name,
                CategoryId = productById.CategoryId,
                CategoryName = DbSetCategory().AsNoTracking().FirstOrDefault(y => y.Id == productById.CategoryId).Name,
                Price = productById.Price
            };
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Product With ID : {id}", productDtoById);
        }

        public async Task<ResponseModelDto> AddProductAsync(ProductDto productDto)
        {
            Product product = new Product();
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.CategoryId = productDto.CategoryId;
            await _repository.AddAsync(product);
            return CommonUtilityHelper.CreateResponseData(true, "Product Saved Successfully", product);
        }

        public async Task<ResponseModelDto> UpdateProductAsync(ProductDto productDto)
        {
            Product product = new Product();
            product.Id = productDto.Id;
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.CategoryId = productDto.CategoryId;
            await _repository.UpdateAsync(product);
            return CommonUtilityHelper.CreateResponseData(true, "Product Updated Successfully", product);
        }

        public async Task<ResponseModelDto> DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return CommonUtilityHelper.CreateResponseData(true, $"Deleted Product With ID : {id}", id);
        }

        public async Task<ResponseModelDto> TruncateProductAsync()
        {
            try
            {
                await _repository.TruncateAsync();
                return CommonUtilityHelper.CreateResponseData(true, "All Products truncated successfully", null);
            }
            catch (Exception ex)
            {
                return CommonUtilityHelper.CreateResponseData(false, $"Error truncating Products: {ex.Message}", null);
            }
        }
    }
}
