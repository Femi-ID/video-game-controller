using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface ICharacterService
    {
        Task<List<CharacterResponseDto>> GetAllCharactersAsync();
        Task<CharacterResponseDto?> GetCharacterByIdAsync(int id);
        Task<Character> CreateCharacterAsync(CreateCharacterDto createCharacterDto);
        Task<CharacterResponseDto?> UpdateCharacterAsync(int id, UpdateCharacterDto updateCharacter);
        Task<bool> DeleteCharacterAsync(int id);
    }
}