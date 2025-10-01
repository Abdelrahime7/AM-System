

using Application.Common.Models;
using Application.Roles.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.RoleInterfaces
{
    public interface IRoleQueries
    {
        Task<Result<IEnumerable<TokenResponse>>> GetAllRolesAsync();
        Task<Result<TokenResponse>> GetRoleByIDAsync(int id);
      

    }
}
