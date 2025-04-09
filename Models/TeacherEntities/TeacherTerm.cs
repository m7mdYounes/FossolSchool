using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models.TeacherEntities
{
    public class TeacherTerm : BaseEntity
    {
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("AcademicTerm")]
        public string AcademicTermId { get; set; }
        public virtual AcademicTerm AcademicTerm { get; set; }
        public virtual List<TeacherGrade> TeacherGrades { get; set; } = new();
        public virtual List<TeacherSubject> TeacherSubjects { get; set; } = new();
    }

}
