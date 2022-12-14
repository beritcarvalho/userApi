using Microsoft.AspNetCore.Authorization;
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
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "admin,manager,sub-Manager")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserInputModel inputUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<UserInputModel>(ModelState.GetErrors()));

                var user = await _userService.AddUser(inputUser);
                return Created($"/user/{user.Id}", new ResultViewModel<UserAddViewModel>(user));
            }
            catch (UserException e)
            {
                return NotFound(new ResultViewModel<List<UserAddViewModel>>(e.Message)); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<UserAddViewModel>>(e.Message));
            }
        }
        
        [Authorize(Roles = "admin,manager,sub-Manager,doorman")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserId([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetUserByIdWithInclude(id);

                return Ok(new ResultViewModel<UserViewModel>(user));
            }
            catch (UserException e)
            {
                return NotFound(new ResultViewModel<List<UserViewModel>>(e.Message)); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<UserViewModel>>(e.Message));
            }
        }

        [Authorize(Roles = "admin,manager,sub-Manager")]
        [HttpPut("Active/{id:int}")]
        public async Task<IActionResult> ActiveUser([FromRoute] int id)
        {
            try
            {
                var user = await _userService.ActiveUser(id);

                return Ok(new ResultViewModel<UserActiveViewModel>(user));
            }
            catch (UserException e)
            {
                return NotFound(new ResultViewModel<List<UserViewModel>>(e.Message)); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<UserViewModel>>(e.Message));
            }
        }


        [Authorize(Roles = "admin,manager,sub-Manager")]
        [HttpPut("Inactive/{id:int}")]
        public async Task<IActionResult> InactiveUser([FromRoute] int id)
        {
            try
            {
                var user = await _userService.InactiveUser(id);

                return Ok(new ResultViewModel<UserInactiveViewModel>(user));
            }
            catch (UserException e)
            {
                return NotFound(new ResultViewModel<List<UserInactiveViewModel>>(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<UserInactiveViewModel>>(e.Message));
            }
        }

        [Authorize(Roles = "admin,manager")]
        [HttpPut("{idUser:int}/roles/{idRole:int}")]
        public async Task<IActionResult> ChangeUserRole([FromRoute] int idUser, int idRole)
        {
            try
            {
                var user = await _userService.ChangeRole(idUser, idRole);

                return Ok(new ResultViewModel<ChangeRoleViewModel>(user));
            }
            catch (UserException e)
            {
                return NotFound(new ResultViewModel<List<ChangeRoleViewModel>>(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<ChangeRoleViewModel>>(e.Message));
            }
        }
    }
}
