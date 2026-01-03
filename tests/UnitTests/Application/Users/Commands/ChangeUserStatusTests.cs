
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
            var request = new ChangeStatusRequest
            {
                userID = 1,
              
                status = UserStatus.Active
            };

            var existingUser = new User
            {
                Role = UserRole.Admin,

                Id = request.userID,
                FullName = "Updated Name",
                Email = "updated@example.com",
                Phone = "0511223344",
                Username = "user1234",
                PasswordHash = "qwerty"
            };
            var updatedUser = new User
            {
                Id = request.userID,
                Role = UserRole.Admin,

                FullName = existingUser.FullName,
                Email = existingUser.Email,
                Phone = existingUser.Phone,
                Username = existingUser.Username,
                Status = (UserStatus)request.status,
               
                PasswordHash = existingUser.PasswordHash,
            };



            _userRepoMock.Setup(r => r.GetByIdAsync(request.userID)).ReturnsAsync(existingUser);

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
            var result = await _userCommands.ChangeUserStatusAsync(request);
            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);

        }



        [Fact]
        public async Task ChangeUserStatusAsync_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var request = new ChangeStatusRequest
            {
                userID = 999,

               
                status = UserStatus.Active
            };
            _userRepoMock.Setup(r => r.GetByIdAsync(request.userID)).ReturnsAsync((User)null);

            // Act
            var result = await _userCommands.ChangeUserStatusAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("failed to Suspended User", result.Error);
        }


    }
 }