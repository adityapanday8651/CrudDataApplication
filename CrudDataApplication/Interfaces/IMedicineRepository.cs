using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IMedicineRepository
    {
        Task<ResponseModelDto> GetAllMedicinesAsync();
        Task<ResponseModelDto> GetMedicineByIdAsync(int id);
        Task<ResponseModelDto> AddMedicineAsync(MedicineDto MedicineDto);
        Task<ResponseModelDto> UpdateMedicineAsync(MedicineDto MedicineDto);
        Task<ResponseModelDto> DeleteMedicineAsync(int id);
        Task<ResponseModelDto> TruncateMedicineAsync();
        Task<ResponseModelDto> DeleteMedicineAndUpdateAsync(int id);
        Task<ResponseModelDto> GetAllIsActiveMedicinesAsync();
    }
}
