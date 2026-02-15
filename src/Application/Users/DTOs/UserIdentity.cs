

using Domain.Enums;

namespace Application.Users.DTOs
{
    public record UserIdentity(int id,UserRole? Role,UserStatus ? Status);
}
