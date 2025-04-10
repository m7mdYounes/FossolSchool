using FosoolSchool.DTO.Level;

namespace FosoolSchool.Services.Interfaces
{
    public interface ILevelService
    {
        Task<List<UpdateGetLevelDTO>> GetAllAsync();
        Task<UpdateGetLevelDTO> GetByIdAsync(string id);
        Task AddAsync(CreateLevelDTO dto, string userId);
        Task UpdateAsync(string id, UpdateGetLevelDTO dto, string userId);
        Task DeleteAsync(string id);
    }
}
