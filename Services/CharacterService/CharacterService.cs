using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CORE_EF_WEB_API.Data;
using CORE_EF_WEB_API.Dtos.Character;
using Microsoft.EntityFrameworkCore;

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
        private readonly DataContext context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
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

        public async Task<ServiceResponce<List<GetCharacterDto>>> GetAllCharactersEF()
        {
            var responce = new ServiceResponce<List<GetCharacterDto>>();
            var dbCharacters = await context.Characters.ToListAsync();
            responce.Data = dbCharacters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
            return responce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> GetCharacterByIdEF(int id)
        {
            var serviceResponce = new ServiceResponce<GetCharacterDto>();
            var dbCharacter = await context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponce.Data = mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> AddCharacterEF(
            AddCharacterDto newCharacter
        )
        {
            var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();
            Character character = mapper.Map<Character>(newCharacter);
            // character.Id = characters.Max(c => c.Id) + 1; SQLServer increase it automatically
            context.Characters.Add(character);
            await context.SaveChangesAsync();
            serviceResponce.Data = await context.Characters
                .Select(c => mapper.Map<GetCharacterDto>(c))
                .ToListAsync();
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> UpdateCharacterEF(
            UpdateCharacterDto updatedCharacter
        )
        {
            ServiceResponce<GetCharacterDto> responce = new ServiceResponce<GetCharacterDto>();
            try
            {
                var character = context.Characters.FirstOrDefaultAsync(
                    c => c.Id == updatedCharacter.Id
                );

                //WITHOUT ?AUTO MAPPER-----------------------------
                // character.Name = updatedCharacter.Name;
                // character.HitPoints = updatedCharacter.HitPoints;
                // character.Strength = updatedCharacter.Strength;
                // character.defence = updatedCharacter.defence;
                // character.Integligence = updatedCharacter.Integligence;
                // character.Class = updatedCharacter.Class;

                //USINNG AUTO MAPPER-------------------------------
                await mapper.Map(updatedCharacter, character);

                await context.SaveChangesAsync();

                responce.Data = mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
            }

            return responce;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> DeleteCharacterEF(int id)
        {
            ServiceResponce<List<GetCharacterDto>> responce =
                new ServiceResponce<List<GetCharacterDto>>();
            try
            {
                Character character = await context.Characters.FirstAsync(c => c.Id == id);
                context.Characters.Remove(character);
                await context.SaveChangesAsync();

                responce.Data = await context.Characters
                    .Select(c => mapper.Map<GetCharacterDto>(c))
                    .ToListAsync();
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
