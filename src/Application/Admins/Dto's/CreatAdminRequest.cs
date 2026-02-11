using Application.RoleRequeste;
using Domain.Enums;

namespace Application.Admins.Dto_s
{
    public class CreateAdminRequest:Role
    {
        public AccessLevels? levels {  get; set; }
    }
}
