using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IManagerService
    {
        Task<ResponseModelDto> GetAllManagerAsync();
        Task<ResponseModelDto> GetManagerByIdAsync(int id);
        Task<ResponseModelDto> AddManagerAsync(ManagerDto ManagerDtos);
        Task<ResponseModelDto> UpdateManagerAsync(ManagerDto ManagerDtos);
        Task<ResponseModelDto> DeleteManagerAsync(int id);
        Task<ResponseModelDto> TruncateManagerAsync();
    }
}
