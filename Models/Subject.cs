using FosoolSchool.Models.TeacherEntities;
using FosoolSchool.Models.Lesson;
using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("Grade")]
        public string GradeId { get; set; }
        public virtual Grade Grade { get; set; }

        public virtual List<Models.Lesson.Lesson> Lessons { get; set; } = new();
        public virtual List<TeacherGradeSubject> TeacherSubjects { get; set; } = new();
    }

}
