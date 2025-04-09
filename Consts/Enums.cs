using System.ComponentModel.DataAnnotations;

namespace FosoolSchool.Consts
{
    public class Enums
    {
        public enum UserRole
        {
            [Display(Name = "Super Admin")]
            SuperAdmin,
            [Display(Name = "Teacher")]
            Teacher,
            [Display(Name = "Student")]
            Student
        }

    }
}
