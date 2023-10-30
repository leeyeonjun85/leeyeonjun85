using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OoManager.Models
{
    [Table("MEMBERS")]
    public class ModelMembers
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Column("GRADE")]
        public string grade { get; set; } = Grades.PrimarySchool_4;

        [Column("NAME")]
        [MaxLength(20)]
        [Required]
        public string name { get; set; } = "김";

        [Column("OLD")]
        public int old { get; set; } = 11;

        [Column("CLASS_PLAN")]
        [MaxLength(100)]
        public string classPlan { get; set; } = "월화수목금";

        [Column("MONEY")]
        public int money { get; set; } = 150000;

        [Column("MEMBER_STATE")]
        [Required]
        public string memberState { get; set; } = States.Normal;

        [Column("PHONE_NUMBER")]
        public string phoneNumber { get; set; } = "010-";

        [Column("MEMBER_MEMO")]
        public string memberMemo { get; set; } = "메모";

        [Column("XP")]
        public int xp { get; set; } = 10;

        [Column("XP_LOG")]
        public string xpLog { get; set; } = "Xp 기록";
    }
}
