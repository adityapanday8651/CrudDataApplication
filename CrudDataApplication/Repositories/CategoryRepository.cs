using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudDataApplication.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IBaseRepository<Category> _repository;

        private readonly AppDbContext _context;
        public CategoryRepository(IBaseRepository<Category> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        protected DbSet<Category> DbSet() => _context.Category;

        public async Task<ResponseModelDto> AddCategoryAsync(CategoryDto Category)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            Category category = new Category();
            category.Name = Category.Name;
            await _repository.AddAsync(category);
            responseModelDto.Status = true;
            responseModelDto.Message = "Category Saved Successfully";
            responseModelDto.Data = category;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> DeleteCategoryAsync(int id)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            await _repository.DeleteAsync(id);
            responseModelDto.Status = true;
            responseModelDto.Message = $" Deleted Category With ID : {id}";
            responseModelDto.Data = id;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> GetAllCategoriesAsync()
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            List<CategoryDto> categoryDtos = await DbSet().Select(x => new CategoryDto
            {
                Name = x.Name,
                Id = x.Id,
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            responseModelDto.Status = true;
            responseModelDto.Message = "Retrieve all Category";
            responseModelDto.Data = categoryDtos;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> GetCategoryByIdAsync(int id)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            var categoryDto = await DbSet().Where(x => x.Id == id).Select(x => new CategoryDto { Id = x.Id, Name = x.Name }).AsNoTracking().FirstOrDefaultAsync();

            responseModelDto.Status = true;
            responseModelDto.Message = $"Retrieve Category With ID : {id}";
            responseModelDto.Data = categoryDto;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> UpdateCategoryAsync(CategoryDto categoryDtos)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            Category category = new Category();
            category.Name = categoryDtos.Name;
            category.Id = categoryDtos.Id;
            await _repository.UpdateAsync(category);
            responseModelDto.Status = true;
            responseModelDto.Message = "Category Updated Successfully";
            responseModelDto.Data = category;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> TruncateCategoriesAsync()
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            try
            {
                await _repository.TruncateAsync();
                responseModelDto.Status = true;
                responseModelDto.Message = "All categories truncated successfully";
            }
            catch (Exception ex)
            {
                responseModelDto.Status = false;
                responseModelDto.Message = $"Error truncating categories: {ex.Message}";
            }
            return responseModelDto;
        }
    }
}
