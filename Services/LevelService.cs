using FosoolSchool.DTO.Level;
using FosoolSchool.Models;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services.Interfaces;

namespace FosoolSchool.Services
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepo _repository;

        public LevelService(ILevelRepo repository)
        {
            _repository = repository;
        }

        public async Task<List<UpdateGetLevelDTO>> GetAllAsync()
        {
            var levels = await _repository.GetAllAsync();
            return levels.Select(l => new UpdateGetLevelDTO { Id = l.Id, Name = l.Name }).ToList();
        }

        public async Task<UpdateGetLevelDTO> GetByIdAsync(string id)
        {
            var l = await _repository.GetByIdAsync(id);
            return l == null ? null : new UpdateGetLevelDTO { Id = l.Id, Name = l.Name };
        }

        public async Task AddAsync(CreateLevelDTO dto, string userId)
        {
            var entity = new Level
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedUserId = userId
            };
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(string id, UpdateGetLevelDTO dto, string userId)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;
            entity.Name = dto.Name;
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
