using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Application.Users.DTOs;
using Domain.Entities;

namespace Application.Users.Features.Queries
{
    partial class UsersQueries(IUserRepository Userrepository,
          IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse> mapper):IUserQueries
    {


        private readonly IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse> _mapper = mapper;
        private readonly IUserRepository _userRepository = Userrepository;


        public async Task<Result<UserResponse>> GetUserByIDAsync(int ID)
        {
            try
            {
                var User = await _userRepository.GetByIdAsync(ID);
                if (User == null)
                    return Result<UserResponse>.Failure("User Not found ");
                else
                {
                    var UserRespons = _mapper.ToResponse(User);

                    return Result<UserResponse>.Success(UserRespons);
                }

            }

            catch (Exception ex)
            {
                return Result<UserResponse>.Failure($"failed to fetche user: {ex.Message}");
            }
        }

    }
}