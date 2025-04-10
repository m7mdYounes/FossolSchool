using System.ComponentModel.DataAnnotations;

namespace FosoolSchool.Models
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
