using Domain.Enums;
using Microsoft.AspNetCore.Authorization;


namespace Infrastructure.Services
{
    public class ApprovedRoleRequirement : IAuthorizationRequirement
    {
        public UserRole RequiredRole { get; }
        
        public bool AllowSuperAdminBypass { get; }
        public bool AllowAdminBypass { get; }



        public ApprovedRoleRequirement(UserRole role, bool allowSuperAdminBypass = false, bool allowAdminBypass = false)
        {
            RequiredRole = role;
            AllowSuperAdminBypass = allowSuperAdminBypass;
            AllowAdminBypass = allowAdminBypass;
        }
    }

}
