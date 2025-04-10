using FosoolSchool.Models.DBContext;
using FosoolSchool.Models.TeacherEntities;
using FosoolSchool.Models;
using FosoolSchool.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly FossolDB _context;

        public AuthService(
            IUserRepo userRepository,
            IPasswordHasher<User> passwordHasher,
            ITokenService tokenService,
            FossolDB context)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<(ResponseDTO,string)> RegisterAsync(RegisterDTO model)
        {
            var existingUser = await _userRepository.GetByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return new (new ResponseDTO { IsValid = false, Error = "Email already exists" },"");
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                UserEmail = model.Email,
                UserRole = (UserRole)Enum.Parse(typeof(UserRole), model.Role)
            };

            user.Password = _passwordHasher.HashPassword(user, model.Password);
            await _userRepository.AddAsync(user);

            if (model.Role == "Teacher")
            {
                await _context.Teachers.AddAsync(new Teacher { UserId = user.Id });
            }
            else if (model.Role == "SuperAdmin")
            {
                // future admin logic
            }

            await _userRepository.SaveChangesAsync();

            var token = _tokenService.GenerateToken(user);
            return new( new ResponseDTO { IsValid = true, Data = token, Message = "Registered successfully" },user.Id);
        }

        public async Task<ResponseDTO> LoginAsync(LoginDTO model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);
            if (user == null)
            {
                return new ResponseDTO { IsValid = false, Error = "Invalid credentials" };
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return new ResponseDTO { IsValid = false, Error = "Invalid credentials" };
            }

            var token = _tokenService.GenerateToken(user);
            return new ResponseDTO { IsValid = true, Data = token, Message = "Login successful" };
        }
    }

}
