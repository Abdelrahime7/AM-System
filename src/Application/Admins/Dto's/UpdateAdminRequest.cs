using Domain.Enums;

namespace Application.Admins.Dto_s
{
    public class UpdateAdminRequest
    {
        public int Id { get; set; }
        public AccessLevels? levels { get; set; }
    }
}
