namespace FosoolSchool.DTO.Student
{
    public class CreateStudentDTO
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string TeacherId { get; set; }
        public string ClassId { get; set; }  = string.Empty;
    }
}
