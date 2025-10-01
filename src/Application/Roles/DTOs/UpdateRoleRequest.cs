using Domain.Enums;

namespace Application.Roles.DTOs;

public record UpdateTokenRequest
{
    public UserRole RoleType { get; set; }
}
