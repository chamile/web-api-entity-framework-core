using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CORE_EF_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController: ControllerBase
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character {Id=1,Name="Chamila"}
        };
        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get(){
            return Ok(characters);
        }

        [HttpGet]
        public ActionResult<List<Character>> GetSingle(){
            return Ok(characters[0]);
        }

        [HttpGet("{id}")]
        public ActionResult<List<Character>> GetSingle(int id){
            return Ok(characters.FirstOrDefault(c=>c.Id ==id));
        }
        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter){

            characters.Add(newCharacter);
            return Ok(characters);
        }
    }
}