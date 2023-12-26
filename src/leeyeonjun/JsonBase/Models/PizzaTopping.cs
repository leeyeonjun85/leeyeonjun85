using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JsonBase.Models
{
    public class PizzaTopping
    {
        public int PizzaId { get; set; }
        public int ToppingId { get; set; }
    }
}
