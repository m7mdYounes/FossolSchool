using FosoolSchool.Models.TeacherEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("Grade")]
        public string GradeId { get; set; }
        public virtual Grade Grade { get; set; }

        public virtual List<Lesson> Lessons { get; set; } = new();
        public virtual List<TeacherSubject> TeacherSubjects { get; set; } = new();
    }

}
