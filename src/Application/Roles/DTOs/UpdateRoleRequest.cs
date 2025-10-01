using Domain.Enums;

namespace Application.Roles.DTOs;

public record UpdateRoleRequest
{
    public UserRole RoleType { get; set; }
}
