using FosoolSchool.Models.Lesson;

namespace FosoolSchool.Repository.Interfaces
{
    public interface ILessonResourceRepo
    {
        Task<List<LessonResource>> GetByLessonIdAsync(string lessonId);
        Task<List<string>> GetHiddenResourceIdsAsync(string teacherId,string lessonId);
        Task HideResourceForTeacherAsync(string teacherId, string resourceId);
        Task AddAsync(LessonResource resource);
    }
}
