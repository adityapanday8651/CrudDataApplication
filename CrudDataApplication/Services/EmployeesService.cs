using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;

namespace CrudDataApplication.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;
        public EmployeesService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }
        public async Task<ResponseModelDto> AddEmployeesAsync(EmployeesDto EmployeesDtos)
        {
            return await _employeesRepository.AddEmployeesAsync(EmployeesDtos);
        }

        public async Task<ResponseModelDto> DeleteEmployeesAsync(int id)
        {
            return await _employeesRepository.DeleteEmployeesAsync(id);
        }

        public async Task<ResponseModelDto> GetAllEmployeesAsync()
        {
            return await _employeesRepository.GetAllEmployeesAsync();
        }

        public async Task<ResponseModelDto> GetEmployeesByIdAsync(int id)
        {
            return await _employeesRepository.GetEmployeesByIdAsync(id);
        }

        public async Task<ResponseModelDto> TruncateEmployeesAsync()
        {
            return await _employeesRepository.TruncateEmployeesAsync();
        }

        public async Task<ResponseModelDto> UpdateEmployeesAsync(EmployeesDto EmployeesDtos)
        {
            return await _employeesRepository.UpdateEmployeesAsync(EmployeesDtos);
        }
    }
}
