
using System.Security.Claims;


namespace Application.Interfaces.JwtService
{
    public interface IJwtService
    {
         string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        Task<bool> StoreRefreshTokenAsync(string token, int UserId);
        Task<bool> RevokeRefreshTokenAsync(string tokenValue);
        Task<object> GenerateAndStoreTokensAsync(int userId, IEnumerable<Claim> claims);
    }
}
