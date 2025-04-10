using FosoolSchool.DTO.Subject;
using FosoolSchool.Models;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services.Interfaces;

namespace FosoolSchool.Services
{
    public class SubjectService : ISubjectService
    {
        
        private readonly ISubjectRepo _repository;

        public SubjectService(ISubjectRepo repository)
        {
            _repository = repository;
        }

        public async Task<List<UpdateGetSubjectDTO>> GetAllAsync()
        {
            var subjects = await _repository.GetAllAsync();
            return subjects.Select(s => new UpdateGetSubjectDTO
            {
                Id = s.Id,
                Name = s.Name,
                GradeId = s.GradeId,
                GradeName = s.Grade?.Name,
                LevelName = s.Grade?.Level?.Name
            }).ToList();
        }

        public async Task<UpdateGetSubjectDTO> GetByIdAsync(string id)
        {
            var s = await _repository.GetByIdAsync(id);
            return s == null ? null : new UpdateGetSubjectDTO
            {
                Id = s.Id,
                Name = s.Name,
                GradeId = s.GradeId,
                GradeName = s.Grade?.Name,
                LevelName = s.Grade?.Level?.Name
            };
        }
        public async Task<List<UpdateGetSubjectDTO>> GetByGradeIdAsync(string gradeId)
        {
            var subjects = await _repository.GetByGradeIdAsync(gradeId);
            return subjects.Select(s => new UpdateGetSubjectDTO
            {
                Id = s.Id,
                Name = s.Name,
                GradeId = s.GradeId,
                GradeName = s.Grade?.Name,
                LevelName = s.Grade?.Level?.Name
            }).ToList();
        }

        public async Task AddAsync(CreateSubjectDTO dto, string userId)
        {
            var entity = new Subject
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                GradeId = dto.GradeId,
                CreatedAt = DateTime.UtcNow,
                CreatedUserId = userId
            };
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(string id, UpdateGetSubjectDTO dto, string userId)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;
            entity.Name = dto.Name;
            entity.GradeId = dto.GradeId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedUserId = userId;
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.SoftDeleteAsync(id);
        }
    }
}
