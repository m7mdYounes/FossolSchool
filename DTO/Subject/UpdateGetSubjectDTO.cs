namespace FosoolSchool.DTO.Subject
{
    public class UpdateGetSubjectDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string GradeId { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public string LevelName { get; set; } = string.Empty;
    }
}
