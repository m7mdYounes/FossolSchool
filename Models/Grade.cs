using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FosoolSchool.Models.TeacherEntities;

namespace FosoolSchool.Models
{
    public class Grade : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("Level")]
        public string LevelId { get; set; }
        public virtual Level Level { get; set; }

        public virtual List<Subject> Subjects { get; set; } = new();
        public virtual List<TeacherSubject> TeacherSubjects { get; set; } = new();
    }

}
