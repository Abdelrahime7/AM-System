using Application.Common.Models;

namespace Application.Roles.Features.Commands;

public partial class RoleCommands
{
    public async Task<Result<bool>> DeleteRoleAsync(int id)
    {
        try
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null)
                return Result<bool>.Failure("Role not found");

            // Check if role has users assigned
            if (role.Users.Any())
                return Result<bool>.Failure("Cannot delete role that has users assigned");

            _repository.Delete(role);
            return Result<bool>.Success(true);
        }
        catch (Exception e)
        {
            return Result<bool>.Failure($"Error deleting role: {e.Message}");
        }
    }
}