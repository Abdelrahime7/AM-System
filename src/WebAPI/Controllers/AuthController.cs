using Application.Interfaces.JwtService;
using Application.Interfaces.Repositories;
using Application.Users.CredentialChecker;
using Application.Users.DTOs;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and password are required.");

            var identity = await _credentialChecker.CheckCredentialsAsync(request.Username, request.Password);
            if (identity == null)
                return Unauthorized("Invalid username or password.");
            UserRole role = identity.Role!.Value;
            UserStatus status = identity.Status!.Value;


            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, identity.id.ToString()),
              new Claim(ClaimTypes.Role, identity.Role.ToString()),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()
              
              )
             };

            var tokens = await _tokenService.GenerateAndStoreTokensAsync(identity.id, claims);
           var response = new {status,role, tokens };
            return Ok(response);

        }






    }


}
