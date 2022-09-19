using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CORE_EF_WEB_API.Dtos.Character;

namespace CORE_EF_WEB_API.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Chamila" }
        };
        private readonly IMapper mapper;

        public CharacterService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();
            Character character = mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponce.Data = characters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponce<List<GetCharacterDto>> responce =
                new ServiceResponce<List<GetCharacterDto>>();
            try
            {
                Character character = characters.First(c => c.Id == id);
                characters.Remove(character);

                responce.Data = characters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
            }

            return responce;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponce<List<GetCharacterDto>>
            {
                Data = characters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList()
            };
        }

        public async Task<ServiceResponce<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponce = new ServiceResponce<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponce.Data = mapper.Map<GetCharacterDto>(character);
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> UpdateCharacter(
            UpdateCharacterDto updatedCharacter
        )
        {
            ServiceResponce<GetCharacterDto> responce = new ServiceResponce<GetCharacterDto>();
            try
            {
                Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                //WITHOUT ?AUTO MAPPER-----------------------------
                // character.Name = updatedCharacter.Name;
                // character.HitPoints = updatedCharacter.HitPoints;
                // character.Strength = updatedCharacter.Strength;
                // character.defence = updatedCharacter.defence;
                // character.Integligence = updatedCharacter.Integligence;
                // character.Class = updatedCharacter.Class;

                //USINNG AUTO MAPPER-------------------------------
                mapper.Map(updatedCharacter, character);

                responce.Data = mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
            }

            return responce;
        }
    }
}
