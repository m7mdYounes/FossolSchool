using FosoolSchool.DTO.Student;

namespace FosoolSchool.DTO.Class
{
    public class UpdateGetClassDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string GradeId { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public string LevelId { get; set; }
        public string LevelName { get; set; } = string.Empty;
        public List<UpdateGetStudentDTO> studentDTOs { get; set; } = new List<UpdateGetStudentDTO>();
    }


}
