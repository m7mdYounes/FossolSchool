using FosoolSchool.DTO;
using FosoolSchool.DTO.Lesson;
using FosoolSchool.Models.Lesson;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services.Interfaces;

namespace FosoolSchool.Services
{
    public class LessonResourceService : ILessonResourceService
    {
        private readonly ILessonResourceRepo _repo;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public LessonResourceService(ILessonResourceRepo repo, IWebHostEnvironment env, IConfiguration config)
        {
            _repo = repo;
            _env = env;
            _config = config;
        }

        public async Task<ResponseDTO> GetResourcesForTeacherAsync(string lessonId, string teacherId)
        {
            try
            {
                var all = await _repo.GetByLessonIdAsync(lessonId);

                var teacherResources = all.Where(r => r.UploadedById == teacherId).ToList();

                var hiddenIds = await _repo.GetHiddenResourceIdsAsync(teacherId,lessonId);

                var adminResources = all
                    .Where(r =>
                        r.UploadedBy.UserRole.ToString() == "Admin" &&
                        !hiddenIds.Contains(r.Id))
                    .ToList();

                var result = teacherResources
                    .Concat(adminResources)
                    .Select(r => new UpdateGetLessonResourceDTO
                    {
                        Id = r.Id,
                        FileName = r.FileName,
                        FilePath = r.FilePath,
                        FileType = r.FileType,
                        UploadedById = r.UploadedById,
                        LessonId = r.LessonId
                    }).ToList();

                return new ResponseDTO
                {
                    IsValid = true,
                    Data = result,
                    Message = "Resources retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsValid = false,
                    Error = ex.Message,
                    Message = "Failed to retrieve lesson resources"
                };
            }
        }


        public async Task<ResponseDTO> AddResourceAsync(CreateLessonResourceDTO dto, string userId)
        {
            try
            {
                string storageRoot = _config["StoragePath"] ?? "wwwroot/uploads";
                string fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                string fullPath = Path.Combine(storageRoot, fileName);

                string relativePath = Path.Combine("uploads", fileName).Replace("\\", "/");

                Directory.CreateDirectory(storageRoot);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(stream);
                }

                var resource = new LessonResource
                {
                    FileName = fileName,
                    FilePath = relativePath,
                    FileType = Path.GetExtension(dto.File.FileName),
                    LessonId = dto.LessonId,
                    UploadedById = userId,
                    CreatedAt =DateTime.UtcNow,
                    CreatedUserId = userId,
                };

                await _repo.AddAsync(resource);

                return new ResponseDTO
                {
                    IsValid = true,
                    Message = "Resource uploaded successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsValid = false,
                    Error = ex.Message,
                    Message = "Failed to upload resource"
                };
            }
        }

        public async Task<ResponseDTO> HideResourceAsync(string teacherId, string resourceId)
        {
            try
            {
                await _repo.HideResourceForTeacherAsync(teacherId, resourceId);
                return new ResponseDTO
                {
                    IsValid = true,
                    Message = "Resource visibility updated"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsValid = false,
                    Error = ex.Message,
                    Message = "Failed to hide resource"
                };
            }
        }
    }
}
