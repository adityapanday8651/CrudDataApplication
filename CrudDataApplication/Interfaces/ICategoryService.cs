using CrudDataApplication.Dto;
using CrudDataApplication.Models;

namespace CrudDataApplication.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseModelDto> GetAllCategoriesAsync();
        Task<ResponseModelDto> GetCategoryByIdAsync(int id);
        Task<ResponseModelDto> AddCategoryAsync(CategoryDto Category);
        Task<ResponseModelDto> UpdateCategoryAsync(Category Category);
        Task<ResponseModelDto> DeleteCategoryAsync(int id);
    }
}
