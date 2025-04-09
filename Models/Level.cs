using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models
{
    public class Level : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("AcademicTerm")]
        public string AcademicTermId { get; set; }
        public virtual AcademicTerm AcademicTerm { get; set; }
        public virtual List<Grade> Grades { get; set; } = new();
    }

}
