using FosoolSchool.Models;

namespace FosoolSchool.Repository.Interfaces
{
    public interface ILevelRepo
    {
        Task<List<Level>> GetAllAsync();
        Task<Level> GetByIdAsync(string id);
        Task AddAsync(Level level);
        Task UpdateAsync(Level level);
        Task SoftDeleteAsync(string id);
    }
}

