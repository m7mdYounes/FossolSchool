using FosoolSchool.Models.DBContext;
using FosoolSchool.Models;
using FosoolSchool.Repository.Interfaces;
namespace FosoolSchool.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly FossolDB _context;

        public UserRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task<List<User>> GetUsersByIdsAsync(List<string> ids)
        {
            return await _context.Users
                .Where(u => ids.Contains(u.Id) && !u.IsDeleted)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
