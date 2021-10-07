using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web1
{
    public class Receipt{
        public Guid Id {get; set;}
        public DateTime DateBought {get; set;}
        public decimal AmountSpent {get; set;}
        public string ProductName {get; set;}
        public int ProductQuantity {get; set;}
        public User User {get; set;}
    }
}