

using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Application.Users.Features.Commands;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Users.Commands
{

    public partial class UserCommandsTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>> _mapperMock;
        private readonly UserCommands _userCommands;

        public UserCommandsTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>>();
            _userCommands = new UserCommands(_userRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreatUserAsync_ShouldReturnSuccess_WhenUserIsCreated()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                FullName="john doe",
                Email = "test@example.com",
                PasswordHash = "StrongPass123!",
                Phone = "0540112233",
                CcpNumber="12345678-10",
                Status = UserStatus.Pending,
                RoleId= 1
            };

            var user = new User {
                Id=42,
                FullName = request.FullName,
                PasswordHash=request.PasswordHash,
                Email = request.Email,
                Phone=request.Phone,
                CcpNumber=request.CcpNumber,
                Status = request.Status };

            _mapperMock.Setup(m => m.ToEntity(request)).Returns(user);
            _userRepoMock.Setup(r => r.AddAsync(user)).Returns(Task.CompletedTask);

            // Act
            var result = await _userCommands.CreatUserAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(42, result.Value);
            _mapperMock.Verify(m => m.ToEntity(request), Times.Once);
            _userRepoMock.Verify(r => r.AddAsync(user), Times.Once);
        }

        [Fact]
        public async Task CreatUserAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new CreateUserRequest {
                FullName= "qwqq",
                Phone= "0000000000",
                PasswordHash= "dummy",
                Email = "fail@example.com" };
            var user = new User
            {
                FullName=request.FullName,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                Status = UserStatus.Pending,
                Phone = request.Phone,
                RoleId = 1
            };

            _mapperMock.Setup(m => m.ToEntity(request)).Returns(user);
            _userRepoMock.Setup(r => r.AddAsync(user)).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _userCommands.CreatUserAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to create user", result.Error);
        }


    }

}
