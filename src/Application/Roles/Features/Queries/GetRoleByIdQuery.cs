using Application.Common.Models;
using Application.Roles.DTOs;

namespace Application.Roles.Features.Queries;

public partial class RoleQueries
{
    public async Task<Result<RoleResponse>> GetRoleByIdAsync(int id)
    {
        try
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null)
                return Result<RoleResponse>.Failure("Role not found");

            var response = _mapper.ToResponse(role);
            return Result<RoleResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<RoleResponse>.Failure($"Failed to fetch role: {ex.Message}");
        }
    }
}