using FosoolSchool.Models.TeacherEntities;

namespace FosoolSchool.Models
{
    public class AcademicTerm : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Level> Levels { get; set; } = new();
        public virtual List<TeacherTerm> TeacherTerms { get; set; } = new();
    }

}
