

using Application.Common.Models;
using Application.Roles.DTOs;


namespace Application.Interfaces.RoleInterfaces
{
    public interface IRoleCommands
    {
        Task<Result<int>> CreatRoleAsync(CreateRoleRequest request);
        Task<Result<bool>> DeleteRoleAsync(int ID);
        Task<Result<bool>> UpdateRoleAsync(UpdateRoleRequest request);

    }
}
