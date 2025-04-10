using FosoolSchool.DTO.Grade;
using FosoolSchool.Models;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services.Interfaces;

namespace FosoolSchool.Services
{
    public class GradeService:IGradeService
    {
        private readonly IGradeRepo _repository;

        public GradeService(IGradeRepo repository)
        {
            _repository = repository;
        }

        public async Task<List<UpdateGetGradeDTO>> GetAllAsync()
        {
            var grades = await _repository.GetAllAsync();
            return grades.Select(g => new UpdateGetGradeDTO
            {
                Id = g.Id,
                Name = g.Name,
                LevelId = g.LevelId,
                LevelName = g.Level?.Name
            }).ToList();
        }

        public async Task<UpdateGetGradeDTO> GetByIdAsync(string id)
        {
            var g = await _repository.GetByIdAsync(id);
            return g == null ? null : new UpdateGetGradeDTO
            {
                Id = g.Id,
                Name = g.Name,
                LevelId = g.LevelId,
                LevelName = g.Level?.Name
            };
        }
        public async Task<List<UpdateGetGradeDTO>> GetByLevelIdAsync(string levelId)
        {
            var grades = await _repository.GetByLevelIdAsync(levelId);
            return grades.Select(g => new UpdateGetGradeDTO
            {
                Id = g.Id,
                Name = g.Name,
                LevelId = g.LevelId,
                LevelName = g.Level?.Name
            }).ToList();
        }
        public async Task AddAsync(CreateGradeDTO dto, string userId)
        {
            var entity = new Grade
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                LevelId = dto.LevelId,
                CreatedAt = DateTime.UtcNow,
                CreatedUserId = userId
            };
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(string id, UpdateGetGradeDTO dto, string userId)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;
            entity.Name = dto.Name;
            entity.LevelId = dto.LevelId;
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
