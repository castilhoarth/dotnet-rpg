using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController] //Atributos da classe
    [Route("api/[controller]")] //Configuramos o "link" para api/Nome da class
    public class CharacterController : ControllerBase
    {
        private static Character knight = new Character();

        [HttpGet] //Já aparece no Swagger
        public ActionResult<Character> Get() { //Se o método começa com Get o API assume se trata de Get automaticamente
            return Ok(knight); //Retorna o status 200 de ok junto com o resultado
        }
    }
}