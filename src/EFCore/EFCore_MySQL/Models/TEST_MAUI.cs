using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EFCore_MySQL.Models
{
    internal class TEST_MAUI
    {

        [Key]
        [Column("ID")]
        public string MAUI_IDNT { get; set; } = "";

        [Column("이름")]
        public string? ANIM_NAME { get; set; }

        [Column("설명")]
        public string? ANIM_DESC { get; set; }

        [Column("파일")]
        public byte[]? ANIM_PICT { get; set; }
    }
}
