using FosoolSchool.Models.TeacherEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models
{
    public class Class : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("Grade")]
        public string GradeId { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual List<Student> Students { get; set; } = new();
    }

}
