using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class CharacterMapper
    {
        public static CharacterResponseDto ToCharacterResponseDto(this Character characterModel)
        // takes a Character model object and returns the equivalent Dto
        {
            return new CharacterResponseDto
            {
                Id = characterModel.Id,
                Name = characterModel.Name,
                Game = characterModel.Game,
                Role = characterModel.Role,
            };
        }

        public static Character CreateCharacterFromDto(this CreateCharacterDto characterDto)
        // takes a characterDto and returns the equivalent Character object
        {
            return new Character
            {
                Name = characterDto.Name,
                Game = characterDto.Game,
                Role = characterDto.Role,
            };
        }

        // public static Character UpdateCharacterFromDto(this UpdateCharacterDto updateCharacterDto)
        // {
        //     return new Character
        //     {
        //         Name = updateCharacterDto.Name,
        //         Game = updateCharacterDto.Game,
        //         Role = updateCharacterDto.Role,
        //     };
        // }

        public static IQueryable<CharacterResponseDto> ProjectToResponseDto(this IQueryable<Character> query)
       => query.Select(c => new CharacterResponseDto
       {
           Id = c.Id,
           Name = c.Name,
           Game = c.Game,
           Role = c.Role
       });
    }
}