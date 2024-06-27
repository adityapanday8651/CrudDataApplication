using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudDataApplication.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly IBaseRepository<Register> _baseRepository;
        private readonly AppDbContext _context;

        public RegisterRepository(IBaseRepository<Register> baseRepository, AppDbContext context)
        {
            _baseRepository = baseRepository;
            _context = context;
        }

        protected DbSet<Register> DbSet() => _context.Register;

        protected DbSet<Roles> DbRolesSet() => _context.Roles;
        public async Task<ResponseModelDto> AddRegisterAsync(RegisterDto registerDto)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            Register register = new Register();
            register.Username = registerDto.Username;
            register.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            register.Email = registerDto.Email;
            await _baseRepository.AddAsync(register);
            responseModelDto.Status = true;
            responseModelDto.Message = "Registration Successfully";
            responseModelDto.Data = register;
            return responseModelDto;
        }

        public async Task<ResponseModelDto> GetRegisterByIdAsync(int id)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            var registerDto = await DbSet().Where(x => x.Id == id).Select(x => new RegisterDto
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email
            }).FirstOrDefaultAsync();
            responseModelDto.Status = true;
            responseModelDto.Message = $"Retrieve Register With ID : {id}";
            responseModelDto.Data = registerDto;
            return responseModelDto;
        }

        public async Task<RegisterDto> FindByNameAsync(string userName)
        {
            var registerDto = await DbSet().Where(x => x.Username == userName).Select(x => new RegisterDto
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email,
                RoleId = x.RoleId,
                RoleName = DbRolesSet().FirstOrDefault(r => r.Id == x.RoleId).RoleName,
            }).AsNoTracking().FirstOrDefaultAsync();

            return registerDto;
        }
    }
}
