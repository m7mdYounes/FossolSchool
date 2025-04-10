using FosoolSchool.Models;
using FosoolSchool.Models.DBContext;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Repository
{
    public class GradeRepo:IGradeRepo
    {
        private readonly FossolDB _context;

        public GradeRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<List<Grade>> GetAllAsync()
        {
            return await _context.Set<Grade>().Include(g => g.Level).Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Grade> GetByIdAsync(string id)
        {
            return await _context.Set<Grade>().Include(g => g.Level).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
        public async Task<List<Grade>> GetByLevelIdAsync(string levelId)
        {
            return await _context.Set<Grade>().Include(g => g.Level).Where(x => x.LevelId == levelId && !x.IsDeleted).ToListAsync();
        }

        public async Task AddAsync(Grade grade)
        {
            await _context.AddAsync(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Grade grade)
        {
            _context.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var grade = await GetByIdAsync(id);
            if (grade != null)
            {
                grade.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
