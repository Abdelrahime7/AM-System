

using Application.Common.Models;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.UserInterfaces
{
    public interface IUserCommands
    {
        Task<Result<int>> CreatUserAsync(CreateUserRequest request);
        Task<Result<bool>> DeleteUserAsync(int ID);
        Task<Result<bool>> UpdateUserAsync(UpdateUserRequest request);
        Task<Result<bool>> ChangeUserStatusAsync(UpdateUserRequest request,UserStatus status);

    }
}
