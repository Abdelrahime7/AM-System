

using Application.Common.Models;
using Application.Users.DTOs;

namespace Application.Users.Features.Commands
{
    public partial class UserCommands
    {

        public async Task<Result<bool>> ChangeUserStatusAsync(ChangeStatusRequest request)
        {
            try
            {
                var User =  await _userRepository.GetByIdAsync(request.userID);
                if (User == null)
                return Result<bool>.Failure("no driver ");

                User.Status = request.status;

                   _userRepository.Update(User);

                    return Result<bool>.Success(true);

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"failed to {request.status} User: {ex.Message}");
            }


        }
    }
}