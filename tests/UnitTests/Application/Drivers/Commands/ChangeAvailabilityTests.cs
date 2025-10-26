using Application.Drivers.DTO_s;
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
        public async Task ChangeDriverAvaillability_ShouldReturnSuccess_WhenDriverExists()
        {
            // Arrange
            var availability = new ChangeAvailability
            {
                DriverID = 1,
                Availability = true
            };
            var driver = new Driver { Id = 1, IsAvailable = false };

            _repository.Setup(r => r.GetByIdAsync(availability.DriverID))
                .ReturnsAsync(driver);

            _repository.Setup(r => r.Update(driver));

            // Act
            var result = await _Commands.ChangeDriverAvaillability(availability);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            Assert.True(driver.IsAvailable);
            _repository.Verify(r => r.GetByIdAsync(availability.DriverID), Times.Once);
            _repository.Verify(r => r.Update(driver), Times.Once);
        }
        [Fact]
        public async Task ChangeDriverAvaillability_ShouldReturnFailure_WhenDriverNotFound()
        {
            // Arrange
            var availability = new ChangeAvailability
            {
                DriverID = 99,
                Availability = false
            };

            _repository.Setup(r => r.GetByIdAsync(availability.DriverID))
                .ReturnsAsync((Driver)null);

            // Act
            var result = await _Commands.ChangeDriverAvaillability(availability);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("no driver ", result.Error);
            _repository.Verify(r => r.GetByIdAsync(availability.DriverID), Times.Once);
            _repository.Verify(r => r.Update(It.IsAny<Driver>()), Times.Never);
        }
        [Fact]
        public async Task ChangeDriverAvaillability_ShouldReturnFailure_WhenGetByIdAsyncThrows()
        {
            // Arrange
            var availability = new ChangeAvailability
            {
                DriverID = 2,
                Availability = true
            };

            _repository.Setup(r => r.GetByIdAsync(availability.DriverID))
                .ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _Commands.ChangeDriverAvaillability(availability);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to Change Driver Availlability", result.Error);
            _repository.Verify(r => r.GetByIdAsync(availability.DriverID), Times.Once);
            _repository.Verify(r => r.Update(It.IsAny<Driver>()), Times.Never);
        }
        [Fact]
        public async Task ChangeDriverAvaillability_ShouldReturnFailure_WhenUpdateThrows()
        {
            // Arrange
            var availability = new ChangeAvailability
            {
                DriverID = 3,
                Availability = false
            };
            var driver = new Driver { Id = 3, IsAvailable = true };

            _repository.Setup(r => r.GetByIdAsync(availability.DriverID))
                .ReturnsAsync(driver);

            _repository.Setup(r => r.Update(driver))
                .Throws(new Exception("Update failed"));

            // Act
            var result = await _Commands.ChangeDriverAvaillability(availability);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to Change Driver Availlability", result.Error);
            _repository.Verify(r => r.GetByIdAsync(availability.DriverID), Times.Once);
            _repository.Verify(r => r.Update(driver), Times.Once);
        }



    }
}
