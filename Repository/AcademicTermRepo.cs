using FosoolSchool.Models;
using FosoolSchool.Models.DBContext;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Repository
{
    public class AcademicTermRepo : IAcedemicTermRepo
    {
        private readonly FossolDB _context;

        public AcademicTermRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<List<AcademicTerm>> GetAllAsync()
        {
            return await _context.Set<AcademicTerm>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<AcademicTerm> GetByIdAsync(string id)
        {
            return await _context.Set<AcademicTerm>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task AddAsync(AcademicTerm term)
        {
            await _context.AddAsync(term);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AcademicTerm term)
        {
            _context.Update(term);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var term = await GetByIdAsync(id);
            if (term != null)
            {
                term.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
