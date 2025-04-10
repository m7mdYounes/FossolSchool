using FosoolSchool.DTO.AcademicTerm;
using FosoolSchool.Models;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services.Interfaces;
using System.Globalization;

namespace FosoolSchool.Services
{
    public class AcademicTermService : IAcademicTermService
    {
        private readonly IAcedemicTermRepo _repository;

        public AcademicTermService(IAcedemicTermRepo repository)
        {
            _repository = repository;
        }

        public async Task<List<UpdateGetAcademicTermDTO>> GetAllAsync()
        {
            var terms = await _repository.GetAllAsync();
            return terms.Select(t => new UpdateGetAcademicTermDTO
            {
                Id = t.Id,
                Name = t.Name,
                StartDate = t.StartDate.ToString("dd/MM/yyyy"),
                EndDate = t.EndDate.ToString("dd/MM/yyyy")
            }).ToList();
        }

        public async Task<UpdateGetAcademicTermDTO> GetByIdAsync(string id)
        {
            var t = await _repository.GetByIdAsync(id);
            if (t == null) return null;
            return new UpdateGetAcademicTermDTO { Id = t.Id, Name = t.Name, StartDate = t.StartDate.ToString("dd/MM/yyyy"), EndDate = t.EndDate.ToString("dd/MM/yyyy") };
        }

        public async Task AddAsync(CreateAcademicTermDTO dto, string UserId)
        {
            var entity = new AcademicTerm
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                StartDate = DateTime.ParseExact(dto.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(dto.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                CreatedAt = DateTime.UtcNow,
                CreatedUserId = UserId
            };
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(string id, UpdateGetAcademicTermDTO dto, string UserId)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;
            entity.Name = dto.Name;
            entity.StartDate = DateTime.ParseExact(dto.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            entity.EndDate = DateTime.ParseExact(dto.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedUserId = UserId;
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.SoftDeleteAsync(id);
        }
    }
}
