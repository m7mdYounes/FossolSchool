namespace FosoolSchool.DTO.Lesson
{
    public class UpdateGetLessonDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string GradeName { get; set; } = string.Empty;
        public string LevelName { get; set; } = string.Empty;
    }
}
