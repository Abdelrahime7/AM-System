
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Users.Queries
{
    public partial class UserQueriesTests
    {
       
          [Fact]
          public async Task GetUserRoleById_ShouldReturnSuccess_WhenUserExists()
            {
            // Arrange
               var user = new User
               {

                Id = 1,
                FullName = "ohne doe",
                PasswordHash="wrweqweq",
                Email = "user@ex.com",
                Phone = "0122334455",
                RoleId = (int)UserRole.Admin
               };
                var userResponse = new UserResponse
                {
                    Id=1,
                    FullName="ohne doe",
                    Email="user@ex.com",
                    Phone="0122334455",
                    RoleId = (int)UserRole.Admin
                };

            _userRepoMock.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.ToResponse(user)).Returns(userResponse);

            // Act
            var result = await _usersQueries.GetUserRoleById(user.Id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(UserRole.Admin, result.Value);

         }

        [Fact]
        public async Task GetUserRoleById_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var userId = 999;
            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _usersQueries.GetUserRoleById(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to get UserRole ", result.Error);
        }


      


    }
}