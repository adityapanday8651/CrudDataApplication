using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IDepartmentsRepository
    {
        Task<ResponseModelDto> GetAllDepartmentsAsync();
        Task<ResponseModelDto> GetDepartmentsByIdAsync(int id);
        Task<ResponseModelDto> AddDepartmentsAsync(DepartmentsDto DepartmentsDtos);
        Task<ResponseModelDto> UpdateDepartmentsAsync(DepartmentsDto DepartmentsDtos);
        Task<ResponseModelDto> DeleteDepartmentsAsync(int id);
        Task<ResponseModelDto> TruncateDepartmentsAsync();
    }
}
