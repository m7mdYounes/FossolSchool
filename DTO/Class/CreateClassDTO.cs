namespace FosoolSchool.DTO.Class
{
    public class CreateClassDTO
    {
        public string Name { get; set; }
        public string GradeId { get; set; }
        public string SubjectId { get; set; }
    }

    public class AssignStudentsToClassDTO
    {
        public string ClassId { get; set; }
        public List<string> StudentIds { get; set; }
    }
}
