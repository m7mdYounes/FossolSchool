using FosoolSchool.Models.DBContext;
using FosoolSchool.Models.TeacherEntities;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly FossolDB _context;

        public StudentRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Set<Student>()
                .Include(s => s.User)
                .Include(s => s.Teacher)
                .Include(s => s.Class)
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            return await _context.Set<Student>()
                .Include(s => s.User)
                .Include(s => s.Teacher)
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<List<Student>> GetByTeacherIdAsync(string teacherId)
        {
            return await _context.Set<Student>()
                .Include(s => s.User)
                .Include(s => s.Teacher)
                .Include(s => s.Class)
                .Where(s => s.TeacherId == teacherId && !s.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(Student student)
        {
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var student = await GetByIdAsync(id);
            if (student != null)
            {
                student.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
