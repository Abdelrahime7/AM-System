using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;

namespace Application.Users.Features.Commands
{
    public class UpdatUserCommand(IUserRepository userRepository,
         IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse> mapper)
    {


        private readonly IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse> _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;

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
