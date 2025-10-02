

using Application.Common.Models;
using Application.Roles.DTOs;


namespace Application.Interfaces.RoleInterfaces
{
    public interface IRoleQueries
    {
        Task<Result<IEnumerable<RoleResponse>>> GetAllRolesAsync();
        Task<Result<RoleResponse>> GetRoleByIDAsync(int id);
      

    }
}
