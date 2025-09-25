using Application.Interfaces;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Mapper
{
    public class UserMapper : IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>
    {
        public User ToEntity (CreateUserRequest dto)
        {
            var PasswordHasher = new PasswordHasher<object>();

            return new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = PasswordHasher.HashPassword(null!,dto.PasswordHash),
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

      
    }
}
