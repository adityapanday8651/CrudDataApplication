using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;
using Microsoft.EntityFrameworkCore;

namespace CrudDataApplication.Repositories
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly IBaseRepository<Departments> _baseRepository;
        private readonly AppDbContext _context;
        public DepartmentsRepository(IBaseRepository<Departments> baseRepository, AppDbContext context)
        {
            _baseRepository = baseRepository;
            _context = context;
        }
        protected DbSet<Manager> DbSetManager() => _context.Managers;
        protected DbSet<Employees> DbSetEmployees() => _context.Employees;
        protected DbSet<Projects> DbSetProjects() => _context.Projects;

        public async Task<ResponseModelDto> AddDepartmentsAsync(DepartmentsDto DepartmentsDtos)
        {
            Departments departmentsModel = new Departments();
            departmentsModel.DepartmentName = DepartmentsDtos.DepartmentName;
            departmentsModel.ManagerId = DepartmentsDtos.ManagerId;
            departmentsModel.EmployeesId = DepartmentsDtos.EmployeesId;
            departmentsModel.ProjectsId = DepartmentsDtos.ProjectsId;
            await _baseRepository.AddAsync(departmentsModel);
            return CommonUtilityHelper.CreateResponseData(true, "Departments Saved Successfully", departmentsModel);
        }

        public Task<ResponseModelDto> DeleteDepartmentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetAllDepartmentsAsync()
        {
            IEnumerable<Departments> lstDepartments = await _baseRepository.GetAllAsync();
            var lstDepartmentsDto = lstDepartments.Select(x => new DepartmentsDto
            {
                DepartmentId = x.DepartmentId,
                DepartmentName = x.DepartmentName,
                ManagerId = x.ManagerId,
                EmployeesId = x.EmployeesId,
                ProjectsId = x.ProjectsId,
                ManagerName = DbSetManager().FirstOrDefault(m => m.ManagerId == x.ManagerId)?.ManagerName,
                EmployeesName = DbSetEmployees().FirstOrDefault(m => m.EmployeeId == x.EmployeesId)?.Name,
                ProjectsName = DbSetProjects().FirstOrDefault(m => m.ProjectId == x.ProjectsId)?.ProjectName,
            });
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Departments", lstDepartmentsDto);
        }

        public async Task<ResponseModelDto> GetDepartmentsByIdAsync(int id)
        {
            var departmentsById = await _baseRepository.GetByIdAsync(id);
            var departmentsByIdDto = new DepartmentsDto()
            {
                DepartmentId = departmentsById.DepartmentId,
                DepartmentName = departmentsById.DepartmentName,
                ManagerId = departmentsById.ManagerId,
                EmployeesId = departmentsById.EmployeesId,
                ProjectsId = departmentsById.ProjectsId,
                ManagerName = DbSetManager()?.FirstOrDefault(m => m.ManagerId == departmentsById.ManagerId).ManagerName,
                EmployeesName = DbSetEmployees()?.FirstOrDefault(m => m.EmployeeId == departmentsById.EmployeesId).Name,
                ProjectsName = DbSetProjects()?.FirstOrDefault(m => m.ProjectId == departmentsById.ProjectsId).ProjectName,
            };
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Projects With ID : {id}", departmentsByIdDto);
        }

        public Task<ResponseModelDto> TruncateDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateDepartmentsAsync(DepartmentsDto DepartmentsDtos)
        {
            Departments departmentsModel = new Departments();
            departmentsModel.DepartmentId = DepartmentsDtos.DepartmentId;
            departmentsModel.DepartmentName = DepartmentsDtos.DepartmentName;
            departmentsModel.ManagerId = DepartmentsDtos.ManagerId;
            departmentsModel.EmployeesId = DepartmentsDtos.EmployeesId;
            departmentsModel.ProjectsId = DepartmentsDtos.ProjectsId;
            await _baseRepository.AddAsync(departmentsModel);
            return CommonUtilityHelper.CreateResponseData(true, "Departments Updated Successfully", departmentsModel);
        }
    }
}
