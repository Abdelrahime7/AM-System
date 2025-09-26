

using Domain.Entities;
using Domain.Enums;

namespace Application.Users.Features.Queries
{
   public partial class UsersQueries
        
    {
        public UserStatus GetUserStatus(User user) => user.Status;

        public UserRole GetUserRole(User user) => user.Role.RoleType;
    }
}
