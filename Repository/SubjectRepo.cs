using FosoolSchool.Models;
using FosoolSchool.Models.DBContext;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Repository
{
    public class SubjectRepo : ISubjectRepo
    {
        
        private readonly FossolDB _context;

        public SubjectRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _context.Set<Subject>()
                .Include(s => s.Grade)
                .ThenInclude(g => g.Level)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<Subject> GetByIdAsync(string id)
        {
            return await _context.Set<Subject>()
                .Include(s => s.Grade)
                .ThenInclude(g => g.Level)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
        public async Task<List<Subject>> GetByGradeIdAsync(string gradeId)
        {
            return await _context.Set<Subject>()
                .Include(s => s.Grade)
                .ThenInclude(g => g.Level)
                .Where(s => s.GradeId == gradeId && !s.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(Subject subject)
        {
            await _context.AddAsync(subject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subject subject)
        {
            _context.Update(subject);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var subject = await GetByIdAsync(id);
            if (subject != null)
            {
                subject.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
