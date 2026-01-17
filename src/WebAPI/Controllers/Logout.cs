using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly TokenService _service;

        public LogoutController(TokenService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string tokenValue)
        {
            if (string.IsNullOrWhiteSpace(tokenValue))
                return BadRequest(new { success = false, message = "Token value is required." });

            var result = await _service.RevokeRefreshTokenAsync(tokenValue);

            if (!result)
                return BadRequest(new { success = false, message = "Revocation failed or token not found." });

            return Ok(new { success = true, message = "Token revoked successfully."});
        }


    }
}
