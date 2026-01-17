using Application.Interfaces.JwtService;
using Application.Users.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly IJwtService  _service;

        public LogoutController(IJwtService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TokenValue))
                return BadRequest(new { success = false, message = "Token value is required." });

            var result = await _service.RevokeRefreshTokenAsync(request.TokenValue);

            if (!result)
                return BadRequest(new { success = false, message = "Revocation failed or token not found." });

            return Ok(new { success = true, message = "Token revoked successfully."});
        }


    }
}
