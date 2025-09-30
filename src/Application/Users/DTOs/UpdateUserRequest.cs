

using Domain.Enums;

namespace Application.Users.DTOs
{
    public record UpdateUserRequest
    {
        public int Id { get; set; }
        public  string? FullName { get; set; }
        public  string? Email { get; set; }
        public  string? Phone { get; set; }
        public string? PasswordHash { get; set; }
        public string? CcpNumber { get; set; }
        public UserStatus ?Status { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int? RoleId { get; set; }
    }
}
