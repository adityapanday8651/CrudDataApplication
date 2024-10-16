using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface ICompanyService
    {
        Task<ResponseModelDto> GetAllCompanyAsync();
        Task<ResponseModelDto> GetCompanyByIdAsync(int id);
        Task<ResponseModelDto> AddCompanyAsync(CompanyDto CompanyDtos);
        Task<ResponseModelDto> UpdateCompanyAsync(CompanyDto CompanyDtos);
        Task<ResponseModelDto> DeleteCompanyAsync(int id);
        Task<ResponseModelDto> TruncateCompanyAsync();
    }
}
