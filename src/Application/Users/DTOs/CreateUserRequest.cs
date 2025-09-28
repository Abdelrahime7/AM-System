using Domain.Enums;

namespace Application.Users.DTOs
{
    public record CreateUserRequest {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string PasswordHash { get; set; }
        public string? CcpNumber { get; set; }
        public UserStatus Status { get; set; }
        public int? RoleId { get; set; }

    };
}
