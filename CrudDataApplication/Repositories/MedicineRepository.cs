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
            medicine.IsActive = MedicineDto.IsActive;
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
            int totalCount = await DbSet().CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)defaultPageSize);
            List<MedicineDto> medicineDtos = await DbSet()
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
                })
                .AsNoTracking()
                .ToListAsync();
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
            var medicineDto = await DbSet()
                .Where(x => x.Id == id)
                .Select(x => new MedicineDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    Manufacturer = x.Manufacturer,
                    ExpiryDate = x.ExpiryDate,
                    ImageUrl = x.ImageUrl,
                })
                .AsNoTracking().
                FirstOrDefaultAsync();
            return CommonUtilityHelper.CreateResponseData(true, $"Retrieve Category With Name and ID : {id}", medicineDto);
        }

        public async Task<ResponseModelDto> UpdateMedicineAsync(MedicineDto MedicineDto)
        {
            Medicine medicine = new Medicine();
            medicine.Name = MedicineDto.Name;
            medicine.Category = MedicineDto.Category;
            medicine.Price = MedicineDto.Price;
            medicine.Manufacturer = MedicineDto.Manufacturer;
            medicine.ExpiryDate = MedicineDto.ExpiryDate;
            medicine.ImageUrl = MedicineDto.ImageUrl;
            await _repository.UpdateAsync(medicine);
            return CommonUtilityHelper.CreateResponseData(true, "Category Updated Successfully", medicine);
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
    }
}
