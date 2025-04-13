using FosoolSchool.DTO.Lesson;

namespace FosoolSchool.Services.Interfaces
{
    public interface ILessonResourceService
    {
        Task<ResponseDTO> GetResourcesForTeacherAsync(string lessonId, string teacherId);
        Task<ResponseDTO> AddResourceAsync(CreateLessonResourceDTO dto, string userId);
        Task<ResponseDTO> HideResourceAsync(string teacherId, string resourceId);
    }
}
