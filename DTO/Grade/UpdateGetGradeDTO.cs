namespace FosoolSchool.DTO.Grade
{
    public class UpdateGetGradeDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string LevelId { get; set; }
        public string LevelName { get; set; } = string.Empty;
    }
}
