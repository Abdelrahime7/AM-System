

using Domain.Enums;

namespace Application.Users.DTOs
{
    public record UpdateUserRequest
    {
        public int Id { get; set; }
        public  string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public UserStatus ?Status { get; set; }
        public DateTime? LastLoginAt { get; set; }
       
    }
}
