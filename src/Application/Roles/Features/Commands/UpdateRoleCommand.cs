using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Roles.DTOs;

namespace Application.Roles.Features.Commands;

public partial class RoleCommands
{
    public async Task<Result<bool>> UpdateRoleAsync(UpdateRoleRequest request)
    {
        try
        {
            var role = await _repository.GetByIdAsync(request.Id);
            if (role == null)
                return Result<bool>.Failure("Role not found");

            // Check if new role type already exists (if changing role type)
            if (request.RoleType.HasValue && request.RoleType.Value != role.RoleType)
            {
                var existingRole = await _repository.GetByRoleTypeAsync(request.RoleType.Value);
                if (existingRole != null)
                    return Result<bool>.Failure($"Role type '{request.RoleType}' already exists");
            }

            _mapper.ToUpdateEntity(role, request);
            _repository.Update(role);
            return Result<bool>.Success(true);
        }
        catch (Exception e)
        {
            return Result<bool>.Failure($"Failed to update role: {e.Message}");
        }
    }
}
