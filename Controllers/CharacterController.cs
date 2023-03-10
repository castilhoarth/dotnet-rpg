using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.dtos.Character;
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
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get() { //Se o método começa com Get o API assume se trata de Get automaticamente
  
            return Ok(await _characterService.GetAllCharacters()); //Retorna o status 200 de ok junto com o resultado
        }

        [HttpGet("GetSingle/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter) //Métodos assíncronos permitem que as threads sejam liberadas e não fiquem bloquedas, caso necessitem de uma condição para prosseguir
        {  //Por exemplo, em métodos síncronos, uma chamada de consulta no banco de dados poderia bloquear a thread e impedir novas requisições, atrapalhando a experiência do usuário
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacter) //Métodos assíncronos permitem que as threads sejam liberadas e não fiquem bloquedas, caso necessitem de uma condição para prosseguir
        {  //Por exemplo, em métodos síncronos, uma chamada de consulta no banco de dados poderia bloquear a thread e impedir novas requisições, atrapalhando a experiência do usuário
            var response = await _characterService.UpdateCharacter(updateCharacter);

            if(response.success == false)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(int id) //Métodos assíncronos permitem que as threads sejam liberadas e não fiquem bloquedas, caso necessitem de uma condição para prosseguir
        {  //Por exemplo, em métodos síncronos, uma chamada de consulta no banco de dados poderia bloquear a thread e impedir novas requisições, atrapalhando a experiência do usuário
            var response = await _characterService.DeleteCharacter(id);

            if(response.success == false)
                return NotFound(response);

            return Ok(response);
        }
    }
}