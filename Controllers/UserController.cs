using FosoolSchool.DTO.Student;
using FosoolSchool.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FosoolSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IStudentService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserIdFromToken()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        [Authorize(Roles = "SuperAdmin,Teacher")]
        [HttpPost("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Students retrieved successfully" });
        }

        [Authorize(Roles = "SuperAdmin,Teacher")]
        [HttpPost("get-by-id-student/{id}")]
        public async Task<IActionResult> GetByIdStudent([FromRoute] string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ResponseDTO { IsValid = false, Error = "Student not found" });

            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Student retrieved successfully" });
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("create-student")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDTO dto)
        {
            var userId = GetUserIdFromToken();
            await _service.AddAsync(dto, userId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Student created successfully" });
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("update-student/{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] string id, [FromBody] UpdateGetStudentDTO dto)
        {
            var userId = GetUserIdFromToken();
            await _service.UpdateAsync(id, dto, userId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Student updated successfully" });
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] string id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ResponseDTO { IsValid = true, Message = "Student deleted successfully" });
        }

        [HttpPost("get-students-by-teacher")]
        [Authorize(Roles = "SuperAdmin,Teacher")]
        public async Task<IActionResult> GetStudentsByTeacherId()
        {
            var teacherId = GetUserIdFromToken();
            var result = await _service.GetByTeacherIdAsync(teacherId);
            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Students by teacher retrieved successfully" });
        }
    }
}
