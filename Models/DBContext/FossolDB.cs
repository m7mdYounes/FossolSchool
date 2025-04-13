using FosoolSchool.Models.TeacherEntities;
using FosoolSchool.Models.Lesson;
using Microsoft.EntityFrameworkCore;
using static FosoolSchool.Consts.Enums;

namespace FosoolSchool.Models.DBContext
{
    public class FossolDB : DbContext
    {
        public FossolDB()
        {
            
        }
        public FossolDB(DbContextOptions<FossolDB> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<AcademicTerm> AcademicTerms { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Models.Lesson.Lesson> Lessons { get; set; }
        public virtual DbSet<TeacherTerm> TeacherTerms { get; set; }
        public virtual DbSet<TeacherGradeSubject> TeacherSubjects { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<LessonResource> LessonResources { get; set; }
        public virtual DbSet<TeacherLessonResourceView> TeacherLessonResourceViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminId = Guid.NewGuid().ToString();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "admin",
                UserEmail = "admin@example.com",
                Password = "AQAAALDzk2IeAfQWn6Qn8lyhN4aRyfBrw5v34ev473NIjBBwkwTf+FuzYbuJMje5nemQcw==", //123456string
                UserRole = UserRole.SuperAdmin,
                CreatedAt = DateTime.UtcNow,
                CreatedUserId = "null",
                UpdatedAt = DateTime.UtcNow,
                UpdatedUserId = "null"
            });
        }
    }

}
