namespace FosoolSchool.DTO.Teacher
{
    public class UpdateTeacherDetailsDTO
    {
        public string TeacherId { get; set; }
        public List<string> GradeIds { get; set; } = new List<string>();
        public List<string> SubjectIds { get; set; } = new List<string>();
        public List<string> AcademicTermIds { get; set; } = new List<string>();
    }
}
