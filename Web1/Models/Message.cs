using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web1
{
    public class Message{
        
        public Guid Id {get; set;}
        public string Text {get; set;}
        [JsonIgnore]
        public User Receiver {get; set;}
        [JsonIgnore]
        public User Sender {get; set;}
        public Message(){
            Sender = new();
            Receiver = new();
        }
        public Message(MessageDto messageDto){ 
            Id = Guid.NewGuid();
            Text = messageDto.Text;
            Sender = new();
            Receiver = new();
        }
    }
}