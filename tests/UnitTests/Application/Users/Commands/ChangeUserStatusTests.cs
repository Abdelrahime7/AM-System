
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
                FullName = "Updated Name",
                Email = "updated@example.com",
                Phone = "0511223344",
                PasswordHash = "user22334",
                Status = UserStatus.Active
            };

            var existingUser = new User
            {
                Id = request.Id,
                Email = "old@example.com",
                FullName = "john doe",
                Phone = "0611223344",
                PasswordHash = "qwerty"
            };
            var updatedUser = new User
            {
                Id = request.Id,
                Email = request.Email,
                FullName = request.FullName,
                Status = (UserStatus)request.Status,
                Phone = request.Phone,
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
                FullName = "Updated Name",
                Email = "updated@example.com",
                Phone = "0511223344",
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