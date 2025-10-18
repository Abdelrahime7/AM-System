
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;


namespace UnitTests.Application.Users.Commands
{
    public  partial class UserCommandsTests
    {
       
        [Fact]
        public async Task ChangeUserStatusAsync_ShouldReturnSuccess_WhenUpdateSucceeds()
        {
            // Arrange
            var request = new UpdateUserRequest
            {
                Id = 1,
                Username = "user1234",
                PasswordHash = "user22334",
                Status = UserStatus.Active
            };

            var existingUser = new User
            {
                Id = request.Id,
                FullName = "Updated Name",
                Email = "updated@example.com",
                Phone = "0511223344",
                Username = "user1234",
                PasswordHash = "qwerty"
            };
            var updatedUser = new User
            {
                Id = request.Id,
                FullName =request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                Username =request.Username,
                Status = (UserStatus)request.Status,
               
                PasswordHash = request.PasswordHash,
            };



            _userRepoMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(existingUser);

            _mapperMock
               .Setup(m => m.ToUpdateEntity(
                  It.Is<User>(u => u != null),
                 It.Is<UpdateUserRequest>(d => d.Status == UserStatus.Active)))
                 .Callback<User, UpdateUserRequest>((user, dto) =>
                  {

                      user.Status = (UserStatus)dto.Status;
       
                  });


            _userRepoMock.Setup(r => r.Update(updatedUser));

            // Act
            var result = await _userCommands.ChangeUserStatusAsync(request, UserStatus.Active);
            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);

        }



        [Fact]
        public async Task ChangeUserStatusAsync_ShouldReturnFailure_WhenUserNotFound()
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
            var result = await _userCommands.ChangeUserStatusAsync(request, UserStatus.Suspended);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("failed to Suspended User", result.Error);
        }


    }
 }