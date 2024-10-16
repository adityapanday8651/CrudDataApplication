using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;

namespace CrudDataApplication.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly IBaseRepository<Manager> _repository;
        public ManagerRepository(IBaseRepository<Manager> repository)
        {
            _repository = repository;
        }
        public async Task<ResponseModelDto> AddManagerAsync(ManagerDto ManagerDtos)
        {
            Manager managerModel = new Manager();
            managerModel.ManagerName = ManagerDtos.ManagerName;
            managerModel.Email = ManagerDtos.Email;
            managerModel.Phone = ManagerDtos.Phone;
            managerModel.IsActive = true;
            await _repository.AddAsync(managerModel);
            return CommonUtilityHelper.CreateResponseData(true, "Manager Saved Successfully", managerModel);
        }

        public Task<ResponseModelDto> DeleteManagerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetAllManagerAsync()
        {
            IEnumerable<Manager> lstManager = await _repository.GetAllAsync();
            var lstManagerDto = lstManager.Select(x => new ManagerDto
            {
                ManagerId = x.ManagerId,
                ManagerName = x.ManagerName,
                Email = x.Email,
                Phone = x.Phone,
                IsActive = x.IsActive,
            });
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Manager", lstManagerDto);
        }

        public async Task<ResponseModelDto> GetManagerByIdAsync(int id)
        {
            var managerById = await _repository.GetByIdAsync(id);
            var managerByIdDto = new ManagerDto()
            {
                ManagerId = managerById.ManagerId,
                ManagerName = managerById.ManagerName,
                Email = managerById.Email,
                Phone = managerById.Phone,
                IsActive = managerById.IsActive,
            };
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Manager With ID : {id}", managerByIdDto);
        }

        public Task<ResponseModelDto> TruncateManagerAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateManagerAsync(ManagerDto ManagerDtos)
        {
            Manager managerModel = new Manager();
            managerModel.ManagerId = ManagerDtos.ManagerId;
            managerModel.ManagerName = ManagerDtos.ManagerName;
            managerModel.Email = ManagerDtos.Email;
            managerModel.Phone = ManagerDtos.Phone;
            managerModel.IsActive = true;
            await _repository.AddAsync(managerModel);
            return CommonUtilityHelper.CreateResponseData(true, "Manager Updated Successfully", managerModel);
        }
    }
}
