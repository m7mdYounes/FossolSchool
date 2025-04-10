namespace FosoolSchool.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(ResponseDTO, string)> RegisterAsync(RegisterDTO model);
        Task<ResponseDTO> LoginAsync(LoginDTO model);
    }
}
