using FosoolSchool.DTO.AcademicTerm;

namespace FosoolSchool.Services.Interfaces
{
    public interface IAcademicTermService
    {
        Task<List<UpdateGetAcademicTermDTO>> GetAllAsync();
        Task<UpdateGetAcademicTermDTO> GetByIdAsync(string id);
        Task AddAsync(CreateAcademicTermDTO dto,string UserId);
        Task UpdateAsync(string AcademicTermid, UpdateGetAcademicTermDTO dto, string UserId);
        Task DeleteAsync(string AcademicTermid);
    }
}
