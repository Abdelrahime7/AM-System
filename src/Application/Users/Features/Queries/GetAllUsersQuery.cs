

using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;

namespace Application.Users.Features.Queries
{
    public class GetAllUsersQuery(IUserRepository Userrepository ,
          IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse> mapper)
    {
        private readonly IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse> _mapper = mapper;
        private readonly IUserRepository _userRepository = Userrepository;

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
