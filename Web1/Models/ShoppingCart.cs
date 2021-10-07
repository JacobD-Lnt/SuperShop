using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web1
{
    public class ShoppingCart{
        public Guid Id {get; set;}
        public string ProductId {get; set;}
        public int Quantity {get; set;}
        public User User {get; set;}
    }
}