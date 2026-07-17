using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Dtos
{
    public class UpdateCharacterDto
    {
        public string? Name { get; set; }
        public string? Game { get; set; }
        public CharacterRole? Role { get; set; }
    }
}