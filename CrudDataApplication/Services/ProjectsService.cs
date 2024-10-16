using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;

namespace CrudDataApplication.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
        public async Task<ResponseModelDto> AddProjectsAsync(ProjectsDto ProjectsDtos)
        {
            return await _projectsRepository.AddProjectsAsync(ProjectsDtos);
        }

        public Task<ResponseModelDto> DeleteProjectsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> GetAllProjectsAsync()
        {
            return _projectsRepository.GetAllProjectsAsync();
        }

        public async Task<ResponseModelDto> GetProjectsByIdAsync(int id)
        {
            return await _projectsRepository.GetProjectsByIdAsync(id);
        }

        public Task<ResponseModelDto> TruncateProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateProjectsAsync(ProjectsDto ProjectsDtos)
        {
            return await _projectsRepository.UpdateProjectsAsync(ProjectsDtos);
        }
    }
}
