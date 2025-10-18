
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
               Username = dto.UserName,
                PasswordHash = PasswordHasher.HashPassword(null!, dto.PasswordHash),
               
                Status = dto.Status,
               
            };
        }

       

        public UserResponse ToResponse(User entity)
        {
            return new UserResponse
            {


                Username = entity.Username,
                LastLoginAt = entity.LastLoginAt,

                Status = entity.Status,
            };
        }

      
        public void ToUpdateEntity(User user, UpdateUserRequest dto)
        {

            user.Id = dto.Id;
            user.Username     = dto.Username     ?? user.Username;
            user.PasswordHash = dto.PasswordHash ?? user.PasswordHash;
            user.LastLoginAt  = dto.LastLoginAt  ?? user.LastLoginAt;

            if (dto.Status.HasValue && Enum.IsDefined(typeof(UserStatus), dto.Status.Value))
            {
                user.Status = (UserStatus)dto.Status.Value;
            }

        }
    }
}
