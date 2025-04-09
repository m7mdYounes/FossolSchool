using FosoolSchool.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FosoolSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result = await _authService.LoginAsync(model);
            return Ok(result);
        }
    }
}
