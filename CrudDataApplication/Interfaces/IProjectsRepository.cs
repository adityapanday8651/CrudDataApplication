using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IProjectsRepository
    {
        Task<ResponseModelDto> GetAllProjectsAsync();
        Task<ResponseModelDto> GetProjectsByIdAsync(int id);
        Task<ResponseModelDto> AddProjectsAsync(ProjectsDto ProjectsDtos);
        Task<ResponseModelDto> UpdateProjectsAsync(ProjectsDto ProjectsDtos);
        Task<ResponseModelDto> DeleteProjectsAsync(int id);
        Task<ResponseModelDto> TruncateProjectsAsync();
    }
}
