using FosoolSchool.Models;
using FosoolSchool.Models.DBContext;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Repository
{
    public class LevelRepo:ILevelRepo
    {
        private readonly FossolDB _context;

        public LevelRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<List<Level>> GetAllAsync()
        {
            return await _context.Set<Level>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Level> GetByIdAsync(string id)
        {
            return await _context.Set<Level>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task AddAsync(Level level)
        {
            await _context.AddAsync(level);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Level level)
        {
            _context.Update(level);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var level = await GetByIdAsync(id);
            if (level != null)
            {
                level.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
