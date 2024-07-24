using CrudDataApplication.Dto;

namespace CrudDataApplication.Interfaces
{
    public interface IStudentRepository
    {
        Task<ResponseModelDto> GetAllStudentsAsync();
        Task<ResponseModelDto> GetStudentByIdAsync(int id);
        Task<ResponseModelDto> AddStudentAsync(StudentDto studentDto);
        Task<ResponseModelDto> UpdateStudentAsync(StudentDto studentDto);
        Task<ResponseModelDto> DeleteStudentAsync(int id);
        Task<ResponseModelDto> TruncateStudentsAsync();
    }
}
