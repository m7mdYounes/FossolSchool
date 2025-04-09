using FosoolSchool.Models;

namespace FosoolSchool.Repository.Interfaces
{
    public interface IUserRepo
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
