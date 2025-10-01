using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.RoleInterfaces;
using Application.Roles.DTOs;
using Domain.Entities;

namespace Application.Roles.Features.Commands;

public partial class RoleCommands : IRoleCommands
{
    public Task<Result<int>> CreatRoleAsync(CreateRoleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteRoleAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateRoleAsync(UpdateRoleRequest request)
    {
        throw new NotImplementedException();
    }
}
