using Microsoft.AspNetCore.Mvc;
using UserApi.Api.Extensions;
using UserApi.Api.Filters.ViewModels;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;
using UserApi.Applications.Interfaces;

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

        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] AccountInputModel inputAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                { 
             
                    return BadRequest(new ResultViewModel<AccountInputModel>(ModelState.GetErrors()));
                }

                var account = await _accountService.AddAccount(inputAccount);
                return Created($"/account/{account.Id}", new ResultViewModel<AccountViewModel>(account));
            }
            catch(Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<AccountViewModel>>(e.Message));
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccountId([FromRoute] int id)
        {
            try
            {
                var account = await _accountService.GetById(id);

                if(account == null)
                    return NotFound(new ResultViewModel<List<AccountViewModel>>("ERR-C02X01 Cadastro não encontrado"));

                return Ok(new ResultViewModel<AccountViewModel>(account));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<AccountViewModel>>(e.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountInputModel inputAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<AccountInputModel>(ModelState.GetErrors()));


                var account = await _accountService.UpdateAccount(inputAccount);

                if (account == null)
                    return NotFound(new ResultViewModel<List<AccountViewModel>>("ERR-C02X01 Cadastro não encontrado"));

                return Ok(new ResultViewModel<AccountViewModel>(account)); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<AccountViewModel>>(e.Message));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAccount([FromRoute] int id)
        {
            try
            {
                var account = await _accountService.RemoveById(id);

                if (account == null)
                    return NotFound(new ResultViewModel<List<AccountViewModel>>("ERR-C02X01 Cadastro não encontrado"));

                return Ok(new ResultViewModel<AccountViewModel>(account));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<AccountViewModel>>(e.Message));
            }
        }
    }
}
