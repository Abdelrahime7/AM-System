using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;

namespace Application.Users.Features.Commands
{
    public class CreatUserCommand

    {
        private readonly IEntityMapper<User, CreateUserRequest,UpdateUserRequest,UserResponse> _mapper;

        private readonly IUserRepository _userRepository;
        public CreatUserCommand( IUserRepository userRepository,
            IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse> mapper)
        {
            _userRepository= userRepository;
            _mapper= mapper;
        }
        public async Task<int> CreatUser(CreateUserRequest request)
        {

            var User = _mapper.ToEntity(request);
            
            await _userRepository.AddAsync(User);
            return User.Id;

        }
    }
}
