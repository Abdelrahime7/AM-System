
using Application.Interfaces.Common.Mappers;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Mapper
{
    public class UserMapper : IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>
    {
        public User ToEntity(CreateUserRequest dto)
        {
            var PasswordHasher = new PasswordHasher<object>();

            return new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = PasswordHasher.HashPassword(null!, dto.PasswordHash),
                CcpNumber = dto.CcpNumber,
                Status = dto.Status,
                RoleId = dto.RoleId,
            };
        }

       

        public UserResponse ToResponse(User entity)
        {
            return new UserResponse
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Email = entity.Email,
                Phone = entity.Phone,
                LastLoginAt = entity.LastLoginAt,
                RoleId = entity.RoleId,
                Status= entity.Status,
            };
        }

      
        public void ToUpdateEntity(User user, UpdateUserRequest dto)
        {

            user.Id = dto.Id;
            user.FullName     = dto.FullName     ?? user.FullName;
            user.PasswordHash = dto.PasswordHash ?? user.PasswordHash;
            user.Email        = dto.Email        ?? user.Email;
            user.Phone        = dto.Phone        ?? user.Phone;
            user.CcpNumber    = dto.CcpNumber    ?? user.CcpNumber;
            user.LastLoginAt  = dto.LastLoginAt  ?? user.LastLoginAt;
            user.RoleId       = dto.RoleId       ?? user.RoleId;

            if (dto.Status.HasValue && Enum.IsDefined(typeof(UserStatus), dto.Status.Value))
            {
                user.Status = (UserStatus)dto.Status.Value;
            }

        }
    }
}
