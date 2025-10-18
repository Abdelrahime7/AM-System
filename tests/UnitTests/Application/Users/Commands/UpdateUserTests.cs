

using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Users.Commands
{

    public partial class UserCommandsTests
    {
        [Fact]
        public async Task UpdateUserAsync_ShouldReturnSuccess_WhenUserIsUpdated()
        {
            // Arrange
            var request = new UpdateUserRequest
            {
                Id = 1,
              
                PasswordHash = "user22334",
                Status = UserStatus.Active
            };

            var user = new User
            {
                Id = 1,
             Username="1132user",
                Email = "old@example.com",
                FullName = "john doe",
                Phone = "0611223344",
                PasswordHash = "qwerty"
            };
            
            _userRepoMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.ToUpdateEntity(user, request)).Equals(user);
            _userRepoMock.Setup(r => r.Update(user));

            // Act
            var result = await _userCommands.UpdateUserAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            _userRepoMock.Verify(r => r.GetByIdAsync(request.Id), Times.Once);
            _mapperMock.Verify(m => m.ToUpdateEntity(user, request), Times.Once);
            _userRepoMock.Verify(r => r.Update(user), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var request = new UpdateUserRequest
            {
                Id = 999,
              
                PasswordHash = "user22334",
                Status = UserStatus.Active
            };

            _userRepoMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((User)null);

            // Act
            var result = await _userCommands.UpdateUserAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User Not Found", result.Error);
            _userRepoMock.Verify(r => r.GetByIdAsync(request.Id), Times.Once);
            _mapperMock.Verify(m => m.ToUpdateEntity(
                                                    It.IsAny<User>(),
                                                    It.IsAny<UpdateUserRequest>()), Times.Never);

            _userRepoMock.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new UpdateUserRequest
            {
                Id = 1,
               
                PasswordHash = "user22334",
                Status = UserStatus.Active
            };
            var user = new User
            {
                Id = request.Id,
              Username  ="22usernae",
                Email = "old@example.com",
                FullName = "john doe",
                Phone = "0611223344",
                PasswordHash = "qwerty"
            };

            _userRepoMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.ToUpdateEntity(user,request)).Equals(user);
            _userRepoMock.Setup(r => r.Update(user)).Throws(new Exception("DB error"));

            // Act
            var result = await _userCommands.UpdateUserAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("User Updated failed", result.Error);
        }
    }
}


