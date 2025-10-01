using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.RoleInterfaces;
using Application.Roles.DTOs;

namespace Application.Roles.Features.Queries;

public partial class RoleQueries : IRoleQueries
{
    public Task<Result<IEnumerable<RoleResponse>>> GetAllRolesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<RoleResponse>> GetRoleByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}