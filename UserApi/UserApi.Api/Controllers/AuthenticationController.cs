using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Applications.Services;

namespace UserApi.Api.Controllers
{
    [ApiController]
    [Route("v1/authentications")]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenService _TokenService;
        public AuthenticationController(TokenService tokenService)
        {
            _TokenService = tokenService;
        }
        
  
        [HttpPut]
        public IActionResult Login()
        {
            var token = _TokenService.GenerateToken(null);
            return Ok(token);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public IActionResult GetUser() => Ok(User.Identity.Name);


        [Authorize(Roles = "resident")]
        [Authorize(Roles = "admin")]
        [HttpGet("resident")]
        public IActionResult GetRole() => Ok(User.Identity.Name);
    }
}
