

using Application.Common.Models;
using Application.Roles.DTOs;
using Application.Tokens.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.RoleInterfaces
{
    public interface IRoleCommands
    {
        Task<Result<int>> CreatRoleAsync(CreateTokenRequest request);
        Task<Result<bool>> DeleteRoleAsync(int ID);
        Task<Result<bool>> UpdateRoleAsync(UpdateTokenRequest request);

    }
}
