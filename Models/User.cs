using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static FosoolSchool.Consts.Enums;

namespace FosoolSchool.Models
{
    public class User:BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
    }
}
