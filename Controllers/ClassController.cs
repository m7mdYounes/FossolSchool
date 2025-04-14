using FosoolSchool.DTO.Class;
using FosoolSchool.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FosoolSchool.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin,Teacher")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClassController(IClassService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserIdFromToken()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ResponseDTO { IsValid = true, Data = result });
        }

        [HttpPost("get-students-by-class-id/{id}")]
        public async Task<IActionResult> GetStudentsByClassId(string classid)
        {
            var result = await _service.GetStudentByClassIdAsync(classid);
            if (result == null)
                return NotFound(new ResponseDTO { IsValid = false, Error = "Class not found" });
            return Ok(new ResponseDTO { IsValid = true, Data = result });
        }

        [HttpPost("get-by-id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ResponseDTO { IsValid = false, Error = "Class not found" });
            return Ok(new ResponseDTO { IsValid = true, Data = result });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateClassDTO dto)
        {
            var teacherId = GetUserIdFromToken();
            await _service.AddAsync(dto, teacherId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Class created successfully" });
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateGetClassDTO dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok(new ResponseDTO { IsValid = true, Message = "Class updated successfully" });
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ResponseDTO { IsValid = true, Message = "Class deleted successfully" });
        }

        [HttpPost("assign-students")]
        public async Task<IActionResult> AssignStudentsToClass([FromBody] AssignStudentsToClassDTO dto)
        {
            await _service.AssignStudentsToClassAsync(dto.ClassId, dto.StudentIds);
            return Ok(new ResponseDTO { IsValid = true, Message = "Students assigned to class successfully" });
        }
    }
}
