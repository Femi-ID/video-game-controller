using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Models
{
    [Table("Characters")]
    public class Character
    {
        public int Id {get; set; }
        public string  Name { get; set; } = String.Empty;
        public string Game { get; set; } = String.Empty;
        public CharacterRole Role { get; set; }

    }
}