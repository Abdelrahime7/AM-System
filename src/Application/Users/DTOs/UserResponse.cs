using Domain.Enums;

namespace Application.Users.DTOs
{
    public record UserResponse
    {
        public int Id { get; set; }
        public  string Username { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? LastLoginAt { get; set; }
      
    }
}
