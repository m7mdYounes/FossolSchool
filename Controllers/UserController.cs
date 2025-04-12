using FosoolSchool.DTO.Student;
using FosoolSchool.DTO.Teacher;
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
        private readonly ITeacherService _teacherService;

        public UserController(IStudentService service, IHttpContextAccessor httpContextAccessor,ITeacherService teacherService)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _teacherService = teacherService;
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

        [HttpPost("get-teacher-all")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ResponseDTO { IsValid = true, Data = result });
        }

        [HttpPost("get-teacher-by-id/{id}")]
        [Authorize(Roles = "SuperAdmin,Teacher")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ResponseDTO { IsValid = false, Error = "Teacher not found" });
            return Ok(new ResponseDTO { IsValid = true, Data = result });
        }

        [HttpPost("create-teacher-basic")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateBasic([FromBody] CreateTeacherDTO dto)
        {
            var userId = GetUserIdFromToken();
            var result = await _teacherService.AddBasicAsync(dto, userId);
            return Ok(result);
        }

        [HttpPost("add-teacher-details")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddDetails([FromBody] UpdateTeacherDetailsDTO dto)
        {
            var userId = GetUserIdFromToken();
            await _teacherService.AddDetailsAsync(dto, userId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Teacher details saved" });
        }

        [HttpPost("delete-teacher/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ResponseDTO { IsValid = true, Message = "Teacher deleted successfully" });
        }
    }
}
