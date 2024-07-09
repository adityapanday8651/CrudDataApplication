using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
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

            ResponseModelDto responseModelDto = new ResponseModelDto();
            List<ProductDto> lstProduct = await DbSet().Select(x => new ProductDto
            {
                Name = x.Name,
                Id = x.Id,
                CategoryId = x.CategoryId,
                CategoryName = DbSetCategory().AsNoTracking().FirstOrDefault(y => y.Id == x.CategoryId).Name,
                Price = x.Price
            }).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            responseModelDto.Status = true;
            responseModelDto.Message = "Retrieve all Product";
            responseModelDto.Data = lstProduct;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> GetProductByIdAsync(int id)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            var productDto = await DbSet().Where(x => x.Id == id).Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = DbSetCategory().FirstOrDefault(y => y.Id == x.CategoryId).Name,
                Price = x.Price,
            }).AsNoTracking().FirstOrDefaultAsync();

            responseModelDto.Status = true;
            responseModelDto.Message = $"Retrieve Product With ID : {id}";
            responseModelDto.Data = productDto;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> AddProductAsync(ProductDto productDto)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            Product product=new Product();
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.CategoryId = productDto.CategoryId;
            await _repository.AddAsync(product);
            responseModelDto.Status = true;
            responseModelDto.Message = "Product Saved Successfully";
            responseModelDto.Data = product;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> UpdateProductAsync(ProductDto productDto)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            Product product =new Product();
            product.Id = productDto.Id;
            product.Name = productDto.Name;
            product.Price= productDto.Price;
            product.CategoryId= productDto.CategoryId;
            await _repository.UpdateAsync(product);
            responseModelDto.Status = true;
            responseModelDto.Message = "Product Updated Successfully";
            responseModelDto.Data = product;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> DeleteProductAsync(int id)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            await _repository.DeleteAsync(id);
            responseModelDto.Status = true;
            responseModelDto.Message = $" Deleted Product With ID : {id}";
            responseModelDto.Data = id;
            return responseModelDto;
        }
    }
}
