using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;

namespace CrudDataApplication.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<ResponseModelDto> AddAddressAsync(AddressDto AddressDtos)
        {
            return await _addressRepository.AddAddressAsync(AddressDtos);
        }

        public async Task<ResponseModelDto> DeleteAddressAsync(int id)
        {
            return await _addressRepository.DeleteAddressAsync(id);
        }

        public async Task<ResponseModelDto> GetAddressByIdAsync(int id)
        {
            return await _addressRepository.GetAddressByIdAsync(id);
        }

        public async Task<ResponseModelDto> GetAllAddressAsync()
        {
            return await _addressRepository.GetAllAddressAsync();
        }

        public async Task<ResponseModelDto> TruncateAddressAsync()
        {
            return await _addressRepository.TruncateAddressAsync();
        }

        public async Task<ResponseModelDto> UpdateAddressAsync(AddressDto AddressDtos)
        {
            return await _addressRepository.UpdateAddressAsync(AddressDtos);
        }
    }
}
