using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models.TeacherEntities
{
    public class Teacher : BaseEntity
    {
        public virtual List<Student> Students { get; set; } = new();
        public virtual List<TeacherTerm> TeacherTerms { get; set; } = new();
        public virtual List<TeacherGradeSubject> TeacherSubjects {  get; set; } = new();
    }

}
