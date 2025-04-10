using FosoolSchool.DTO.Student;

namespace FosoolSchool.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<UpdateGetStudentDTO>> GetAllAsync();
        Task<UpdateGetStudentDTO> GetByIdAsync(string id);
        Task<List<UpdateGetStudentDTO>> GetByTeacherIdAsync(string teacherId);
        Task AddAsync(CreateStudentDTO dto, string userId);
        Task UpdateAsync(string id, UpdateGetStudentDTO dto, string userId);
        Task DeleteAsync(string id);
    }
}
