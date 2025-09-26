

using Application.Common.Models;
using Application.Users.DTOs;


namespace Application.Users.Features.Queries
{
   public partial  class UsersQueries
    {

        public async Task<Result<IEnumerable<UserResponse>>> GetAllUsersAsync() 
        {
            try
            {
                var Users = await _userRepository.GetAllAsync();
                if (!Users.Any())
                    return Result<IEnumerable<UserResponse>>.Failure("No users found.");

                var Responses = Users.ToList().Select(U => _mapper.ToResponse(U));


                return Result<IEnumerable<UserResponse>>.Success(Responses);

            }

            catch (Exception ex)
            {
                return Result<IEnumerable<UserResponse>>.Failure($"failed to fetche users: {ex.Message}");
            }
        }

    }
}
