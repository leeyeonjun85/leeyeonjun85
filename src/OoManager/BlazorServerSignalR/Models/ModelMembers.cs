using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorServerSignalR.Data;

namespace BlazorServerSignalR.Models
{
    [Table("MEMBERS")]
    public class ModelMembers
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Column("NAME")]
        [MaxLength(20)]
        [Required]
        public string name { get; set; } = "김";

        [Column("GRADE")]
        public Grade grade { get; set; } = Grade.middleSchool1;

        [Column("COST")]
        public int cost { get; set; } = 150000;

        [Column("MEMBER_STATE")]
        [Required]
        public MemberState memberState { get; set; } = MemberState.Normal;

        [Column("PHONE_NUMBER")]
        public string phoneNumber { get; set; } = "010-";

        [Column("CLASS_PLAN")]
        [MaxLength(100)]
        public string classPlan { get; set; } = "월화수목금";

        [Column("MEMBER_MEMO")]
        public string memberMemo { get; set; } = "메모";
    }
}
