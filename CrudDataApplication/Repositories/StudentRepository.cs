using CrudDataApplication.DataContext;
using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using CrudDataApplication.Services;
using Microsoft.EntityFrameworkCore;

namespace CrudDataApplication.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly IBaseRepository<Student> _repository;

        private readonly AppDbContext _context;
        public StudentRepository(IBaseRepository<Student> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public Task<ResponseModelDto> AddStudentAsync(StudentDto studentDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> DeleteStudentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetAllStudentsAsync()
        {
            var lstStudentDto = await DbSet().Select(x => new StudentDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CourseName = x.CourseName,
            }).ToListAsync();
            return CommonUtilityHelper.CreateResponseData(true, "Retrieve all Data", lstStudentDto);
        }


        public Task<ResponseModelDto> GetStudentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> TruncateStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> UpdateStudentAsync(StudentDto studentDto)
        {
            throw new NotImplementedException();
        }

        protected DbSet<Student> DbSet() => _context.Students;


    }
}
