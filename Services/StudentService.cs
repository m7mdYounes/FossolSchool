using FosoolSchool.DTO.Student;
using FosoolSchool.Models.TeacherEntities;
using FosoolSchool.Models;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FosoolSchool.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _repository;
        private readonly IUserRepo _userRepo;
        private readonly IAuthService _authService;

        public StudentService(IStudentRepo repository, IUserRepo userRepo , IAuthService authService)
        {
            _repository = repository;
            _userRepo = userRepo;
            _authService = authService;
        }

        public async Task<List<UpdateGetStudentDTO>> GetAllAsync()
        {
            var students = await _repository.GetAllAsync();
            return students.Select(s => new UpdateGetStudentDTO
            {
                Id = s.Id,
                UserId = s.UserId,
                UserName = s.User?.UserName,
                TeacherId = s.TeacherId,
                TeacherName = s.Teacher?.User?.UserName,
                ClassId = s.ClassId,
                ClassName = s.Class?.Name
            }).ToList();
        }

        public async Task<UpdateGetStudentDTO> GetByIdAsync(string id)
        {
            var s = await _repository.GetByIdAsync(id);
            return s == null ? null : new UpdateGetStudentDTO
            {
                Id = s.Id,
                UserId = s.UserId,
                UserName = s.User?.UserName,
                TeacherId = s.TeacherId,
                TeacherName = s.Teacher?.User?.UserName,
                ClassId = s.ClassId,
                ClassName = s.Class?.Name
            };
        }

        public async Task<List<UpdateGetStudentDTO>> GetByTeacherIdAsync(string teacherId)
        {
            var students = await _repository.GetByTeacherIdAsync(teacherId);
            return students.Select(s => new UpdateGetStudentDTO
            {
                Id = s.Id,
                UserId = s.UserId,
                UserName = s.User?.UserName,
                TeacherId = s.TeacherId,
                TeacherName = s.Teacher?.User?.UserName,
                ClassId = s.ClassId,
                ClassName = s.Class?.Name
            }).ToList();
        }

        public async Task AddAsync(CreateStudentDTO dto, string userId)
        {
            if (dto.UserEmail == null) dto.UserEmail = $"{dto.UserName}student@example.com";
            if (dto.Password == null) dto.Password = $"{dto.UserName}@1234";
            var usermodel = new RegisterDTO()
            {
                UserName = dto.UserName,
                Email = dto.UserEmail,
                Role = UserRole.Student.ToString(),
                Password = dto.Password,
            };
            var (response, registeredUserId) = await _authService.RegisterAsync(usermodel);

            if (!response.IsValid)
            {
                throw new Exception(response.Error);
            }

            var student = new Student
            {
                Id = Guid.NewGuid().ToString(),
                UserId = registeredUserId,
                TeacherId = dto.TeacherId,
                ClassId = dto.ClassId,
                CreatedAt = DateTime.UtcNow,
                CreatedUserId = userId
            };

            await _repository.AddAsync(student);
        }
        
        public async Task UpdateAsync(string id, UpdateGetStudentDTO dto, string userId)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null) return;
            student.UserId = dto.UserId;
            student.TeacherId = dto.TeacherId;
            student.ClassId = dto.ClassId;
            student.UpdatedAt = DateTime.UtcNow;
            student.UpdatedUserId = userId;
            await _repository.UpdateAsync(student);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.SoftDeleteAsync(id);
        }
    }
}
