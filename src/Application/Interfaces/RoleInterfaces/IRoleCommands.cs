using Application.Common.Models;
using Application.Roles.DTOs;

namespace Application.Interfaces.RoleInterfaces;

public interface IRoleCommands
{
    Task<Result<int>> CreateRoleAsync(CreateRoleRequest request);
    Task<Result<bool>> DeleteRoleAsync(int id);
    Task<Result<bool>> UpdateRoleAsync(UpdateRoleRequest request);
}