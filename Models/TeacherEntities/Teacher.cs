using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models.TeacherEntities
{
    public class Teacher : BaseEntity
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Student> Students { get; set; } = new();
        public virtual List<TeacherTerm> TeacherTerms { get; set; } = new();
    }

}
