using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models.Lesson
{
    public class LessonResource : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }

        [ForeignKey("Lesson")]
        public string LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("UploadedBy")]
        public string UploadedById { get; set; }
        public virtual User UploadedBy { get; set; }
    }
}
