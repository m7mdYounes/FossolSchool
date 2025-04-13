using FosoolSchool.DTO.Lesson;
using FosoolSchool.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FosoolSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin,Teacher")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILessonResourceService _lessonResourceService;

        public LessonController(ILessonService service, IHttpContextAccessor httpContextAccessor, ILessonResourceService lessonResourceService)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _lessonResourceService = lessonResourceService;
        }

        private string GetUserIdFromToken()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpPost("get-all-lessons")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Lessons retrieved successfully" });
        }

        [HttpPost("get-lesson-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ResponseDTO { IsValid = false, Error = "Lesson not found" });

            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Lesson retrieved successfully" });
        }

        [HttpPost("get-lessons-by-subject/{subjectId}")]
        public async Task<IActionResult> GetBySubjectId([FromRoute] string subjectId)
        {
            var result = await _service.GetBySubjectIdAsync(subjectId);
            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Lessons by subject retrieved successfully" });
        }

        [HttpPost("create-lesson")]
        public async Task<IActionResult> Create([FromBody] CreateLessonDTO dto)
        {
            var userId = GetUserIdFromToken();
            await _service.AddAsync(dto, userId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Lesson created successfully" });
        }

        [HttpPost("update-lesson/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateGetLessonDTO dto)
        {
            var userId = GetUserIdFromToken();
            await _service.UpdateAsync(id, dto, userId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Lesson updated successfully" });
        }

        [HttpPost("delete-lesson/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ResponseDTO { IsValid = true, Message = "Lesson deleted successfully" });
        }

        [HttpPost("upload-resource")]
        public async Task<IActionResult> Upload([FromForm] CreateLessonResourceDTO dto)
        {
            var result = await _lessonResourceService.AddResourceAsync(dto, GetUserIdFromToken());
            return Ok(result);
        }

        [HttpPost("lesson/{lessonId}")]
        public async Task<IActionResult> GetResources([FromRoute]string lessonId)
        {
            var result = await _lessonResourceService.GetResourcesForTeacherAsync(lessonId, GetUserIdFromToken());
            return Ok(result);
        }

        [HttpPost("hide-resource/{Id}")]
        public async Task<IActionResult> HideResource([FromRoute] string Id)
        {
            var result = await _lessonResourceService.HideResourceAsync(GetUserIdFromToken(), Id);
            return Ok(result);
        }
    }
}
