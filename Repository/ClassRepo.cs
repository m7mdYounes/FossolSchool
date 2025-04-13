using FosoolSchool.Models;
using FosoolSchool.Models.DBContext;
using FosoolSchool.Models.TeacherEntities;
using FosoolSchool.Repository.Interfaces;

namespace FosoolSchool.Repository
{
    public class ClassRepo : IClassRepo
    {
        private readonly FossolDB _context;

        public ClassRepo(FossolDB context)
        {
            _context = context;
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await _context.Set<Class>()
                .Include(c => c.Grade).ThenInclude(g => g.Level)
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Class> GetByIdAsync(string id)
        {
            return await _context.Set<Class>()
                .Include(c => c.Grade).ThenInclude(g => g.Level)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<List<Student>> GetStudentsByClassIdAsync(string classId)
        {
            return await _context.Set<Student>()
                .Include(s => s.User)
                .Where(s => s.ClassId == classId && !s.IsDeleted && !s.User.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(Class entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AssignStudentsToClassAsync(string classId, List<string> studentIds)
        {
            var students = await _context.Set<Student>().Where(s => studentIds.Contains(s.Id)).ToListAsync();
            foreach (var student in students)
            {
                student.ClassId = classId;
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Class entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var cls = await GetByIdAsync(id);
            if (cls != null)
            {
                cls.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
