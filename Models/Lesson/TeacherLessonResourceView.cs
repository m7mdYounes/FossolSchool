using FosoolSchool.Models.TeacherEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models.Lesson
{
    public class TeacherLessonResourceView : BaseEntity
    {
        public bool IsVisible { get; set; } = true;

        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Resource")]
        public string ResourceId { get; set; }
        public virtual LessonResource Resource { get; set; }
    }
}
