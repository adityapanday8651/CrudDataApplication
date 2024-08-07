using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IMedicineService
    {
        Task<ResponseModelDto> GetAllMedicinesAsync();
        Task<ResponseModelDto> GetMedicineByIdAsync(int id);
        Task<ResponseModelDto> AddMedicineAsync(MedicineDto MedicineDto);
        Task<ResponseModelDto> UpdateMedicineAsync(MedicineDto MedicineDto);
        Task<ResponseModelDto> DeleteMedicineAsync(int id);
    }
}
