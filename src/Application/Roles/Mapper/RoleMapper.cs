using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Roles.DTOs;
using Domain.Entities;


namespace Application.Roles.Mapper;

public class RoleMapper : IEntityMapper<Role, CreateRoleRequest,
    UpdateRoleRequest, RoleResponse>
{
    public Role ToEntity(CreateRoleRequest dto)
    {
        throw new NotImplementedException();
    }

    public RoleResponse ToResponse(Role entity)
    {
        throw new NotImplementedException();
    }

    public void ToUpdateEntity(Role entity, UpdateRoleRequest dto)
    {
        throw new NotImplementedException();
    }
}