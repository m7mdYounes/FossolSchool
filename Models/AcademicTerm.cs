using FosoolSchool.Models.TeacherEntities;

namespace FosoolSchool.Models
{
    public class AcademicTerm : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<TeacherTerm> TeacherTerms { get; set; } = new();
    }

}
