using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Dtos
{
    public class CreateCharacterDto
    {
        public string Name { get; set; } = String.Empty;
        public string Game { get; set; } = String.Empty;
        public CharacterRole Role { get; set; }
    }
}