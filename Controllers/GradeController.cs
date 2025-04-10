using FosoolSchool.DTO.Grade;
using FosoolSchool.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FosoolSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GradeController(IGradeService service, IHttpContextAccessor httpContextAccessor)
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
            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Grades retrieved successfully" });
        }

        [HttpPost("get-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ResponseDTO { IsValid = false, Error = "Grade not found" });

            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Grade retrieved successfully" });
        }

        [HttpPost("get-by-level/{levelId}")]
        public async Task<IActionResult> GetByLevelId([FromRoute] string levelId)
        {
            var result = await _service.GetByLevelIdAsync(levelId);
            return Ok(new ResponseDTO { IsValid = true, Data = result, Message = "Grades for level retrieved successfully" });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateGradeDTO dto)
        {
            var userId = GetUserIdFromToken();
            await _service.AddAsync(dto, userId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Grade created successfully" });
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateGetGradeDTO dto)
        {
            var userId = GetUserIdFromToken();
            await _service.UpdateAsync(id, dto, userId);
            return Ok(new ResponseDTO { IsValid = true, Message = "Grade updated successfully" });
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ResponseDTO { IsValid = true, Message = "Grade deleted successfully" });
        }
    }
}
