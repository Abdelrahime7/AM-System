using Domain.Enums;

namespace Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public UserRole RoleType { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

}