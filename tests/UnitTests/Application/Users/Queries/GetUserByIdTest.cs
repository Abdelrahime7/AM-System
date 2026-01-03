

using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Users.Queries
{
    public partial class UserQueriesTests
    {


        [Fact]
        public async Task GetUserByIDAsync_ShouldReturnSuccess_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User
            {
                Role = UserRole.Admin,
                Id = userId,
                Username = "Jane Doe",
                Email = "old@example.com",
                FullName = "john doe",
                Phone = "0611223344",
                PasswordHash="www"
            };

            var response = new UserResponse
            {
                Id = userId,
            
            };

            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.ToResponse(user)).Returns(response);

            // Act
            var result = await _usersQueries.GetUserByIDAsync(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(userId, result.Value.Id);
          
        }

        [Fact]
        public async Task GetUserByIDAsync_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var userId = 999;
            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _usersQueries.GetUserByIDAsync(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User Not found ", result.Error);
        }

        [Fact]
        public async Task GetUserByIDAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var userId = 1;
            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _usersQueries.GetUserByIDAsync(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetche user", result.Error);
            Assert.Contains("DB error", result.Error);
        }



    }
}
