using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oracle_EFCore.Models
{
    [Table("STUDENT")]
    public class Student
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [Column("birthday")]
        [Required]
        public DateTime Birthday { get; set; } = DateTime.UtcNow;

        [Column("room_id")]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room? Room { get; set; }
    }
}
