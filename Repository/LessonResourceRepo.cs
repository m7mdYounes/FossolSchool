using FosoolSchool.Models.DBContext;
using FosoolSchool.Models.Lesson;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Repository
{
    public class LessonResourceRepo : ILessonResourceRepo
    {
        private readonly FossolDB _context;

        public LessonResourceRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<List<LessonResource>> GetByLessonIdAsync(string lessonId)
        {
            return await _context.LessonResources
                .Where(r => r.LessonId == lessonId && !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<string>> GetHiddenResourceIdsAsync(string teacherId, string lessonId)
        {
            return await _context.TeacherLessonResourceViews
                .Where(v => v.TeacherId == teacherId && !v.IsVisible && v.Resource.LessonId == lessonId)
                .Select(v => v.ResourceId)
                .ToListAsync();
        }


        public async Task HideResourceForTeacherAsync(string teacherId, string resourceId)
        {
            var view = await _context.TeacherLessonResourceViews
                .FirstOrDefaultAsync(v => v.TeacherId == teacherId && v.ResourceId == resourceId);

            if (view == null)
            {
                _context.TeacherLessonResourceViews.Add(new TeacherLessonResourceView
                {
                    TeacherId = teacherId,
                    ResourceId = resourceId,
                    IsVisible = false,
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedUserId = teacherId,
                    CreatedUserId = teacherId,
                    CreatedAt = DateTime.UtcNow,
                });
            }
            else
            {
                view.IsVisible = false;
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(LessonResource resource)
        {
            await _context.LessonResources.AddAsync(resource);
            await _context.SaveChangesAsync();
        }
    }
}
