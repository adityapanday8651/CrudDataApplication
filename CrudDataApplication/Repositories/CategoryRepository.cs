using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;
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
            Category category = new Category();
            category.Name = Category.Name;
            await _repository.AddAsync(category);
            return CommonUtilityHelper.CreateResponseData(true, "Category Saved Successfully", category);
        }

        public async Task<ResponseModelDto> DeleteCategoryAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return CommonUtilityHelper.CreateResponseData(true, $"Deleted Category With ID : {id}", id);
        }

        public async Task<ResponseModelDto> GetAllCategoriesAsync()
        {
            const int defaultPageNumber = 1;
            const int defaultPageSize = 10;
            int totalCount = await DbSet().CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)defaultPageSize);
            List<CategoryDto> categoryDtos = await DbSet()
                .OrderByDescending(x => x.Id)
                .Skip((defaultPageNumber - 1) * defaultPageSize)
                .Take(defaultPageSize)
                .Select(x => new CategoryDto
                {
                    Name = x.Name,
                    Id = x.Id,
                })
                .AsNoTracking()
                .ToListAsync();
            var paginationMetadata = new
            {
                TotalCount = totalCount,
                PageSize = defaultPageSize,
                CurrentPage = defaultPageNumber,
                TotalPages = totalPages,
                HasNext = defaultPageNumber < totalPages,
                HasPrevious = false // Always false for the first page
            };
            var paginationWithCategoryData = new
            {
                Categories = categoryDtos,
                Pagination = paginationMetadata
            };
            return CommonUtilityHelper.CreateResponseData(true, "Retrieved paginated categories", paginationWithCategoryData);
        }

        public async Task<ResponseModelDto> GetCategoryByIdAsync(int id)
        {
            var categoryDto = await DbSet().Where(x => x.Id == id).Select(x => new CategoryDto { Id = x.Id, Name = x.Name }).AsNoTracking().FirstOrDefaultAsync();
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Category With Name and ID : {id}", categoryDto);
        }

        public async Task<ResponseModelDto> UpdateCategoryAsync(CategoryDto categoryDtos)
        {
            Category category = new Category();
            category.Name = categoryDtos.Name;
            category.Id = categoryDtos.Id;
            await _repository.UpdateAsync(category);
            return CommonUtilityHelper.CreateResponseData(true, "Category Updated Successfully", category);
        }

        public async Task<ResponseModelDto> TruncateCategoriesAsync()
        {
            try
            {
                await _repository.TruncateAsync();
                return CommonUtilityHelper.CreateResponseData(true, "All categories truncated successfully", null);
            }
            catch (Exception ex)
            {
                return CommonUtilityHelper.CreateResponseData(false, $"Error truncating categories: {ex.Message}", null);
            }
        }
    }
}
