using Application.Users.DTOs;

namespace Application.RoleRequeste
{
    public class CreateRoleSession
    {
        public CreateUserRequest UserRequest { get; set; } // base user info + role
        public object RoleRequest { get; set; } // role-specific payload 
    }
}
