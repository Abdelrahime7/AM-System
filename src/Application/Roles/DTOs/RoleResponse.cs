using Application.Users.DTOs;

namespace Application.Roles.DTOs;


    public record RoleResponse
    {
        public int Id { get; set; }

        public string RoleType { get; set; } = string.Empty;

        public List<UserResponse> Users { get; set; } = new();
    }





