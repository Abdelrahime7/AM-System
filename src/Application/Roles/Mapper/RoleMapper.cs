using Application.Interfaces.Common.Mappers;
using Application.Roles.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Roles.Mapper;

public class RoleMapper : IEntityMapper<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse>
{
    public Role ToEntity(CreateRoleRequest dto)
    {
        return new Role
        {
            RoleType = dto.RoleType
        };
    }

    public RoleResponse ToResponse(Role entity)
    {
        return new RoleResponse
        {
            Id = entity.Id,
            RoleType = entity.RoleType,
         
        };
    }

    public void ToUpdateEntity(Role entity, UpdateRoleRequest dto)
    {
        if (dto.RoleType.HasValue && Enum.IsDefined(typeof(UserRole), dto.RoleType.Value))
        {
            entity.RoleType = dto.RoleType.Value;
        }
    }
}