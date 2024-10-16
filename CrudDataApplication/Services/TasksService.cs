using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;

namespace CrudDataApplication.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        public TasksService(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<ResponseModelDto> AddTasksAsync(TasksDto Tasks)
        {
            return await _tasksRepository.AddTasksAsync(Tasks);
        }

        public Task<ResponseModelDto> DeleteTasksAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetAllTasksAsync()
        {
            return await _tasksRepository.GetAllTasksAsync();
        }

        public async Task<ResponseModelDto> GetTasksByIdAsync(int id)
        {
            return await _tasksRepository.GetTasksByIdAsync(id);
        }

        public Task<ResponseModelDto> TruncateTasksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateTasksAsync(TasksDto TasksDtos)
        {
            return await _tasksRepository.UpdateTasksAsync(TasksDtos);
        }
    }
}
