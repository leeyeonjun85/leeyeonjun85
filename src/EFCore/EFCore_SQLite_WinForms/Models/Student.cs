using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_SQLite_WinForms.Models
{
    [Table("STUDENT")]
    public class Student
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Column("NAME")]
        [MaxLength(20)]
        [Required]
        public string name { get; set; } = string.Empty;

        [Column("SCHOLL_ID")]
        public int schoolId { get; set; }

        [ForeignKey("schoolId")]
        public virtual School? school { get; set; }
    }
}
