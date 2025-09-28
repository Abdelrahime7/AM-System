using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Users.Features.Queries
{
   public partial class UsersQueries
    {
       public async  Task<Result<UserRole>> GetUserRoleById(int id)
        {
            try
            {
                var result = await GetUserByIDAsync(id);
                if (result.IsSuccess)
                {
                    return Result<UserRole>.Success((UserRole)result.Value!.RoleId!);
                }
                return
                    Result<UserRole>.Failure("Failed to get UserRole ");
            }
            catch (Exception ex)
            {
                return
                   Result<UserRole>.Failure($"Failed to get UserRole : {ex.Message} ");
            }

        }
    }
}
