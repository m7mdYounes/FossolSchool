using FosoolSchool.Models.DBContext;
using FosoolSchool.Models.TeacherEntities;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Repository
{
    public class TeacherRepo : ITeacherRepo
    {
        private readonly FossolDB _context;

        public TeacherRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _context.Set<Teacher>()
                .Include(t => t.User)
                .Include(t => t.TeacherTerms).ThenInclude(tt => tt.AcademicTerm)
                .Include(t => t.TeacherSubjects).ThenInclude(ts => ts.Grade).ThenInclude(g => g.Level)
                .Include(t => t.TeacherSubjects).ThenInclude(ts => ts.Subject)
                .Where(t => !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<Teacher> GetByIdAsync(string id)
        {
            return await _context.Set<Teacher>()
                .Include(t => t.User)
                .Include(t => t.TeacherTerms).ThenInclude(tt => tt.AcademicTerm)
                .Include(t => t.TeacherSubjects).ThenInclude(ts => ts.Grade).ThenInclude(g => g.Level)
                .Include(t => t.TeacherSubjects).ThenInclude(ts => ts.Subject)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task AddAsync(Teacher teacher)
        {
            await _context.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }
        public async Task AddSubjectsAsync(IEnumerable<TeacherGradeSubject> subjects)
        {
            await _context.AddRangeAsync(subjects);
            await _context.SaveChangesAsync();
        }

        public async Task AddTermsAsync(IEnumerable<TeacherTerm> terms)
        {
            await _context.AddRangeAsync(terms);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var teacher = await GetByIdAsync(id);
            if (teacher != null)
            {
                teacher.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
