using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;

namespace CrudDataApplication.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDepartmentsRepository _departmentsRepository;
        public DepartmentsService(IDepartmentsRepository departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        public async Task<ResponseModelDto> AddDepartmentsAsync(DepartmentsDto DepartmentsDtos)
        {
            return await _departmentsRepository.AddDepartmentsAsync(DepartmentsDtos);
        }

        public async Task<ResponseModelDto> DeleteDepartmentsAsync(int id)
        {
            return await _departmentsRepository.DeleteDepartmentsAsync(id);
        }

        public async Task<ResponseModelDto> GetAllDepartmentsAsync()
        {
            return await _departmentsRepository.GetAllDepartmentsAsync();
        }

        public async Task<ResponseModelDto> GetDepartmentsByIdAsync(int id)
        {
            return await _departmentsRepository.GetDepartmentsByIdAsync(id);
        }

        public async Task<ResponseModelDto> TruncateDepartmentsAsync()
        {
            return await _departmentsRepository.TruncateDepartmentsAsync();
        }

        public async Task<ResponseModelDto> UpdateDepartmentsAsync(DepartmentsDto DepartmentsDtos)
        {
            return await _departmentsRepository.UpdateDepartmentsAsync(DepartmentsDtos);
        }
    }
}
