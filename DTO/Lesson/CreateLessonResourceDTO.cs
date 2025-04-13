namespace FosoolSchool.DTO.Lesson
{
    public class CreateLessonResourceDTO
    {
        public ICollection<IFormFile> Files { get; set; }
        public string LessonId { get; set; }
    }
}
