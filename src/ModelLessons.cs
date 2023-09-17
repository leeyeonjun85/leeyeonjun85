using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorServerSignalR.Models
{
    [Table("LESSONS")]
    public class ModelLessons
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Column("DATE_TIME_START")]
        public DateTime dateTimeStart { get; set; } = DateTime.Now;

        [Column("DATE_TIME_END")]
        public DateTime dateTimeEnd { get; set; } = DateTime.Now;

        [Column("LESSON_TOPIC")]
        public string? lessonTopic { get; set; }

        [Column("ASSIGNMENT")]
        public string? assignment { get; set; }

        [Column("LESSON_MEMO")]
        public string? lessonMemo { get; set; }

        [Column("MEMBER_ID")]
        [Required]
        public int memberId { get; set; }

        [ForeignKey("memberId")]
        public virtual ModelMembers? member { get; set; }
    }
}
