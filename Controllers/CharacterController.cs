using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/character")]
    [ApiController]
    public class CharacterController(ICharacterService _characterService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CharacterResponseDto>> GetCharacters()
        {
            var response = await _characterService.GetAllCharactersAsync();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CharacterResponseDto>> GetCharacterById([FromRoute] int id)
        {
            var response = await _characterService.GetCharacterByIdAsync(id);
            return response is null ? NotFound($"Character with id-{id} was not found") : Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CharacterResponseDto>> CreateCharacter([FromBody] CreateCharacterDto createCharacterDto)
        {
            var newCharacter = await _characterService.CreateCharacterAsync(createCharacterDto);
            return CreatedAtAction(nameof(GetCharacterById), new {id=newCharacter.Id}, newCharacter);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCharacter([FromRoute] int id, [FromBody] UpdateCharacterDto updateCharacterDto)
        {
            var updatedCharacter = await _characterService.UpdateCharacterAsync(id, updateCharacterDto);
            return updatedCharacter is null ? NotFound($"Character with id-{id} was not found") : Ok(updatedCharacter);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCharacter([FromRoute] int id)
        {
           var response = await _characterService.DeleteCharacterAsync(id);
           return response is false ? NotFound($"Character with id-{id} was not found") : NoContent(); 
        }
    }
}