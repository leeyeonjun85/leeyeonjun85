using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EFCore_MySQL.Models
{
    public class TEST_MAUI
    {
        public int Id { get; set; }

        public string? ANIM_NAME { get; set; }

        public string? ANIM_DESC { get; set; }

        public byte[]? ANIM_PICT { get; set; }
    }
}
