using FosoolSchool.DTO.Teacher;
using FosoolSchool.Models;
using FosoolSchool.Models.TeacherEntities;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services;
using FosoolSchool.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FosoolSchool.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepo _repository;
        private readonly IAuthService _authService;

        public TeacherService(ITeacherRepo repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public async Task<List<TeacherViewDTO>> GetAllAsync()
        {
            var teachers = await _repository.GetAllAsync();
            return teachers.Select(t => new TeacherViewDTO
            {
                Id = t.Id,
                UserName = t.User?.UserName,
                UserEmail = t.User?.UserEmail,
                AcademicTerms = t.TeacherTerms.Select(tt => tt.AcademicTerm?.Name).Distinct().ToList(),
                LevelsGradesSubjects = t.TeacherSubjects
                    .GroupBy(s => s.Grade.Level)
                    .Select(lg => new LevelWithGradesDto
                    {
                        Id = lg.Key?.Id,
                        Name = lg.Key?.Name,
                        Grades = lg.GroupBy(g => g.Grade)
                            .Select(g => new GradeWithSubjectsDto
                            {
                                Id = g.Key?.Id,
                                Name = g.Key?.Name,
                                Subjects = g.Select(sub => new SubjectMiniDto
                                {
                                    Id = sub.Subject?.Id,
                                    Name = sub.Subject?.Name
                                }).ToList()
                            }).ToList()
                    }).ToList()
            }).ToList();
        }


        public async Task<TeacherViewDTO> GetByIdAsync(string id)
        {
            var t = await _repository.GetByIdAsync(id);
            if (t == null) return null;

            return new TeacherViewDTO
            {
                Id = t.Id,
                UserName = t.User?.UserName,
                UserEmail = t.User?.UserEmail,
                AcademicTerms = t.TeacherTerms.Select(tt => tt.AcademicTerm?.Name).Distinct().ToList(),
                LevelsGradesSubjects = t.TeacherSubjects
                    .GroupBy(s => s.Grade.Level)
                    .Select(lg => new LevelWithGradesDto
                    {
                        Id = lg.Key?.Id,
                        Name = lg.Key?.Name,
                        Grades = lg.GroupBy(g => g.Grade)
                            .Select(g => new GradeWithSubjectsDto
                            {
                                Id = g.Key?.Id,
                                Name = g.Key?.Name,
                                Subjects = g.Select(sub => new SubjectMiniDto
                                {
                                    Id = sub.Subject?.Id,
                                    Name = sub.Subject?.Name
                                }).ToList()
                            }).ToList()
                    }).ToList()
            };
        }

        public async Task<ResponseDTO> AddBasicAsync(CreateTeacherDTO dto, string creatorId)
        {
            var register = new RegisterDTO
            {
                UserName = dto.UserName,
                Email = dto.UserEmail,
                Password = dto.Password,
                Role = UserRole.Teacher.ToString()
            };

            var (response, userId) = await _authService.RegisterAsync(register,creatorId);
            if (!response.IsValid)
                return response;

            var teacher = new Teacher
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                CreatedUserId = creatorId
            };

            await _repository.AddAsync(teacher);
            return new ResponseDTO { IsValid = true, Message = "Teacher created successfully",Data = userId };
        }

        public async Task AddDetailsAsync(UpdateTeacherDetailsDTO dto, string updaterId)
        {
            var teacher = await _repository.GetByIdAsync(dto.TeacherId);
            if (teacher == null) return;

            var subjects = dto.SubjectIds.Select(subjectId => new TeacherGradeSubject
            {
                Id = Guid.NewGuid().ToString(),
                TeacherId = dto.TeacherId,
                SubjectId = subjectId
            });

            await _repository.AddSubjectsAsync(subjects);

            var terms = dto.AcademicTermIds.Select(termId => new TeacherTerm
            {
                Id = Guid.NewGuid().ToString(),
                TeacherId = dto.TeacherId,
                AcademicTermId = termId
            });

            await _repository.AddTermsAsync(terms);
        }
       
        public async Task DeleteAsync(string id)
        {
            await _repository.SoftDeleteAsync(id);
        }
    }
}
