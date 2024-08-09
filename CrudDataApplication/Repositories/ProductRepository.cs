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

        protected DbSet<Product> DbSet() => _context.Products;
        protected DbSet<Category> DbSetCategory() => _context.Category;



        public async Task<ResponseModelDto> GetAllProductsAsync()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            List<ProductDto> lstProduct = await DbSet().Select(x => new ProductDto
            {
                Name = x.Name,
                Id = x.Id,
                CategoryId = x.CategoryId,
                CategoryName = DbSetCategory().AsNoTracking().FirstOrDefault(y => y.Id == x.CategoryId).Name,
                Price = x.Price
            }).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Product", lstProduct);
        }

        public async Task<ResponseModelDto> GetProductByIdAsync(int id)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var productDto = await DbSet().Where(x => x.Id == id).Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = DbSetCategory().FirstOrDefault(y => y.Id == x.CategoryId).Name,
                Price = x.Price,
            }).AsNoTracking().FirstOrDefaultAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Product With ID : {id}", productDto);
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
