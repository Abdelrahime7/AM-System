using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByRoleTypeAsync(UserRole roleType);
}