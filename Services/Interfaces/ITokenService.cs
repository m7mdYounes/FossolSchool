using FosoolSchool.Models;

namespace FosoolSchool.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
