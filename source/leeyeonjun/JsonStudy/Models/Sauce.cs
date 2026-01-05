using System.ComponentModel.DataAnnotations;

namespace JsonStudy.Models
{
    public class Sauce
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public bool IsVegan { get; set; }
    }
}
