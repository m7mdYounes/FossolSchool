using FosoolSchool.Models;

namespace FosoolSchool.Repository.Interfaces
{
    public interface ISubjectRepo
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject> GetByIdAsync(string id); 
        Task<List<Subject>> GetByGradeIdAsync(string gradeId);
        Task AddAsync(Subject subject);
        Task UpdateAsync(Subject subject);
        Task SoftDeleteAsync(string id);
    }
}
