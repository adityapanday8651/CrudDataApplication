using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface ITasksRepository
    {
        Task<ResponseModelDto> GetAllTasksAsync();
        Task<ResponseModelDto> GetTasksByIdAsync(int id);
        Task<ResponseModelDto> AddTasksAsync(TasksDto TasksDtos);
        Task<ResponseModelDto> UpdateTasksAsync(TasksDto TasksDtos);
        Task<ResponseModelDto> DeleteTasksAsync(int id);
        Task<ResponseModelDto> TruncateTasksAsync();
    }
}
