using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web1
{
    public class Message{
        
        public Guid Id {get; set;}
        public string Text {get; set;}
        public User Receiver {get; set;}
        public string Sender {get; set;}
        public Message(){
            Sender = "";
            Receiver = new();
        }
        public Message(MessageDto messageDto){ 
            Id = Guid.NewGuid();
            Text = messageDto.Text;
            Sender = "";
            Receiver = new();
        }
    }
}