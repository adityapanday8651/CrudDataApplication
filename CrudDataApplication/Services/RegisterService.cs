using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;

namespace CrudDataApplication.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterService(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }
        public async Task<ResponseModelDto> AddRegisterAsync(RegisterDto registerDto)
        {
            return await _registerRepository.AddRegisterAsync(registerDto);
        }

        public async Task<RegisterDto> FindByNameAsync(string? userName)
        {
            return await _registerRepository.FindByNameAsync(userName);
        }

        public async Task<ResponseModelDto> GetRegisterByIdAsync(int id)
        {
            return await _registerRepository.GetRegisterByIdAsync(id);
        }
    }
}
