using System;
using System.Collections.Generic;

namespace Web2
{
    public class Product{
        public Guid Id {get; set;}
        public string Name {get; set;}
        public decimal Price {get; set;}
        public double Weight {get; set;}
        public int Quantity {get; set;}
        public int AmountSold {get; set;}
        public Product(){
            
        }
        public Product(ProductDto productDto){
            Id = Guid.NewGuid();
            Name = productDto.Name;
            Price = productDto.Price;
            Weight = productDto.Weight;
            Quantity = productDto.Quantity;
            AmountSold = 0;
        }
    }
}