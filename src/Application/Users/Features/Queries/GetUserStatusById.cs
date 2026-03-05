

using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Users.Features.Queries
{
   public partial class UsersQueries
        
    {
        public async Task<Result<UserStatus>> GetUserStatusById(int UserID)
        {
            try
            {
                var result = await GetUserByIDAsync(UserID);
                if (result.IsSuccess)
                {
                    return Result<UserStatus>.Success(result.Value!.Status);
                }
                return
                    Result<UserStatus>.Failure("Failed to get UserStatus ");
            }
            catch (Exception ex) 
            {
                return
                   Result<UserStatus>.Failure($"Failed to get UserStatus : {ex.Message} ");
            }
           
        }

        
    }
}
