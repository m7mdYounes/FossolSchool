using System.ComponentModel.DataAnnotations.Schema;

namespace FosoolSchool.Models
{
    //Primary , middle , high
    public class Level : BaseEntity
    {
        public string Name { get; set; }

        public virtual List<Grade> Grades { get; set; } = new();
    }

}
