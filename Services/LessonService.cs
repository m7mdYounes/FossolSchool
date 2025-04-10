using FosoolSchool.DTO.Lesson;
using FosoolSchool.Models;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services.Interfaces;

namespace FosoolSchool.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepo _repository;

        public LessonService(ILessonRepo repository)
        {
            _repository = repository;
        }

        public async Task<List<UpdateGetLessonDTO>> GetAllAsync()
        {
            var lessons = await _repository.GetAllAsync();
            return lessons.Select(l => new UpdateGetLessonDTO
            {
                Id = l.Id,
                Title = l.Title,
                SubjectId = l.SubjectId,
                SubjectName = l.Subject?.Name,
                GradeName = l.Subject?.Grade?.Name,
                LevelName = l.Subject?.Grade?.Level?.Name
            }).ToList();
        }

        public async Task<UpdateGetLessonDTO> GetByIdAsync(string id)
        {
            var l = await _repository.GetByIdAsync(id);
            return l == null ? null : new UpdateGetLessonDTO
            {
                Id = l.Id,
                Title = l.Title,
                SubjectId = l.SubjectId,
                SubjectName = l.Subject?.Name,
                GradeName = l.Subject?.Grade?.Name,
                LevelName = l.Subject?.Grade?.Level?.Name
            };
        }

        public async Task<List<UpdateGetLessonDTO>> GetBySubjectIdAsync(string subjectId)
        {
            var lessons = await _repository.GetBySubjectIdAsync(subjectId);
            return lessons.Select(l => new UpdateGetLessonDTO
            {
                Id = l.Id,
                Title = l.Title,
                SubjectId = l.SubjectId,
                SubjectName = l.Subject?.Name,
                GradeName = l.Subject?.Grade?.Name,
                LevelName = l.Subject?.Grade?.Level?.Name
            }).ToList();
        }

        public async Task AddAsync(CreateLessonDTO dto, string userId)
        {
            var entity = new Lesson
            {
                Id = Guid.NewGuid().ToString(),
                Title = dto.Title,
                SubjectId = dto.SubjectId,
                CreatedAt = DateTime.UtcNow,
                CreatedUserId = userId
            };
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(string id, UpdateGetLessonDTO dto, string userId)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;
            entity.Title = dto.Title;
            entity.SubjectId = dto.SubjectId;
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
