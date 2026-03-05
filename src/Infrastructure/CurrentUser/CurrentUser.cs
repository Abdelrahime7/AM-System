using Application.Interfaces.CurrentUser;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Infrastructure.Currentuser
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? Username =>
            _httpContextAccessor.HttpContext?.User?.Identity?.Name;
    }

}
