using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IAddressRepository
    {
        Task<ResponseModelDto> GetAllAddressAsync();
        Task<ResponseModelDto> GetAddressByIdAsync(int id);
        Task<ResponseModelDto> AddAddressAsync(AddressDto AddressDtos);
        Task<ResponseModelDto> UpdateAddressAsync(AddressDto AddressDtos);
        Task<ResponseModelDto> DeleteAddressAsync(int id);
        Task<ResponseModelDto> TruncateAddressAsync();
    }
}
