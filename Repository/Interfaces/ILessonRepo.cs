using FosoolSchool.Models;

namespace FosoolSchool.Repository.Interfaces
{
    public interface ILessonRepo
    {
        Task<List<Lesson>> GetAllAsync();
        Task<Lesson> GetByIdAsync(string id);
        Task<List<Lesson>> GetBySubjectIdAsync(string subjectId);
        Task AddAsync(Lesson lesson);
        Task UpdateAsync(Lesson lesson);
        Task SoftDeleteAsync(string id);
    }
}
