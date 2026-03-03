using Application.Interfaces.JwtService;
using Application.Interfaces.Repositories;
using Application.Users.CredentialChecker;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefereshTokenController(ITokenRepository tokenRepository, IJwtService jwtService,
        ICredentialChecker credentialChecker) : ControllerBase
    {

        private readonly ITokenRepository _tokenRepository = tokenRepository;
        private readonly IJwtService _jwtService= jwtService;
        private readonly ICredentialChecker _credentialChecker= credentialChecker;

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var storedToken = await _tokenRepository.GetbyValueAsync(request.RefreshToken);
            if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            }
            var userClaims =await _credentialChecker.BuildClaims(storedToken.UserId);

            if (userClaims != null)
            {

                var newAccessToken = _jwtService.GenerateAccessToken(userClaims);


                // Optionally rotate refresh token
                var newRefreshToken = _jwtService.GenerateRefreshToken();
                await _jwtService.StoreRefreshTokenAsync(newRefreshToken, storedToken.UserId);

                // Revoke old refresh token if rotating
                await _jwtService.RevokeRefreshTokenAsync(request.RefreshToken);

                return Ok(new
                {
                    accessToken = newAccessToken,
                    refreshToken = newRefreshToken
                });
            }
            return BadRequest();
        }

    }
}
