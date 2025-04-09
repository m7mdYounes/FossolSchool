namespace FosoolSchool.DTO
{
    public class ResponseDTO
    {
        public bool IsValid { get; set; }
        public Object Data { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
