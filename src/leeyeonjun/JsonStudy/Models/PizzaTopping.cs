using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JsonStudy.Models
{
    public class PizzaTopping
    {
        public int PizzaId { get; set; }
        public int ToppingId { get; set; }
    }
}
