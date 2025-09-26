using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Application.Users.DTOs;
using Domain.Entities;

namespace Application.Users.Features.Commands
{
    public partial class UserCommands(IUserRepository userRepository,
        IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse> mapper):IUserCommands


    {
        private readonly IEntityMapper<User, CreateUserRequest,UpdateUserRequest,UserResponse> _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<int>> CreatUserAsync(CreateUserRequest request)
        {
            try
            {
                var user = _mapper.ToEntity(request);
                await _userRepository.AddAsync(user);

                return Result<int>.Success(user.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"Failed to create user: {ex.Message}");
            }

        }
    }
}
