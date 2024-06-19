using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;

namespace CrudDataApplication.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ResponseModelDto> AddCategoryAsync(CategoryDto Category)
        {
            return await _categoryRepository.AddCategoryAsync(Category);
        }

        public async Task<ResponseModelDto> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<ResponseModelDto> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<ResponseModelDto> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<ResponseModelDto> UpdateCategoryAsync(Category Category)
        {
            return await _categoryRepository.UpdateCategoryAsync(Category);
        }
    }
}
