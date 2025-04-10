namespace FosoolSchool.DTO.Student
{
    public class UpdateGetStudentDTO
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string ClassId { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
    }
}
