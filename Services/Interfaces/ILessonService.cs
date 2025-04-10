using FosoolSchool.DTO.Lesson;
using FosoolSchool.Models;

namespace FosoolSchool.Services.Interfaces
{
    public interface ILessonService
    {
        Task<List<UpdateGetLessonDTO>> GetAllAsync();
        Task<UpdateGetLessonDTO> GetByIdAsync(string id);
        Task<List<UpdateGetLessonDTO>> GetBySubjectIdAsync(string subjectId);
        Task AddAsync(CreateLessonDTO dto, string userId);
        Task UpdateAsync(string id, UpdateGetLessonDTO dto, string userId);
        Task DeleteAsync(string id);
    }
}
