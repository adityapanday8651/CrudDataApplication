using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;
using Microsoft.EntityFrameworkCore;

namespace CrudDataApplication.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly IBaseRepository<Medicine> _repository;

        private readonly AppDbContext _context;
        public MedicineRepository(IBaseRepository<Medicine> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        protected DbSet<Medicine> DbSet() => _context.Medicines;

        public async Task<ResponseModelDto> AddMedicineAsync(MedicineDto MedicineDto)
        {
            Medicine medicine = new Medicine();
            medicine.Name = MedicineDto.Name;
            medicine.Category = MedicineDto.Category;
            medicine.Price = MedicineDto.Price;
            medicine.Manufacturer = MedicineDto.Manufacturer;
            medicine.ExpiryDate = MedicineDto.ExpiryDate;
            medicine.ImageUrl = MedicineDto.ImageUrl;
            medicine.IsActive = true;
            await _repository.AddAsync(medicine);
            return CommonUtilityHelper.CreateResponseData(true, "Medicine Saved Successfully", medicine);
        }

        public async Task<ResponseModelDto> DeleteMedicineAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return CommonUtilityHelper.CreateResponseData(true, $"Deleted Medicine With ID : {id}", id);
        }

        public async Task<ResponseModelDto> GetAllMedicinesAsync()
        {
            const int defaultPageNumber = 1;
            const int defaultPageSize = 10;
            var lstMedicines = await _repository.GetAllAsync();
            int totalCount = lstMedicines.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)defaultPageSize);
            List<MedicineDto> medicineDtos = lstMedicines
                .OrderByDescending(x => x.Id)
                .Skip((defaultPageNumber - 1) * defaultPageSize)
                .Take(defaultPageSize)
                .Select(x => new MedicineDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    Manufacturer = x.Manufacturer,
                    ExpiryDate = x.ExpiryDate,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive,
                }).ToList();
            var paginationMetadata = new
            {
                TotalCount = totalCount,
                PageSize = defaultPageSize,
                CurrentPage = defaultPageNumber,
                TotalPages = totalPages,
                HasNext = defaultPageNumber < totalPages,
                HasPrevious = false // Always false for the first page
            };
            var paginationWithMedicineData = new
            {
                Medicines = medicineDtos,
                Pagination = paginationMetadata
            };
            return CommonUtilityHelper.CreateResponseData(true, "Retrieved paginated Medicines", paginationWithMedicineData);
        }

        public async Task<ResponseModelDto> GetMedicineByIdAsync(int id)
        {
            var medicalById = await _repository.GetByIdAsync(id);
            var medicineDtoCheck = new MedicineDto()
            {
                Id = medicalById.Id,
                Name = medicalById.Name,
                Category = medicalById.Category,
                Price = medicalById.Price,
                Manufacturer = medicalById.Manufacturer,
                ExpiryDate = medicalById.ExpiryDate,
                ImageUrl = medicalById.ImageUrl,
                IsActive = medicalById.IsActive,
            };
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Medicines With Name and ID : {id}", medicineDtoCheck);
        }

        public async Task<ResponseModelDto> UpdateMedicineAsync(MedicineDto MedicineDto)
        {
            Medicine medicine = new Medicine();
            medicine.Id = MedicineDto.Id;
            medicine.Name = MedicineDto.Name;
            medicine.Category = MedicineDto.Category;
            medicine.Price = MedicineDto.Price;
            medicine.Manufacturer = MedicineDto.Manufacturer;
            medicine.ExpiryDate = MedicineDto.ExpiryDate;
            medicine.ImageUrl = MedicineDto.ImageUrl;
            medicine.IsActive = MedicineDto.IsActive;
            await _repository.UpdateAsync(medicine);
            return CommonUtilityHelper.CreateResponseData(true, "Medicines Updated Successfully", medicine);
        }

        public async Task<ResponseModelDto> TruncateMedicineAsync()
        {
            try
            {
                await _repository.TruncateAsync();
                return CommonUtilityHelper.CreateResponseData(true, "All Medicine truncated successfully", null);
            }
            catch (Exception ex)
            {
                return CommonUtilityHelper.CreateResponseData(false, $"Error truncating Medicine: {ex.Message}", null);
            }
        }

        public async Task<ResponseModelDto> DeleteMedicineAndUpdateAsync(int id)
        {
            Medicine medicalById = await _repository.GetByIdAsync(id);
            medicalById.IsActive = !medicalById.IsActive;
            await _repository.UpdateAsync(medicalById);
            return CommonUtilityHelper.CreateResponseData(true, "Medicines Enabled or Disabled Successfully", medicalById);
        }

        public async Task<ResponseModelDto> GetAllIsActiveMedicinesAsync()
        {
            var GetAllMedicines = await _repository.GetAllAsync();
            var GetAllIsActiveMedicines = GetAllMedicines.Where(x => x.IsActive == true).ToList();
            return CommonUtilityHelper.CreateResponseData(true, "Get All IsActive Medicines Successfully", GetAllIsActiveMedicines);
        }
    }
}
