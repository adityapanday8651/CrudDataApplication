using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;

namespace CrudDataApplication.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        public MedicineService(IMedicineRepository mediicineRepository)
        {
            _medicineRepository = mediicineRepository;
        }
        public async Task<ResponseModelDto> AddMedicineAsync(MedicineDto MedicineDto)
        {
            return await _medicineRepository.AddMedicineAsync(MedicineDto);
        }

        public async Task<ResponseModelDto> DeleteMedicineAndUpdateAsync(int id)
        {
           return await _medicineRepository.DeleteMedicineAndUpdateAsync(id);
        }

        public async Task<ResponseModelDto> DeleteMedicineAsync(int id)
        {
            return await _medicineRepository.DeleteMedicineAsync(id);
        }

        public async Task<ResponseModelDto> GetAllIsActiveMedicinesAsync()
        {
            return await _medicineRepository.GetAllIsActiveMedicinesAsync();
        }

        public async Task<ResponseModelDto> GetAllMedicinesAsync()
        {
            return await _medicineRepository.GetAllMedicinesAsync();
        }

        public async Task<ResponseModelDto> GetMedicineByIdAsync(int id)
        {
            return await _medicineRepository.GetMedicineByIdAsync(id);
        }

        public async Task<ResponseModelDto> TruncateMedicineAsync()
        {
           return await _medicineRepository.TruncateMedicineAsync();
        }

        public async Task<ResponseModelDto> UpdateMedicineAsync(MedicineDto MedicineDto)
        {
            return await _medicineRepository.UpdateMedicineAsync(MedicineDto);
        }
    }
}
