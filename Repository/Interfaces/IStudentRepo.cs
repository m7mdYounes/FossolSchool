using FosoolSchool.Models.TeacherEntities;

namespace FosoolSchool.Repository.Interfaces
{
    public interface IStudentRepo
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(string id);
        Task<List<Student>> GetByTeacherIdAsync(string teacherId);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task SoftDeleteAsync(string id);
    }
}
