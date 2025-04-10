using FosoolSchool.Models;
using FosoolSchool.Models.DBContext;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Repository
{
    public class LessonRepo : ILessonRepo
    {
        private readonly FossolDB _context;

        public LessonRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _context.Set<Lesson>()
                .Include(l => l.Subject)
                .ThenInclude(s => s.Grade)
                .ThenInclude(g => g.Level)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<Lesson> GetByIdAsync(string id)
        {
            return await _context.Set<Lesson>()
                .Include(l => l.Subject)
                .ThenInclude(s => s.Grade)
                .ThenInclude(g => g.Level)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<Lesson>> GetBySubjectIdAsync(string subjectId)
        {
            return await _context.Set<Lesson>()
                .Include(l => l.Subject)
                .ThenInclude(s => s.Grade)
                .ThenInclude(g => g.Level)
                .Where(x => x.SubjectId == subjectId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(Lesson lesson)
        {
            await _context.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lesson lesson)
        {
            _context.Update(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var lesson = await GetByIdAsync(id);
            if (lesson != null)
            {
                lesson.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
