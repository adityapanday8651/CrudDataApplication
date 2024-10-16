using CrudDataApplication.Dto;
namespace CrudDataApplication.Interfaces
{
    public interface ICompanyRepository
    {
        Task<ResponseModelDto> GetAllCompanyAsync();
        Task<ResponseModelDto> GetCompanyByIdAsync(int id);
        Task<ResponseModelDto> AddCompanyAsync(CompanyDto CompanyDtos);
        Task<ResponseModelDto> UpdateCompanyAsync(CompanyDto CompanyDtos);
        Task<ResponseModelDto> DeleteCompanyAsync(int id);
        Task<ResponseModelDto> TruncateCompanyAsync();
    }
}
