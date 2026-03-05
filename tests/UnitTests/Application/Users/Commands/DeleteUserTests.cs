
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Users.Commands
{
    public partial class UserCommandsTests
    {
      
        [Fact]
        public async Task DeleteUserAsync_ShouldReturnSuccess_WhenUserIsDelete()
        {
            // Arrange
            var userId = 1;
            var  existingUser = new User
            {
                Role = UserRole.Admin,

                Id = userId,
                Username="user11",
                Email = "old@example.com",
                FullName = "john doe",
                Phone = "0611223344",
                PasswordHash = "qwerty"
            };

            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _userRepoMock.Setup(r => r.Delete(existingUser));

            // Act
            var result = await _userCommands.DeleteUserAsync(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            _userRepoMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
            _userRepoMock.Verify(r => r.Delete(existingUser), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var userId = 999;

            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userCommands.DeleteUserAsync(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User Not Found", result.Error);
            _userRepoMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
            _userRepoMock.Verify(r => r.Delete(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var userId = 1;
            var UsertoDelete = new User
            {
                Role = UserRole.Admin,

                Id = userId,
                Email = "old@example.com",
                FullName = "john doe",
                Phone = "0611223344",
                PasswordHash = "qwerty",
                Username = "john doe",
             
            };

            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(UsertoDelete);
            _userRepoMock.Setup(r => r.Delete(UsertoDelete)).Throws(new Exception("DB error"));

            // Act
            var result = await _userCommands.DeleteUserAsync(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to delet user", result.Error);
        }

    }
}
