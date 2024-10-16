using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;

namespace CrudDataApplication.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IBaseRepository<Address> _repository;
        public AddressRepository(IBaseRepository<Address> repository)
        {
            _repository = repository;
        }
        public async Task<ResponseModelDto> AddAddressAsync(AddressDto AddressDtos)
        {
            Address address = new Address();
            address.Street = AddressDtos.Street;
            address.City = AddressDtos.City;
            address.State = AddressDtos.State;
            address.Zip = AddressDtos.Zip;
            address.IsActive = true;
            await _repository.AddAsync(address);
            return CommonUtilityHelper.CreateResponseData(true, "Address Saved Successfully", address);
        }

        public async Task<ResponseModelDto> DeleteAddressAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return CommonUtilityHelper.CreateResponseData(true, $"Deleted Address With ID : {id}", id);
        }

        public async Task<ResponseModelDto> GetAddressByIdAsync(int id)
        {
            var addressById = await _repository.GetByIdAsync(id);
            var addressByIdDto = new AddressDto()
            {
                AddressId = addressById.AddressId,
                Street = addressById.Street,
                City = addressById.City,
                State = addressById.State,
                Zip = addressById.Zip,
                IsActive = addressById.IsActive
            };
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Address With ID : {id}", addressByIdDto);
        }
        public async Task<ResponseModelDto> GetAllAddressAsync()
        {
            IEnumerable<Address> lstAllAddress = await _repository.GetAllAsync();
            var lstAddressDtos = lstAllAddress.Select(x => new AddressDto
            {
                AddressId = x.AddressId,
                Street = x.Street,
                State = x.State,
                City = x.City,
                Zip = x.Zip,
                IsActive = x.IsActive
            });
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Address", lstAddressDtos);
        }

        public Task<ResponseModelDto> TruncateAddressAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateAddressAsync(AddressDto AddressDtos)
        {
            Address address = new Address();
            address.AddressId = AddressDtos.AddressId;
            address.Street = AddressDtos.Street;
            address.City = AddressDtos.City;
            address.State = AddressDtos.State;
            address.Zip = AddressDtos.Zip;
            address.IsActive = AddressDtos.IsActive;
            await _repository.UpdateAsync(address);
            return CommonUtilityHelper.CreateResponseData(true, "Address Updated Successfully", address);
        }
    }
}
