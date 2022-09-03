using Microsoft.AspNetCore.Mvc;
using UserApi.Api.Extensions;
using UserApi.Applications.InputModels;
using UserApi.Applications.Interfaces;
using UserApi.Applications.ViewModels;

namespace UserApi.Api.Controllers
{

    [ApiController]
    [Route("v1/people")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("/person")]
        public async Task<IActionResult> AddPerson([FromBody] PersonInputModel inputPerson)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<PersonInputModel>(ModelState.GetErrors()));

                var person = await _personService.AddPerson(inputPerson);
                return Created($"/person/{person.Id}", new ResultViewModel<PersonViewModel>(person));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<PersonViewModel>>("ERR-05X011 Falha interna no servidor"));
            }
        }


        [HttpGet("/{id:int}")]
        public async Task<IActionResult> GetPersonId([FromRoute] int id)
        {
            try
            {
                var person = await _personService.GetById(id);
                return Ok(person);
            }
            catch
            {
                return BadRequest("Erro");
            }
        }

        

        [HttpPut("/DisablePerson/{id:int}")]
        public async Task<IActionResult> DisablePerson([FromRoute] int id)
        {
            try
            {
                var person = await _personService.DisablePerson(id);
                return Ok(person);
            }
            catch
            {
                return BadRequest("Erro");
            }
        }

        [HttpPut("/ActivatePerson/{id:int}")]
        public async Task<IActionResult> ActivatePerson([FromRoute] int id)
        {
            try
            {
                var person = await _personService.ActivatePerson(id);
                return Ok(person);
            }
            catch
            {
                return BadRequest("Erro");
            }
        }


    }
}
