using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;

namespace CrudDataApplication.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<ResponseModelDto> AddManagerAsync(ManagerDto ManagerDtos)
        {
            return await _managerRepository.AddManagerAsync(ManagerDtos);
        }

        public async Task<ResponseModelDto> DeleteManagerAsync(int id)
        {
            return await _managerRepository.DeleteManagerAsync(id);
        }

        public async Task<ResponseModelDto> GetAllManagerAsync()
        {
            return await _managerRepository.GetAllManagerAsync();
        }

        public async Task<ResponseModelDto> GetManagerByIdAsync(int id)
        {
            return await _managerRepository.GetManagerByIdAsync(id);
        }

        public async Task<ResponseModelDto> TruncateManagerAsync()
        {
            return await _managerRepository.TruncateManagerAsync();
        }

        public async Task<ResponseModelDto> UpdateManagerAsync(ManagerDto ManagerDtos)
        {
            return await _managerRepository.UpdateManagerAsync(ManagerDtos);
        }
    }
}
