using System.ComponentModel.DataAnnotations;

namespace FosoolSchool.Models
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
