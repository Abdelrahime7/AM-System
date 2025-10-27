using Application.Admins.Dto_s;
using Application.Users.DTOs;

namespace Application.Admins.DTO_s.session
{
    public class CreatAdminSession
    {
        public required CreateUserRequest UserRequest { get; set; }
        public required CreateAdminRequest DriverRequest { get; set; }
    }
}
