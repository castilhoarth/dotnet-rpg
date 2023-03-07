using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController] //Atributos da classe
    [Route("api/[controller]")] //Configuramos o "link" para api/Nome da class
    public class CharacterController : ControllerBase
    {
      
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")] //Já aparece no Swagger
        public ActionResult<List<Character>> Get() { //Se o método começa com Get o API assume se trata de Get automaticamente
            List<Character> characters = _characterService.GetAllCharacters();
            return Ok(characters); //Retorna o status 200 de ok junto com o resultado
        }

        [HttpGet("GetSingle/{id}")]
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(_characterService.GetCharacterById(id));
        }

        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter)
        {  
            return Ok(_characterService.AddCharacter(newCharacter));
        }
    }
}