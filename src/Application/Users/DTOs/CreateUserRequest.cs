using Domain.Enums;

namespace Application.Users.DTOs
{
    public record CreateUserRequest {

        public required string UserName { get; set; }

        public required string FullName { get; set; }
        public required string Phone { get; set; }

        public string  ?Email { get; set; }


        public required string PasswordHash { get; set; }
        public UserStatus Status { get; set; }
     

    };
}
