using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web1
{
    public class UserDto{
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name {get; set;}
    }
}