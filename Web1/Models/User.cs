using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Linq;

namespace Web1
{
    public class User{
        [Key]
        public string Username {get; set;}
        public string Name {get; set;}
        [InverseProperty("Receiver")]
        public List<Message> Inbox {get; set;}
        [InverseProperty("Sender")]
        public List<Message> Sent {get; set;}
        [JsonIgnore]
        public List<ShoppingCart> ShoppingCarts {get; set;}
        [JsonIgnore]
        public List<Receipt> Receipts {get; set;}
        public User(){
            Inbox = new();
            ShoppingCarts = new();
            Receipts = new();
        }
        public User(UserDto userDto){ 
            string microGuid = String.Join("", Guid.NewGuid().ToString().Take(8));
            Name = userDto.Name;
            Username = $"{userDto.Name}-{microGuid}";
            Inbox = new();
            ShoppingCarts = new();
            Receipts = new();
        }
    }
}