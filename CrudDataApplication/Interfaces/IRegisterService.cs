using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IRegisterService
    {
        Task<ResponseModelDto> GetRegisterByIdAsync(int id);
        Task<ResponseModelDto> AddRegisterAsync(RegisterDto registerDto);
        Task<RegisterDto> FindByNameAsync(string? userName);
    }
}
