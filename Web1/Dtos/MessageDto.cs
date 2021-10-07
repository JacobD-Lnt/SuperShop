using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web1
{
    public class MessageDto{
        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Text {get; set;}
    }
}