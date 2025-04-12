using FosoolSchool.Models.TeacherEntities;

namespace FosoolSchool.Repository.Interfaces
{
    public interface ITeacherRepo
    {
        Task<List<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(string id);
        Task AddAsync(Teacher teacher);
        Task AddSubjectsAsync(IEnumerable<TeacherGradeSubject> subjects);
        Task AddTermsAsync(IEnumerable<TeacherTerm> terms);
        Task UpdateAsync(Teacher teacher);
        Task SoftDeleteAsync(string id);
    }
}
