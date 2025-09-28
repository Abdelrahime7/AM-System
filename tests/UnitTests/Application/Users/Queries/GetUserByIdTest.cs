

using Application.Users.DTOs;
using Domain.Entities;
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
                Id = userId,
                FullName = "Jane Doe",
                PasswordHash = "wewedw",
                Email = "jane@example.com",
                Phone = "0123456789"
            };

            var response = new UserResponse
            {
                Id = userId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone
            };

            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.ToResponse(user)).Returns(response);

            // Act
            var result = await _usersQueries.GetUserByIDAsync(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(userId, result.Value.Id);
            Assert.Equal("jane@example.com", result.Value.Email);
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
