using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class CharacterService(AppDBContext _context) : ICharacterService
    {
        public async Task<Character> CreateCharacterAsync(CreateCharacterDto createCharacterDto)
        {
            var characterModel = CharacterMapper.CreateCharacterFromDto(createCharacterDto);
            await _context.Characters.AddAsync(characterModel);
            await _context.SaveChangesAsync();
            System.Console.WriteLine("nw character- ", characterModel);
            return characterModel;
        }

        public async Task<bool> DeleteCharacterAsync(int id)
        {
            var existingCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCharacter is null) return false;

            _context.Characters.Remove(existingCharacter);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CharacterResponseDto>> GetAllCharactersAsync()
        {
            return await _context.Characters.Select(c => new CharacterResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Game = c.Game,
                Role = c.Role
            }).ToListAsync();

        }

        public async Task<CharacterResponseDto?> GetCharacterByIdAsync(int id)
        {
            // var character = await _context.Characters.Where(c => c.Id == id).Select(c => new CharacterResponseDto
            // {
            //     Id = c.Id,
            //     Name = c.Name,
            //     Game = c.Game,
            //     Role = c.Role
            // }).FirstOrDefaultAsync();

            var character = await _context.Characters
                .Where(c => c.Id == id)
                .ProjectToResponseDto()
                .FirstOrDefaultAsync();
            return character;
        }

        public async Task<CharacterResponseDto?> UpdateCharacterAsync(int id, UpdateCharacterDto updateCharacterDto)
        {
            var existingCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCharacter is null) return null;

            if (!string.IsNullOrWhiteSpace(updateCharacterDto.Name)) existingCharacter.Name = updateCharacterDto.Name;
            if (!string.IsNullOrWhiteSpace(updateCharacterDto.Game)) existingCharacter.Game = updateCharacterDto.Game;
            if (updateCharacterDto.Role.HasValue) existingCharacter.Role = updateCharacterDto.Role.Value;

            await _context.SaveChangesAsync();
            return existingCharacter.ToCharacterResponseDto();
        }
    }
}