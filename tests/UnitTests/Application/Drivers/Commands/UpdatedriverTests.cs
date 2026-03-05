using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Application.Users.DTOs;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.Drivers.Commands
{
    partial class DriverCommandsTests
    {
        [Fact]
        public async Task UpdateDriverAsync_ShouldReturnSuccess_WhenDriverAndUserAreUpdated()
        {
            var driverRequest = new UpdateDriverRequest { Id = 1 };
            var userRequest = new UpdateUserRequest { Id = 10 };
            var driver = new Driver { Id = 1 };

            var session = new UpdateDriverSession
            {
                DriverRequest = driverRequest,
                UserRequest = userRequest
            };

            _repository.Setup(r => r.GetByIdAsync(driverRequest.Id))
                .ReturnsAsync(driver);

            _userCommands.Setup(u => u.UpdateUserAsync(userRequest))
    .ReturnsAsync(Result<bool>.Success(true));
            _mockMapper.Setup(m => m.ToUpdateEntity(driver, driverRequest));

            _repository.Setup(r => r.Update(driver));

            var result = await _Commands.UpdateDriverAsnc(session);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            _repository.Verify(r => r.GetByIdAsync(driverRequest.Id), Times.Once);
            _userCommands.Verify(u => u.UpdateUserAsync(userRequest), Times.Once);
            _mockMapper.Verify(m => m.ToUpdateEntity(driver, driverRequest), Times.Once);
            _repository.Verify(r => r.Update(driver), Times.Once);
        }
        [Fact]
        public async Task UpdateDriverAsync_ShouldReturnSuccess_WhenDriverUpdatedWithoutUserRequest()
        {
            var driverRequest = new UpdateDriverRequest { Id = 2 };
            var driver = new Driver { Id = 2 };

            var session = new UpdateDriverSession
            {
                DriverRequest = driverRequest,
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(driverRequest.Id))
                .ReturnsAsync(driver);

            _mockMapper.Setup(m => m.ToUpdateEntity(driver, driverRequest));

            _repository.Setup(r => r.Update(driver));

            var result = await _Commands.UpdateDriverAsnc(session);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            _userCommands.Verify(u => u.UpdateUserAsync(It.IsAny<UpdateUserRequest>()), Times.Never);
            _mockMapper.Verify(m => m.ToUpdateEntity(driver, driverRequest), Times.Once);
            _repository.Verify(r => r.Update(driver), Times.Once);
        }
        [Fact]
        public async Task UpdateDriverAsync_ShouldReturnFailure_WhenDriverNotFound()
        {
            var driverRequest = new UpdateDriverRequest { Id = 3 };

            var session = new UpdateDriverSession
            {
                DriverRequest = driverRequest,
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(driverRequest.Id))
                .ReturnsAsync((Driver)null);

            var result = await _Commands.UpdateDriverAsnc(session);

            Assert.False(result.IsSuccess);
            Assert.Equal("Driver not found", result.Error);
            _repository.Verify(r => r.GetByIdAsync(driverRequest.Id), Times.Once);
            _repository.Verify(r => r.Update(It.IsAny<Driver>()), Times.Never);
        }
        [Fact]
        public async Task UpdateDriverAsync_ShouldReturnFailure_WhenGetByIdAsyncThrows()
        {
            var driverRequest = new UpdateDriverRequest { Id = 4 };

            var session = new UpdateDriverSession
            {
                DriverRequest = driverRequest,
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(driverRequest.Id))
                .ThrowsAsync(new Exception("DB error"));

            var result = await _Commands.UpdateDriverAsnc(session);

            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Driver", result.Error);
        }
        [Fact]
        public async Task UpdateDriverAsync_ShouldReturnFailure_WhenUpdateUserAsyncThrows()
        {
            var driverRequest = new UpdateDriverRequest { Id = 5 };
            var userRequest = new UpdateUserRequest { Id = 20 };
            var driver = new Driver { Id = 5 };

            var session = new UpdateDriverSession
            {
                DriverRequest = driverRequest,
                UserRequest = userRequest
            };

            _repository.Setup(r => r.GetByIdAsync(driverRequest.Id))
                .ReturnsAsync(driver);

            _userCommands.Setup(u => u.UpdateUserAsync(userRequest))
                .ThrowsAsync(new Exception("User update failed"));

            var result = await _Commands.UpdateDriverAsnc(session);

            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Driver", result.Error);
        }

        [Fact]
        public async Task UpdateDriverAsync_ShouldReturnFailure_WhenRepositoryUpdateThrows()
        {
            var driverRequest = new UpdateDriverRequest { Id = 6 };
            var driver = new Driver { Id = 6 };

            var session = new UpdateDriverSession
            {
                DriverRequest = driverRequest,
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(driverRequest.Id))
                .ReturnsAsync(driver);

            _mockMapper.Setup(m => m.ToUpdateEntity(driver, driverRequest));

            _repository.Setup(r => r.Update(driver))
                .Throws(new Exception("Update failed"));

            var result = await _Commands.UpdateDriverAsnc(session);

            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Driver", result.Error);
        }


    }
}
