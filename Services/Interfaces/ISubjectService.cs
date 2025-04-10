using FosoolSchool.DTO.Subject;

namespace FosoolSchool.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<UpdateGetSubjectDTO>> GetByGradeIdAsync(string gradeId);
        Task<List<UpdateGetSubjectDTO>> GetAllAsync();
        Task<UpdateGetSubjectDTO> GetByIdAsync(string id);
        Task AddAsync(CreateSubjectDTO dto, string userId);
        Task UpdateAsync(string id, UpdateGetSubjectDTO dto, string userId);
        Task DeleteAsync(string id);
    }
}
