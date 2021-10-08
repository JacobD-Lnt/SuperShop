using System;
using System.Collections.Generic;

namespace Web1
{
    public class Product{
        public Guid Id {get; set;}
        public string Name {get; set;}
        public decimal Price {get; set;}
        public double Weight {get; set;}
        public int Quantity {get; set;}
        public int AmountSold {get; set;}
    }
}