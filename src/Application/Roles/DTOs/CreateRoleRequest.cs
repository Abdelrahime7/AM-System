using Domain.Enums;

namespace Application.Roles.DTOs;


    public record CreateRoleRequest
    {
        public UserRole RoleType { get; set; }
    }


