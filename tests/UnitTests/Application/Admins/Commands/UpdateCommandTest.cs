using Application.Admins.Dto_s;
using Application.Admins.DTO_s.session;
using Application.Common.Models;

using Application.Users.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Admins.Commands
{
    public partial class CommandTests
    {
        

        [Fact]
        public async Task UpdateAdminAsync_ReturnsSuccess_WhenAllStepsSucceed()
        {
            // Arrange
            var admin = new Admin { Id = 1 };
            var request = new UpdateAdminSession
            {
                AdminRequest = new UpdateAdminRequest { Id = 1 },
                UserRequest = new UpdateUserRequest()
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(admin);
            _userCommands.Setup(u => u.UpdateUserAsync(request.UserRequest))
     .ReturnsAsync(Result<bool>.Success(true));
            _mapper.Setup(m => m.ToUpdateEntity(admin, request.AdminRequest));
            _repository.Setup(r => r.Update(admin));

            // Act
            var result = await _Commands.UpdateAdminAsnc(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task UpdateAdminAsync_ReturnsFailure_WhenAdminNotFound()
        {
            // Arrange
            var request = new UpdateAdminSession
            {
                AdminRequest = new UpdateAdminRequest { Id = 1 },
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Admin)null);

            // Act
            var result = await _Commands.UpdateAdminAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Admin not found", result.Error);
        }

        [Fact]
        public async Task UpdateAdminAsync_ReturnsFailure_WhenUserUpdateThrows()
        {
            // Arrange
            var admin = new Admin { Id = 1 };
            var request = new UpdateAdminSession
            {
                AdminRequest = new UpdateAdminRequest { Id = 1 },
                UserRequest = new UpdateUserRequest()
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(admin);
            _userCommands.Setup(u => u.UpdateUserAsync(request.UserRequest)).ThrowsAsync(new Exception("User update failed"));

            // Act
            var result = await _Commands.UpdateAdminAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Admin", result.Error);
        }

        [Fact]
        public async Task UpdateAdminAsync_ReturnsFailure_WhenMapperThrows()
        {
            // Arrange
            var admin = new Admin { Id = 1 };
            var request = new UpdateAdminSession
            {
                AdminRequest = new UpdateAdminRequest { Id = 1 },
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(admin);
            _mapper.Setup(m => m.ToUpdateEntity(admin, request.AdminRequest)).Throws(new Exception("Mapping failed"));

            // Act
            var result = await _Commands.UpdateAdminAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Admin", result.Error);
        }

        [Fact]
        public async Task UpdateAdminAsync_ReturnsFailure_WhenRepositoryUpdateThrows()
        {
            // Arrange
            var admin = new Admin { Id = 1 };
            var request = new UpdateAdminSession
            {
                AdminRequest = new UpdateAdminRequest { Id = 1 },
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(admin);
            _mapper.Setup(m => m.ToUpdateEntity(admin, request.AdminRequest));
            _repository.Setup(r => r.Update(admin)).Throws(new Exception("DB error"));

            // Act
            var result = await _Commands.UpdateAdminAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Admin", result.Error);
        }
    }

}
