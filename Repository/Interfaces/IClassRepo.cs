using FosoolSchool.Models;
using FosoolSchool.Models.TeacherEntities;

namespace FosoolSchool.Repository.Interfaces
{
    public interface IClassRepo
    {
        Task<List<Class>> GetAllAsync();
        Task<Class> GetByIdAsync(string id);
        Task AddAsync(Class entity);
        Task UpdateAsync(Class entity);
        Task SoftDeleteAsync(string id);
        Task AssignStudentsToClassAsync(string classId, List<string> studentIds);
        Task<List<Student>> GetStudentsByClassIdAsync(string classId);
    }
}
