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
        private readonly ITeacherService _teacherService;

        public StudentService(IStudentRepo repository, IUserRepo userRepo , IAuthService authService,ITeacherService teacherService)
        {
            _repository = repository;
            _userRepo = userRepo;
            _authService = authService;
            _teacherService = teacherService;
        }

        public async Task<List<UpdateGetStudentDTO>> GetAllAsync()
        {
            var students = await _repository.GetAllAsync();
            var allTeachers = await _teacherService.GetAllAsync();
            var teacherDict = allTeachers
                .Where(t => !string.IsNullOrEmpty(t.Id))
                .ToDictionary(t => t.Id, t => t.UserName); 

            var result = students.Select(s => new UpdateGetStudentDTO
            {
                Id = s.Id,
                UserName = s.User?.UserName,
                TeacherId = s.TeacherId,
                TeacherName = s.TeacherId != null && teacherDict.ContainsKey(s.TeacherId)
                    ? teacherDict[s.TeacherId]
                    : null,
                ClassId = s.ClassId,
                ClassName = s.Class?.Name
            }).ToList();

            return result;
        }


        public async Task<UpdateGetStudentDTO> GetByIdAsync(string id)
        {
            var s = await _repository.GetByIdAsync(id);
            if (s == null)
                return null;

            string teacherName = null;

            if (!string.IsNullOrEmpty(s.TeacherId))
            {
                var teacher = await _teacherService.GetByIdAsync(s.TeacherId);
                teacherName = teacher?.UserName;
            }

            return new UpdateGetStudentDTO
            {
                Id = s.Id,
                UserName = s.User?.UserName,
                TeacherId = s.TeacherId,
                TeacherName = teacherName,
                ClassId = s.ClassId,
                ClassName = s.Class?.Name
            };
        }


        public async Task<List<UpdateGetStudentDTO>> GetByTeacherIdAsync(string teacherId)
        {
            var students = await _repository.GetByTeacherIdAsync(teacherId);

            // Get teacher name once based on teacherId
            var teacher = await _teacherService.GetByIdAsync(teacherId);
            var teacherName = teacher?.UserName;

            return students.Select(s => new UpdateGetStudentDTO
            {
                Id = s.Id,
                UserName = s.User?.UserName,
                TeacherId = s.TeacherId,
                TeacherName = teacherName, 
                ClassId = s.ClassId,
                ClassName = s.Class?.Name
            }).ToList();
        }


        public async Task AddAsync(CreateStudentDTO dto, string userId)
        {
            if (dto.UserEmail == null) dto.UserEmail = $"{Guid.NewGuid}@example.com";
            if (dto.Password == null) dto.Password = $"{Guid.NewGuid}";
            var usermodel = new RegisterDTO()
            {
                UserName = dto.UserName,
                Email = dto.UserEmail,
                Role = UserRole.Student.ToString(),
                Password = dto.Password,
            };
            var (response, registeredUserId) = await _authService.RegisterAsync(usermodel,userId);

            if (!response.IsValid)
            {
                throw new Exception(response.Error);
            }

            var student = new Student
            {
                Id = registeredUserId,
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
            student.UserId = dto.Id;
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
