using Domain.Enums;

namespace Application.Roles.DTOs;

public record RoleResponse
{
    public int Id { get; set; }

    public UserRole RoleType { get; set; } 

    public List<string> UsersName { get; set; } = null!;
}





