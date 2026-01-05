using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_SQLite_WinForms.Models
{
    [Table("SCHOOL")]
    public class School
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Column("NAME", TypeName = "VARCHAR")]
        [Required]
        [MaxLength(20)]
        public string name { get; set; } = string.Empty;

        public virtual ICollection<Student>? students { get; set; }
    }
}
