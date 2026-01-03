using Domain.Enums;

namespace Application.Users.DTOs
{
    public record UserResponse
    {
        public int Id { get; set; }
        public  string Username { get; set; }
        public UserRole role { get; set; }
        public  string FullName { get; set; }
        public  string Phone { get; set; }

        public string? Email { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? LastLoginAt { get; set; }
      
    }
}
