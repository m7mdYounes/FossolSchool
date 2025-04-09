namespace FosoolSchool.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO> RegisterAsync(RegisterDTO model);
        Task<ResponseDTO> LoginAsync(LoginDTO model);
    }
}
