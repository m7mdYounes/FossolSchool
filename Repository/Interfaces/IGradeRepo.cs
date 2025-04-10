using FosoolSchool.Models;

namespace FosoolSchool.Repository.Interfaces
{
    public interface IGradeRepo
    {
        Task<List<Grade>> GetAllAsync();
        Task<Grade> GetByIdAsync(string id);
        Task<List<Grade>> GetByLevelIdAsync(string levelId);
        Task AddAsync(Grade grade);
        Task UpdateAsync(Grade grade);
        Task SoftDeleteAsync(string id);
    }
}
