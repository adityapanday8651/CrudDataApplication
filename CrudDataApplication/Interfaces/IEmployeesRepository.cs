using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<ResponseModelDto> GetAllEmployeesAsync();
        Task<ResponseModelDto> GetEmployeesByIdAsync(int id);
        Task<ResponseModelDto> AddEmployeesAsync(EmployeesDto EmployeesDtos);
        Task<ResponseModelDto> UpdateEmployeesAsync(EmployeesDto EmployeesDtos);
        Task<ResponseModelDto> DeleteEmployeesAsync(int id);
        Task<ResponseModelDto> TruncateEmployeesAsync();
    }
}
