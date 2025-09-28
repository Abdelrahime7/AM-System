using Application.Common.Models;

using Application.Users.DTOs;

namespace Application.Users.Features.Commands
{
    public partial class UserCommands


    {


        public async Task<Result<bool>> UpdateUserAsync(UpdateUserRequest request)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);
                if (user == null)
                    return Result<bool>.Failure("User Not Found");
                
                else

                   user = _mapper.ToUpdateEntity(request);
                  _userRepository.Update(user);

                   return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"User Updated failed: {ex.Message}");
            }

        }
    }
}
