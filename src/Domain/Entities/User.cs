using Domain.Enums;

namespace Domain.Entities;

public class User
{
  

    public int Id { get; set; }
    public required string  FullName { get; set; }
    public string? Email { get; set; }
    public required string  Phone { get; set; }  
    public required string PasswordHash { get; set; }
    public required string Username { get; set; }

    public required UserRole Role { get; set; }

    public UserStatus Status { get; set; }
    public DateTime? LastLoginAt { get; set; }

}
   

   