using FosoolSchool.DTO.Subject;
using FosoolSchool.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FosoolSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubjectController(ISubjectService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserIdFromToken()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        [Authorize(Roles = "SuperAdmin,Teacher")]
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Subjects retrieved successfully" });
        }
        [Authorize(Roles = "SuperAdmin,Teacher")]
        [HttpPost("get-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ResponseDTO { IsValid = false, Error = "Subject not found" });

            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Subject retrieved successfully" });
        }
        [Authorize(Roles = "SuperAdmin,Teacher")]
        [HttpPost("get-by-grade/{gradeId}")]
        public async Task<IActionResult> GetByGradeId([FromRoute] string gradeId)
        {
            var result = await _service.GetByGradeIdAsync(gradeId);
            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Subjects by grade retrieved successfully" });
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateSubjectDTO dto)
        {
            var userId = GetUserIdFromToken();
            await _service.AddAsync(dto, userId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Subject created successfully" });
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateGetSubjectDTO dto)
        {
            var userId = GetUserIdFromToken();
            await _service.UpdateAsync(id, dto, userId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Subject updated successfully" });
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ResponseDTO { IsValid = true, Message = "Subject deleted successfully" });
        }
    }
}
