using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;
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
            Register register = new Register();
            register.Username = registerDto.Username;
            register.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            register.Email = registerDto.Email;
            await _baseRepository.AddAsync(register);
            return CommonUtilityHelper.CreateResponseData(true, "Registration Successfully", register);
        }

        public async Task<ResponseModelDto> GetRegisterByIdAsync(int id)
        {
            var registerDto = await DbSet().Where(x => x.Id == id).Select(x => new RegisterDto
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email
            }).AsNoTracking().FirstOrDefaultAsync();
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Register With ID : {id}", registerDto);
        }

        public async Task<RegisterDto> FindByNameAsync(string? userName)
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

        public async Task<ResponseModelDto> GetAllRolesAsync()
        {
            List<RoleDto> lstRoles = await DbRolesSet().Select(x => new RoleDto
            {
                RoleName = x.RoleName,
                Id = x.Id,
            }).AsNoTracking().ToListAsync();
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Roles", lstRoles);
        }
    }
}
