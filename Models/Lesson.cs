using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models
{
    public class Lesson : BaseEntity
    {
        public string Title { get; set; }

        [ForeignKey("Subject")]
        public string SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }

}
