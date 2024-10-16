using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseModelDto> GetAllCategoriesAsync();
        Task<ResponseModelDto> GetCategoryByIdAsync(int id);
        Task<ResponseModelDto> AddCategoryAsync(CategoryDto Category);
        Task<ResponseModelDto> UpdateCategoryAsync(CategoryDto Category);
        Task<ResponseModelDto> DeleteCategoryAsync(int id);
        Task<ResponseModelDto> TruncateCategoriesAsync();
    }
}
