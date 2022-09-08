﻿using Microsoft.AspNetCore.Mvc;
using UserApi.Api.Extensions;
using UserApi.Applications.InputModels;
using UserApi.Applications.Interfaces;
using UserApi.Applications.ViewModels;
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

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserInputModel inputUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<UserInputModel>(ModelState.GetErrors()));

                var user = await _userService.AddUser(inputUser);
                return Created($"/user/{user.Id}", new ResultViewModel<UserViewModel>(user));
            }
            catch(Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<UserViewModel>>(e.Message));
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserId([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);

                if(user == null)
                    return NotFound(new ResultViewModel<List<UserViewModel>>("ERR-02X01 Cadastro não encontrado"));

                return Ok(new ResultViewModel<UserViewModel>(user));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<UserViewModel>>(e.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserInputModel inputUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<UserInputModel>(ModelState.GetErrors()));


                var user = await _userService.UpdateUser(inputUser);

                if (user == null)
                    return NotFound(new ResultViewModel<List<UserViewModel>>("ERR-02X02 Cadastro não encontrado"));

                return Ok(new ResultViewModel<UserViewModel>(user)); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<UserViewModel>>(e.Message));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveUser([FromRoute] int id)
        {
            try
            {
                var user = await _userService.RemoveUserById(id);

                if (user == null)
                    return NotFound(new ResultViewModel<List<UserViewModel>>("ERR-02X03 Cadastro não encontrado"));

                return Ok(new ResultViewModel<UserViewModel>(user));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<UserViewModel>>(e.Message));
            }
        }







    }
}