using FosoolSchool.Models;

namespace FosoolSchool.Repository.Interfaces
{
    public interface IAcedemicTermRepo
    {
        Task<List<AcademicTerm>> GetAllAsync();
        Task<AcademicTerm> GetByIdAsync(string id);
        Task AddAsync(AcademicTerm term);
        Task UpdateAsync(AcademicTerm term);
        Task SoftDeleteAsync(string id);
    }
}
