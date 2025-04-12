namespace FosoolSchool.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(ResponseDTO, string)> RegisterAsync(RegisterDTO model,string CreatedUserId);
        Task<ResponseDTO> LoginAsync(LoginDTO model);
    }
}
