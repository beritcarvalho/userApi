using Microsoft.AspNetCore.Mvc;
using UserApi.Api.Extensions;
using UserApi.Applications.InputModels;
using UserApi.Applications.Interfaces;
using UserApi.Applications.ViewModels;

namespace UserApi.Api.Controllers
{

    [ApiController]
    [Route("v1/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("/account")]
        public async Task<IActionResult> AddAccount([FromBody] AccountInputModel inputAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<AccountInputModel>(ModelState.GetErrors()));

                var account = await _accountService.AddAccount(inputAccount);
                return Created($"/account/{account.Id}", new ResultViewModel<AccountViewModel>(account));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<AccountViewModel>>("ERR-05X011 Falha interna no servidor"));
            }
        }


        [HttpGet("/{id:int}")]
        public async Task<IActionResult> GetAccountId([FromRoute] int id)
        {
            try
            {
                var account = await _accountService.GetById(id);
                return Ok(account);
            }
            catch
            {
                return BadRequest("Erro");
            }
        }

        

        [HttpPut("/DisableAccount/{id:int}")]
        public async Task<IActionResult> DisableAccount([FromRoute] int id)
        {
            try
            {
                var account = await _accountService.DisableAccount(id);
                return Ok(account);
            }
            catch
            {
                return BadRequest("Erro");
            }
        }

        [HttpPut("/ActivateAccount/{id:int}")]
        public async Task<IActionResult> ActivateAccount([FromRoute] int id)
        {
            try
            {
                var account = await _accountService.ActivateAccount(id);
                return Ok(account);
            }
            catch
            {
                return BadRequest("Erro");
            }
        }


    }
}
