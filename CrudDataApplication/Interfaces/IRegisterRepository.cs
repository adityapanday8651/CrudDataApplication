using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IRegisterRepository
    {
        Task<ResponseModelDto> GetRegisterByIdAsync(int id);
        Task<ResponseModelDto> AddRegisterAsync(RegisterDto registerDto);
        Task<RegisterDto> FindByNameAsync(string? userName);
    }
}
