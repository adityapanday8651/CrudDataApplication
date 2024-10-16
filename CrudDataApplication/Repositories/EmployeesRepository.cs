using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CrudDataApplication.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IBaseRepository<Employees> _repository;
        private readonly AppDbContext _context;
        public EmployeesRepository(IBaseRepository<Employees> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        protected DbSet<Address> DbSetAddress() => _context.Address;

        public async Task<ResponseModelDto> AddEmployeesAsync(EmployeesDto EmployeesDtos)
        {
            Employees employees = new Employees();
            employees.Name = EmployeesDtos.Name;
            employees.Position = EmployeesDtos.Position;
            employees.Salary = EmployeesDtos.Salary;
            employees.HireDate = EmployeesDtos.HireDate;
            employees.AddressId = EmployeesDtos.AddressId;
            employees.IsActive = true;
            await _repository.AddAsync(employees);
            return CommonUtilityHelper.CreateResponseData(true, "Employees Saved Successfully", employees);
        }

        public Task<ResponseModelDto> DeleteEmployeesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetAllEmployeesAsync()
        {
            IEnumerable<Employees> lstEmployees = await _repository.GetAllAsync();
            var lstEmployeesDto = lstEmployees.Select(x => new EmployeesDto
            {
                EmployeeId = x.EmployeeId,
                Name = x.Name,
                Position = x.Position,
                Salary = x.Salary,
                HireDate = x.HireDate,
                AddressId = x.AddressId,
                AdrressStreet = DbSetAddress().AsNoTracking().FirstOrDefault(d => d.AddressId == x.AddressId)?.Street,
                IsActive = x.IsActive,
            });
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Address", lstEmployeesDto);
        }

        public async Task<ResponseModelDto> GetEmployeesByIdAsync(int id)
        {
            var employeesById = await _repository.GetByIdAsync(id);
            var employeesByIdDto = new EmployeesDto()
            {
                EmployeeId = employeesById.EmployeeId,
                Name = employeesById.Name,
                Position = employeesById.Position,
                Salary = employeesById.Salary,
                HireDate = employeesById.HireDate,
                AddressId = employeesById.AddressId,
                AdrressStreet = DbSetAddress().AsNoTracking().FirstOrDefault(d => d.AddressId == employeesById.AddressId)?.Street,
                IsActive = employeesById.IsActive,
            };
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Employees With ID : {id}", employeesByIdDto);
        }

        public Task<ResponseModelDto> TruncateEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateEmployeesAsync(EmployeesDto EmployeesDtos)
        {
            Employees employees = new Employees();
            employees.EmployeeId = EmployeesDtos.EmployeeId;
            employees.Name = EmployeesDtos.Name;
            employees.Position = EmployeesDtos.Position;
            employees.Salary = EmployeesDtos.Salary;
            employees.HireDate = EmployeesDtos.HireDate;
            employees.AddressId = EmployeesDtos.AddressId;
            employees.IsActive = true;
            await _repository.AddAsync(employees);
            return CommonUtilityHelper.CreateResponseData(true, "Employees Updated Successfully", employees);
        }
    }
}
