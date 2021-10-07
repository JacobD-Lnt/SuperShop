using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Web2
{
    public class ProductDto{
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name {get; set;}
        [Required]
        public decimal Price {get; set;}
        [Required]
        public double Weight {get; set;}
        [Required]
        public int Quantity {get; set;}
    }
}