using FosoolSchool.DTO.Grade;

namespace FosoolSchool.Services.Interfaces
{
    public interface IGradeService
    {
        Task<List<UpdateGetGradeDTO>> GetAllAsync();
        Task<UpdateGetGradeDTO> GetByIdAsync(string id);
        Task<List<UpdateGetGradeDTO>> GetByLevelIdAsync(string levelId);
        Task AddAsync(CreateGradeDTO dto, string userId);
        Task UpdateAsync(string id, UpdateGetGradeDTO dto, string userId);
        Task DeleteAsync(string id);
    }
}
