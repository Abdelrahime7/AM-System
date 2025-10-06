using Application.Common.Models;
using Application.Roles.DTOs;
using Domain.Enums;

namespace Application.Interfaces.RoleInterfaces;

public interface IRoleQueries
{
    Task<Result<IEnumerable<RoleResponse>>> GetAllRolesAsync();
    Task<Result<RoleResponse>> GetRoleByIdAsync(int id);
    Task<Result<RoleResponse>> GetByRoleTypeAsync(UserRole roleType);
}