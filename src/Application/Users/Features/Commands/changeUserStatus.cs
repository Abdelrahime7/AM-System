

using Application.Common.Models;
using Application.Users.DTOs;
using Domain.Enums;

namespace Application.Users.Features.Commands
{
    public partial class UserCommands
    {

        public async Task<Result<bool>> ChangeUserStatusAsync(UpdateUserRequest request,UserStatus status)
        {
            try
            {
                request.Status = status;
                var result = await UpdateUserAsync(request);
                if (result.IsSuccess)
                {
            
                    return result;
                }
                else

                    return Result<bool>.Failure($"failed to {status} User");

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"failed to {status} User: {ex.Message}");
            }


        }
    }
}