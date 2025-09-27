using Application.Common.Models;

namespace Application.Users.Features.Commands
{
    public partial class UserCommands
    {

        public async Task<Result<bool>> DeleteUserAsync(int ID)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(ID);
                if (user == null)
                    return Result<bool>.Failure("User Not Found");

                else
                    _userRepository.Delete(user);
                     return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"failed to delet user: {ex.Message}");
            }

        }

    }
}
