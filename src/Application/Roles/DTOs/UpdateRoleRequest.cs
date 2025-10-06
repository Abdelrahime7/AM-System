using Domain.Enums;

namespace Application.Roles.DTOs;

public record UpdateRoleRequest
{
    public int Id { get; set; }
    public UserRole? RoleType { get; set; }
}
