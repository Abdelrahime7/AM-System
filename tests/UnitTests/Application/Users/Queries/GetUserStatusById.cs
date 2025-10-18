
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;


namespace UnitTests.Application.Users.Queries
{
    public partial class UserQueriesTests
    {


        [Fact]
        public async Task GetUserStatusById_ShouldReturnSuccess_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User
            {

                Id = userId,
                PasswordHash="ewrwr-rwr-",
                Username="john smithe",
                Email = "old@example.com",
                FullName = "john doe",
                Phone = "0611223344",

                Status = UserStatus.Active
            };

            var response = new UserResponse
            {
                Status = user.Status,
               

            };

            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.ToResponse(user)).Returns(response);

            // Act
            var result = await _usersQueries.GetUserStatusById(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(UserStatus.Active, result.Value);
        }

        [Fact]
        public async Task GetUserStatusById_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var userId = 999;
            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _usersQueries.GetUserStatusById(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to get UserStatus ", result.Error);
        }

    }

}
