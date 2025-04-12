using FosoolSchool.DTO.Auth;
using FosoolSchool.Models;

namespace FosoolSchool.Services.Interfaces
{
    public interface ITokenService
    {
        tokenDTO GenerateToken(User user);
    }
}
