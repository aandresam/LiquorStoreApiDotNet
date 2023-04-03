using LiquorStoreApi.DTOs;
using LiquorStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService) 
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto loginDto)
        {
            var result = _authenticationService.Login(loginDto);
            
            return Ok(result);
        }

        [HttpPost("register")]
        public async  Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authenticationService.Register(registerDto);

            return StatusCode(201, result);
        }
    }
}
