using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models.TeacherEntities
{
    public class TeacherGrade : BaseEntity
    {
        [ForeignKey("TeacherTerm")]
        public string TeacherTermId { get; set; }
        public virtual TeacherTerm TeacherTerm { get; set; }

        [ForeignKey("Grade")]
        public string GradeId { get; set; }
        public virtual Grade Grade { get; set; }
    }

}
