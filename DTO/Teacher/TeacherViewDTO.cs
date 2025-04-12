namespace FosoolSchool.DTO.Teacher
{
    public class TeacherViewDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<string> AcademicTerms { get; set; }
        public List<LevelWithGradesDto> LevelsGradesSubjects { get; set; }
    }
    public class SubjectMiniDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class GradeWithSubjectsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SubjectMiniDto> Subjects { get; set; }
    }
    public class LevelWithGradesDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<GradeWithSubjectsDto> Grades { get; set; }
    }
}
