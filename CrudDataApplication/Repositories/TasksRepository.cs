using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;

namespace CrudDataApplication.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly IBaseRepository<Tasks> _repository;
        public TasksRepository(IBaseRepository<Tasks> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModelDto> AddTasksAsync(TasksDto TasksDtos)
        {
            Tasks tasks = new Tasks();
            tasks.TaskName = TasksDtos.TaskName;
            tasks.Deadline = TasksDtos.Deadline;
            tasks.Completed = TasksDtos.Completed;
            tasks.IsActive = true;
            await _repository.AddAsync(tasks);
            return CommonUtilityHelper.CreateResponseData(true, "Tasks Saved Successfully", tasks);
        }

        public Task<ResponseModelDto> DeleteTasksAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetAllTasksAsync()
        {
            var tasksList = await _repository.GetAllAsync();
            var tasksDtoList = tasksList.Select(x => new TasksDto
            {
                TaskId = x.TaskId,
                TaskName = x.TaskName,
                Deadline = x.Deadline,
                Completed = x.Completed,
                IsActive = x.IsActive,
            });
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Tasks", tasksDtoList);
        }


        public async Task<ResponseModelDto> GetTasksByIdAsync(int id)
        {
            var taskById = await _repository.GetByIdAsync(id);
            var taskDtoById = new TasksDto()
            {
                TaskId = taskById.TaskId,
                TaskName = taskById.TaskName,
                Deadline = taskById.Deadline,
                Completed = taskById.Completed,
                IsActive = taskById.IsActive,
            };
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve tasks With ID : {id}", taskDtoById);
        }

        public Task<ResponseModelDto> TruncateTasksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateTasksAsync(TasksDto TasksDtos)
        {
            Tasks tasks = new Tasks();
            tasks.TaskId = TasksDtos.TaskId;
            tasks.TaskName = TasksDtos.TaskName;
            tasks.Deadline = TasksDtos.Deadline;
            tasks.Completed = TasksDtos.Completed;
            tasks.IsActive = TasksDtos.IsActive;
            await _repository.UpdateAsync(tasks);
            return CommonUtilityHelper.CreateResponseData(true, "Tasks Updated Successfully", tasks);
        }
    }
}
