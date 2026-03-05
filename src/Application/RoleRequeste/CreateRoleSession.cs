
using Application.Users.DTOs;


namespace Application.RoleRequeste
{
    public class CreateRoleSession
    {
        public CreateUserRequest UserRequest { get; set; }
        public Role RoleRequest { get; set; }
    }



    public abstract class Role { };

}
