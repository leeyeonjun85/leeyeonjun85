using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace OoManager.Common.Models
{
    //[Table("LESSONS")]
    //public class ModelLessons
    //{
    //    [Key]
    //    [Column("ID")]
    //    public int id { get; set; }

    //    [Column("DATE_TIME_START")]
    //    public DateTime dateTimeStart { get; set; } = DateTime.Now;

    //    [Column("DATE_TIME_END")]
    //    public DateTime dateTimeEnd { get; set; } = DateTime.Now.AddHours(-1);

    //    [Column("LESSON_TOPIC")]
    //    public string lessonTopic { get; set; } = string.Empty;

    //    [Column("ASSIGNMENT")]
    //    public string assignment { get; set; } = string.Empty;

    //    [Column("LESSON_MEMO")]
    //    public string lessonMemo { get; set; } = string.Empty;

    //    [Column("MEMBER_ID")]
    //    [Required]
    //    public int memberId { get; set; }

    //    [ForeignKey("memberId")]
    //    public virtual ModelMember? ModelMember { get; set; }
    //}

    public class ModelLessons
    {
        public string id { get; set; } = string.Empty;
        public DateTime dateTimeStart { get; set; }
        public DateTime dateTimeEnd { get; set; }
        public string lessonTopic { get; set; } = string.Empty;
        public string assignment { get; set; } = string.Empty;
        public string lessonMemo { get; set; } = string.Empty;

        public int memberId { get; set; }
        public ModelMember? ModelMember { get; set; }
    }
}
