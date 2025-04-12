using FosoolSchool.DTO.Teacher;

namespace FosoolSchool.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherViewDTO>> GetAllAsync();
        Task<TeacherViewDTO> GetByIdAsync(string id);
        Task<ResponseDTO> AddBasicAsync(CreateTeacherDTO dto, string creatorId);
        Task AddDetailsAsync(UpdateTeacherDetailsDTO dto, string updaterId);
        Task DeleteAsync(string id);
    }
}
