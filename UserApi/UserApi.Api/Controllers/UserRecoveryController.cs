using Microsoft.AspNetCore.Mvc;
using UserApi.Api.Extensions;
using UserApi.Api.Filters.ViewModels;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;
using UserApi.Applications.Interfaces;
using UserApi.Domain.Exceptions;

namespace UserApi.Api.Controllers
{

    [ApiController]
    [Route("v1/recoveries")]
    public class UserRecoveryController : ControllerBase
    {
        private readonly IRecoveryService _RecoveryService;
        public UserRecoveryController(IRecoveryService userService)
        {
            _RecoveryService = userService;
        }


        [HttpGet("passwords")]
        public async Task<IActionResult> RecoveryPassword([FromQuery]RecoveryPasswordInputModel inputModel)
        {
            try
            {
                var user = await _RecoveryService.GetPassword(inputModel);

                return Ok(new ResultViewModel<RecoveryPasswordViewModel>(user));
            }
            catch (UserException e)
            {
                return NotFound(new ResultViewModel<List<RecoveryPasswordViewModel>>(e.Message)); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<RecoveryPasswordViewModel>>(e.Message));
            }
        }      


        [HttpPut("passwords")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordInputModel inputModel)
        {
            try
            {
                var user = await _RecoveryService.ChangePassword(inputModel);

                return Ok(new ResultViewModel<ChangePasswordViewModel>(user));
            }
            catch (UserException e)
            {
                return NotFound(new ResultViewModel<List<ChangePasswordViewModel>>(e.Message)); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<ChangePasswordViewModel>>(e.Message));
            }
        }

        [HttpGet("usernames")]
        public async Task<IActionResult> RecoveryUserName([FromQuery] RecoveryUserNameInputModel inputModel)
        {
            try
            {
                var user = await _RecoveryService.GetUserName(inputModel);

                return Ok(new ResultViewModel<RecoveryUsernameViewModel>(user));
            }
            catch (UserException e)
            {
                return NotFound(new ResultViewModel<List<RecoveryUsernameViewModel>>(e.Message)); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<RecoveryUsernameViewModel>>(e.Message));
            }
        }

        [HttpPut("usernames")]
        public async Task<IActionResult> ChangeUserName([FromBody] ChangeUserNameInputModel inputModel)
        {
            try
            {
                var user = await _RecoveryService.ChangeUserName(inputModel);

                return Ok(new ResultViewModel<ChangeUserNameViewModel>(user));
            }
            catch (UserException e)
            {
                return NotFound(new ResultViewModel<List<ChangeUserNameViewModel>>(e.Message)); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<ChangeUserNameViewModel>>(e.Message));
            }
        }
    }
}
