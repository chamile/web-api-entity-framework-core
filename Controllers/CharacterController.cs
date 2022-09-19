using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE_EF_WEB_API.Dtos.Character;
using CORE_EF_WEB_API.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace CORE_EF_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        public ICharacterService CharacterService { get; }

        public CharacterController(ICharacterService characterService)
        {
            this.CharacterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> Get()
        {
            return Ok(await CharacterService.GetAllCharacters());
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> GetSingle()
        {
            return Ok(await CharacterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponce<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await CharacterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            return Ok(await CharacterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponce<GetCharacterDto>>> UpdateCharacter(
            UpdateCharacterDto newCharacter
        )
        {
            var responce = await CharacterService.UpdateCharacter(newCharacter);
            if (responce.Data == null)
            {
                return NotFound(responce);
            }
            return Ok(responce);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> Delete(int id)
        {
            var responce = await CharacterService.DeleteCharacter(id);
            if (responce.Data == null)
            {
                return NotFound(responce);
            }
            return Ok(responce);
        }
    }
}
