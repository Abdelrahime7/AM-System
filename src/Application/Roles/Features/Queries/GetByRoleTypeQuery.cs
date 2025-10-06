using Application.Common.Models;
using Application.Roles.DTOs;
using Domain.Enums;

namespace Application.Roles.Features.Queries;

public partial class RoleQueries
{
    public async Task<Result<RoleResponse>> GetByRoleTypeAsync(UserRole roleType)
    {
        try
        {
            var role = await _repository.GetByRoleTypeAsync(roleType);
            if (role == null)
                return Result<RoleResponse>.Failure($"Role type '{roleType}' not found");

            var response = _mapper.ToResponse(role);
            return Result<RoleResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<RoleResponse>.Failure($"Failed to fetch role: {ex.Message}");
        }
    }
}