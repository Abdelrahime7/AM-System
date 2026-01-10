using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService 
{
    private readonly JwtSetting _settings;
    private readonly ITokenRepository _tokenRepository;
    private readonly ILogger _logger;
     
    public TokenService(IOptions<JwtSetting> settings ,
        ITokenRepository tokenRepository,
        ILogger logger)
    {
        _settings = settings.Value;
        _tokenRepository = tokenRepository;
        _logger = logger;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.LifeTime),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }

    private RefereshToken CreateRefreshToken(string token, int userId)
    { return new RefereshToken 
    { TokenValue = token,
       ExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenLifeTime),
        CreatedAt = DateTime.UtcNow,
        UserId = userId
    }; 
    }

    public async Task<bool> StoreRefreshTokenAsync(string token, int UserId)
    {
        var RefereshToken = CreateRefreshToken(token, UserId);

        try
        {
            await _tokenRepository.AddAsync(RefereshToken);
            return true;
        }
        catch
        (Exception ex)
        {
            _logger.LogError(ex, "Failed to store refresh token");
            return false;
        }
     }

    public async Task<bool> RevokeRefreshTokenAsync(string tokenValue)
    {
        var token = await _tokenRepository.GetbyValueAsync(tokenValue);
        if (token == null || token.IsRevoked)
            return false;

        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;

       _tokenRepository.Update(token);
        return true;
    }

    public async Task<object> GenerateAndStoreTokensAsync(int userId, IEnumerable<Claim> claims)
    {
        var accessToken = GenerateAccessToken(claims);
        var refreshToken = GenerateRefreshToken();

        var success = await StoreRefreshTokenAsync(refreshToken, userId);
        if (!success)
            throw new Exception("Failed to store refresh token.");

        return new { accessToken, refreshToken };
    }



}
