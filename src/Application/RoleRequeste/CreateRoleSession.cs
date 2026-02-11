using Application.Admins.Dto_s;
using Application.Affiliates.DTO_s;
using Application.Assisstants.Dto_s;
using Application.Drivers.DTO_s;
using Application.Users.DTOs;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.RoleRequeste
{
    public class CreateRoleSession
    {
        public CreateUserRequest UserRequest { get; set; }
        public Role RoleRequest { get; set; }
    }



    public abstract class Role { };

}
