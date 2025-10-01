using Domain.Enums;

namespace Application.Roles.DTOs;


    public record CreateTokenRequest
    {
        public UserRole RoleType { get; set; }
    }


