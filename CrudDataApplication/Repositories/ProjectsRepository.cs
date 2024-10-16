using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;
using Microsoft.EntityFrameworkCore;

namespace CrudDataApplication.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly IBaseRepository<Projects> _repository;
        private readonly AppDbContext _context;
        public ProjectsRepository(IBaseRepository<Projects> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        protected DbSet<Tasks> DbSetTasks() => _context.Tasks;
        public async Task<ResponseModelDto> AddProjectsAsync(ProjectsDto ProjectsDtos)
        {
            Projects projects = new Projects();
            projects.ProjectName = ProjectsDtos.ProjectName;
            projects.Status = ProjectsDtos.Status;
            projects.TasksId = ProjectsDtos.TasksId;
            projects.IsActive = ProjectsDtos.IsActive;
            await _repository.AddAsync(projects);
            return CommonUtilityHelper.CreateResponseData(true, "Projetcs Saved Successfully", projects);
        }

        public async Task<ResponseModelDto> DeleteProjectsAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return CommonUtilityHelper.CreateResponseData(true, $"Deleted Projects With ID : {id}", id);
        }

        public async Task<ResponseModelDto> GetAllProjectsAsync()
        {
            IEnumerable<Projects> listProjects = await _repository.GetAllAsync();
            var listProjectsDto = listProjects.Select(x => new ProjectsDto
            {
                ProjectId = x.ProjectId,
                ProjectName = x.ProjectName,
                Status = x.Status,
                TasksId = x.TasksId,
                TasksName = DbSetTasks().AsNoTracking().FirstOrDefault(y => y.TaskId == x.TasksId)?.TaskName,
                IsActive = x.IsActive,
            });
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Projects", listProjectsDto);
        }
        public async Task<ResponseModelDto> GetProjectsByIdAsync(int id)
        {
            var projectsById = await _repository.GetByIdAsync(id);
            var projectsByIdDto = new ProjectsDto()
            {
                ProjectId = projectsById.ProjectId,
                ProjectName = projectsById.ProjectName,
                Status = projectsById.Status,
                TasksId = projectsById.TasksId,
                TasksName = DbSetTasks()?.AsNoTracking()?.FirstOrDefault(x => x.TaskId == projectsById.TasksId)?.TaskName,
                IsActive = projectsById.IsActive,
            };
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Projects With ID : {id}", projectsByIdDto);
        }
        public Task<ResponseModelDto> TruncateProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateProjectsAsync(ProjectsDto ProjectsDtos)
        {
            Projects projects = new Projects();
            projects.ProjectId = ProjectsDtos.ProjectId;
            projects.ProjectName = ProjectsDtos.ProjectName;
            projects.Status = ProjectsDtos.Status;
            projects.TasksId = ProjectsDtos.TasksId;
            projects.IsActive = ProjectsDtos.IsActive;
            await _repository.UpdateAsync(projects);
            return CommonUtilityHelper.CreateResponseData(true, "Projetcs Updated Successfully", projects);
        }
    }
}
