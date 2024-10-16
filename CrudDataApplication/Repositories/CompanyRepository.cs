using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;
using Microsoft.EntityFrameworkCore;

namespace CrudDataApplication.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IBaseRepository<Company> _repository;
        private readonly AppDbContext _context;
        public CompanyRepository(IBaseRepository<Company> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        protected DbSet<Departments> DbSetDepartments() => _context.Departments;
        public async Task<ResponseModelDto> AddCompanyAsync(CompanyDto CompanyDtos)
        {
            Company companyModel = new Company();
            companyModel.Name = CompanyDtos.Name;
            companyModel.Location = CompanyDtos.Location;
            companyModel.DepartmentId = CompanyDtos.DepartmentId;
            await _repository.AddAsync(companyModel);
            return CommonUtilityHelper.CreateResponseData(true, "Company Saved Successfully", companyModel);
        }

        public Task<ResponseModelDto> DeleteCompanyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetAllCompanyAsync()
        {
            IEnumerable<Company> lstCompany = await _repository.GetAllAsync();
            var lstCompanyDto = lstCompany.Select(x => new CompanyDto
            {
                Id = x.Id,
                Name = x.Name,
                Location = x.Location,
                DepartmentId = x.DepartmentId,
                DepartmentName = DbSetDepartments().FirstOrDefault(d => d.DepartmentId == x.DepartmentId)?.DepartmentName
            });
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Company", lstCompanyDto);
        }

        public async Task<ResponseModelDto> GetCompanyByIdAsync(int id)
        {
            var companyById = await _repository.GetByIdAsync(id);
            var companyByIdDto = new CompanyDto()
            {
                Id = companyById.Id,
                Name = companyById.Name,
                Location = companyById.Location,
                DepartmentId = companyById.DepartmentId,
                DepartmentName = DbSetDepartments()?.AsNoTracking()?.FirstOrDefault(d => d.DepartmentId == companyById.DepartmentId)?.DepartmentName
            };
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Company With ID : {id}", companyByIdDto);
        }

        public Task<ResponseModelDto> TruncateCompanyAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateCompanyAsync(CompanyDto CompanyDtos)
        {
            Company companyModel = new Company();
            companyModel.Id = CompanyDtos.Id;
            companyModel.Name = CompanyDtos.Name;
            companyModel.Location = CompanyDtos.Location;
            companyModel.DepartmentId = CompanyDtos.DepartmentId;
            await _repository.UpdateAsync(companyModel);
            return CommonUtilityHelper.CreateResponseData(true, "Company Updated Successfully", companyModel);
        }
    }
}
