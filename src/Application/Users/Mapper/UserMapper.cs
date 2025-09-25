using Application.Interfaces.Common.Mappers;
using Application.Users.DTOs;
using Domain.Entities;

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

        public User ToUpdateEntity(UpdateUserRequest dto)
        {
            return new User
            {
                Id = dto.Id,
                FullName = dto.FullName,
                PasswordHash = dto.PasswordHash,
                Email = dto.Email,
                Phone = dto.Phone,
                CcpNumber = dto.CcpNumber,
                LastLoginAt = dto.LastLoginAt,
                RoleId = dto.RoleId,
                Status = dto.Status
            };
        }

      
    }
}
