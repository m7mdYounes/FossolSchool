using FosoolSchool.DTO.Class;
using FosoolSchool.DTO.Student;
using FosoolSchool.Models;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services.Interfaces;

namespace FosoolSchool.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepo _repo;

        public ClassService(IClassRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<UpdateGetClassDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var result = new List<UpdateGetClassDTO>();

            foreach (var c in list)
            {
                var students = await _repo.GetStudentsByClassIdAsync(c.Id);

                var dto = new UpdateGetClassDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    GradeId = c.GradeId,
                    GradeName = c.Grade?.Name,
                    LevelId = c.Grade?.LevelId,
                    LevelName = c.Grade?.Level?.Name,
                    studentDTOs = students.Select(s => new UpdateGetStudentDTO
                    {
                        Id = s.Id,
                        UserName = s.User?.UserName,
                    }).ToList()
                };

                result.Add(dto);
            }

            return result;
        }


        public async Task<UpdateGetClassDTO> GetByIdAsync(string id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;

            var students = await _repo.GetStudentsByClassIdAsync(id);

            return new UpdateGetClassDTO
            {
                Id = c.Id,
                Name = c.Name,
                GradeId = c.GradeId,
                GradeName = c.Grade?.Name,
                LevelId = c.Grade?.LevelId,
                LevelName = c.Grade?.Level?.Name,
                studentDTOs = students.Select(s => new UpdateGetStudentDTO
                {
                    Id = s.Id,
                    UserName = s.User?.UserName,  
                }).ToList()
            };
        }

        //public async Task GetStudentbyclassid(string classid)
        //{
        //    var students = await _repo.GetStudentsByClassIdAsync(classid);
        //    var result = new List<UpdateGetStudentDTO>();
        //    foreach(var s in students)
        //    {
        //        var student = new UpdateGetStudentDTO()
        //        {
        //            Id = s.Id,
        //            UserName = s.User?.UserName,

        //        };
        //        result.Add(student);
        //    }
        //    await result;
        //}

        public async Task AssignStudentsToClassAsync(string classId, List<string> studentIds)
        {
            await _repo.AssignStudentsToClassAsync(classId, studentIds);
        }
        public async Task AddAsync(CreateClassDTO dto, string teacherId)
        {
            var entity = new Class
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                GradeId = dto.GradeId,
                TeacherId = teacherId,
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(string id, UpdateGetClassDTO dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return;

            entity.Name = dto.Name;
            entity.GradeId = dto.GradeId;
            entity.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repo.SoftDeleteAsync(id);
        }
    }
}
