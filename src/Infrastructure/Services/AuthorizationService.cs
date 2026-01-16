using Application.Interfaces.Repositories;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Infrastructure.Services
{
    public class ApprovedRoleHandler : AuthorizationHandler<ApprovedRoleRequirement>
    {
        private readonly IUserRepository _users;
       
        public ApprovedRoleHandler(IUserRepository users )
        {

            _users = users;
          
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ApprovedRoleRequirement requirement)
        {

           
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return;

            var user = await _users.GetByIdAsync(int.Parse(userId));
            if (user == null) return;


            var roleMatch = user.Role == requirement.RequiredRole && user.Status==UserStatus.Active;

            var superAdminBypass = requirement.AllowSuperAdminBypass && user.Role == UserRole.Admin;
    
            var AdminBypass=requirement.AllowAdminBypass &&
                user.Role == UserRole.Admin && user.Status== UserStatus.Active;

            if (roleMatch || superAdminBypass||AdminBypass)
            {
                context.Succeed(requirement);
            }
        }
    }


}

