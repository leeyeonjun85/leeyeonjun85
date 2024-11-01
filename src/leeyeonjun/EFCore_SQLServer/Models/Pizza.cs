﻿using System.ComponentModel.DataAnnotations;

namespace EFCore_SQLServer.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public Sauce? Sauce { get; set; }

        public ICollection<Topping>? Toppings { get; set; }
    }
}
