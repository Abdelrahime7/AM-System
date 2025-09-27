

using Application.Common.Models;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.UserInterfaces
{
    public interface IUserQueries
    {
        Task<Result<IEnumerable<UserResponse>>> GetAllUsersAsync();
        Task<Result<UserResponse>> GetUserByIDAsync(int id);
        Task<Result<UserStatus>> GetUserStatusById(int id);
        Task<Result<UserRole>> GetUserRoleById(int id);

    }
}
