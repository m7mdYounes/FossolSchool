using FosoolSchool.DTO.Class;
using FosoolSchool.DTO.Student;

namespace FosoolSchool.Services.Interfaces
{
    public interface IClassService
    {
        Task<List<UpdateGetClassDTO>> GetAllAsync();
        Task<UpdateGetClassDTO> GetByIdAsync(string id);
        Task AddAsync(CreateClassDTO dto, string teacherId);
        Task UpdateAsync(string id, UpdateGetClassDTO dto);
        Task DeleteAsync(string id);
        Task AssignStudentsToClassAsync(string classId, List<string> studentIds);
        Task<List<UpdateGetStudentDTO>> GetStudentByClassIdAsync(string classId);
    }
}
