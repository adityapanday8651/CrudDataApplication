using CrudDataApplication.Dto;
using CrudDataApplication.Models;

namespace CrudDataApplication.Interfaces
{
    public interface IProductService
    {
        Task<ResponseModelDto> GetAllProductsAsync();
        Task<ResponseModelDto> GetProductByIdAsync(int id);
        Task<ResponseModelDto> AddProductAsync(ProductDto productDto);
        Task<ResponseModelDto> UpdateProductAsync(ProductDto productDto);
        Task<ResponseModelDto> DeleteProductAsync(int id);
        Task<ResponseModelDto> TruncateProductAsync();
    }
}
