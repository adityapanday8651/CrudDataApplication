using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;

namespace CrudDataApplication.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<ResponseModelDto> AddCompanyAsync(CompanyDto CompanyDtos)
        {
            return await _companyRepository.AddCompanyAsync(CompanyDtos);
        }

        public Task<ResponseModelDto> DeleteCompanyAsync(int id)
        {
            return _companyRepository.DeleteCompanyAsync(id);
        }

        public async Task<ResponseModelDto> GetAllCompanyAsync()
        {
            return await _companyRepository.GetAllCompanyAsync();
        }

        public async Task<ResponseModelDto> GetCompanyByIdAsync(int id)
        {
            return await _companyRepository.GetCompanyByIdAsync(id);
        }

        public async Task<ResponseModelDto> TruncateCompanyAsync()
        {
            return await _companyRepository.TruncateCompanyAsync();
        }

        public async Task<ResponseModelDto> UpdateCompanyAsync(CompanyDto CompanyDtos)
        {
            return await _companyRepository.UpdateCompanyAsync(CompanyDtos);
        }
    }
}
