using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IAddressService
    {
        Task<ResponseModelDto> GetAllAddressAsync();
        Task<ResponseModelDto> GetAddressByIdAsync(int id);
        Task<ResponseModelDto> AddAddressAsync(AddressDto AddressDtos);
        Task<ResponseModelDto> UpdateAddressAsync(AddressDto AddressDtos);
        Task<ResponseModelDto> DeleteAddressAsync(int id);
        Task<ResponseModelDto> TruncateAddressAsync();
    }
}
