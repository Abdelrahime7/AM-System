using Application.Interfaces.JwtService;
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
        private readonly IJwtService _tokenService;
        private readonly ICredentialChecker _credentialChecker;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(IJwtService tokenService,
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
              new Claim(ClaimTypes.Role, identity.Role.ToString()),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var tokens = await _tokenService.GenerateAndStoreTokensAsync(identity.id, claims);

            return Ok(tokens);
        }






    }


}
