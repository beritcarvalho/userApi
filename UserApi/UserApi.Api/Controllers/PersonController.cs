using Microsoft.AspNetCore.Mvc;
using UserApi.Applications.Interfaces;

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

        
        [HttpGet("{id:int}")]
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
    }
}
