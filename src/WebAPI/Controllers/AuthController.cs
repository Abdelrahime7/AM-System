using Application.Interfaces.Repositories;
using Application.Users.CredentialChecker;
using Application.Users.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly ICredentialChecker _credentialChecker;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(TokenService tokenService,
            ICredentialChecker credentialChecker,
             ITokenRepository tokenRepository)
        {
            _tokenService = tokenService;
            _credentialChecker = credentialChecker;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Username and password are required.");

            var identity = await _credentialChecker.CheckCredentialsAsync(request.Username, request.Password);
            if (identity == null)
                return Unauthorized("Invalid username or password.");

            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Sub, identity.id.ToString()),
               new Claim(ClaimTypes.Role, identity.Role.ToString())
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            //save refreshToken in DB with userId
            _tokenService.StorRefereshToken(refreshToken, identity.id.Value);
          

            return Ok(new { accessToken, refreshToken });
        }

        



    }


}
