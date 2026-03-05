
using Domain.Enums;

namespace Application.Admins.Dto_s
{
    public class AdminResponse
    {
        public AccessLevels levels { get; set; }
        public int UserID { get; set; }
    }
}
