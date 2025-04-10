namespace FosoolSchool.DTO.AcademicTerm
{
    public class UpdateGetAcademicTermDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string StartDate { get; set; } // dd/mm/yyyy
        public string EndDate { get; set; } // dd/mm/yyyy
    }
}

