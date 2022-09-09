using Microsoft.AspNetCore.Mvc;
using UserApi.Api.Extensions;
using UserApi.Api.Filters.ViewModels;
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

        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] AccountInputModel inputAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<AccountInputModel>(ModelState.GetErrors()));

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
                    return NotFound(new ResultViewModel<List<AccountViewModel>>("ERR-02X01 Cadastro não encontrado"));

                return Ok(new ResultViewModel<AccountViewModel>(account));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<AccountViewModel>>(e.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountInputModel inputAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<AccountInputModel>(ModelState.GetErrors()));


                var account = await _accountService.UpdateAccount(inputAccount);

                if (account == null)
                    return NotFound(new ResultViewModel<List<AccountViewModel>>("ERR-02X02 Cadastro não encontrado"));

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
                    return NotFound(new ResultViewModel<List<AccountViewModel>>("ERR-02X03 Cadastro não encontrado"));

                return Ok(new ResultViewModel<AccountViewModel>(account));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<AccountViewModel>>(e.Message));
            }
        }







    }
}
