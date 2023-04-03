using LiquorStoreApi.DTOs;
using LiquorStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiquorStoreApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-profile")]
        public ActionResult GetProfileData()
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email is null)
                return StatusCode(401, "No autorizado.");

            var result = _userService.GetProfileData(email);

            return StatusCode(200, result);
        }

        [HttpPut("update-profile")]
        public async Task<ActionResult> UpdateProfileData([FromBody] UserDto userDto)
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email is null)
                return StatusCode(401, "No autorizado.");

            var result = await _userService.UpdateProfileData(email, userDto);

            return Ok(result);
        }

        [HttpDelete("delete-account")]
        public async Task<ActionResult> DeleteAccount()
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email is null)
                return StatusCode(401, "No autorizado.");

            var result = await _userService.DeleteAccount(email);

            return StatusCode(200, result);
        }

        [HttpPatch("set-password")]
        public async Task<ActionResult> UpdatePassword([FromBody] PasswordUpdateDto password)
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email is null)
                return StatusCode(401, "No autorizado.");

            var result = await _userService.UpdatePassword(email, password);

            return StatusCode(200, result);
        }
    }
}
