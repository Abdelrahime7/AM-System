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
        public async Task DeleteDriverAsync_ShouldReturnSuccess_WhenDriverIsFoundAndDeleted()
        {
            // Arrange
            var driverId = 1;
            var driver = new Driver { Id = driverId };

            _repository.Setup(r => r.GetByIdAsync(driverId))
                .ReturnsAsync(driver);

            _repository.Setup(r => r.Delete(driver));

            // Act
            var result = await _Commands.DeleteDriverAsnc(driverId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            _repository.Verify(r => r.GetByIdAsync(driverId), Times.Once);
            _repository.Verify(r => r.Delete(driver), Times.Once);
        }

        [Fact]
        public async Task DeleteDriverAsync_ShouldReturnFailure_WhenDriverNotFound()
        {
            // Arrange
            var driverId = 99;

            _repository.Setup(r => r.GetByIdAsync(driverId))
                .ReturnsAsync((Driver)null);

            // Act
            var result = await _Commands.DeleteDriverAsnc(driverId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Driver Not Found", result.Error);
            _repository.Verify(r => r.GetByIdAsync(driverId), Times.Once);
            _repository.Verify(r => r.Delete(It.IsAny<Driver>()), Times.Never);
        }
        [Fact]
        public async Task DeleteDriverAsync_ShouldReturnFailure_WhenGetByIdAsyncThrows()
        {
            // Arrange
            var driverId = 1;

            _repository.Setup(r => r.GetByIdAsync(driverId))
                .ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _Commands.DeleteDriverAsnc(driverId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error Deleting Driver", result.Error);
            _repository.Verify(r => r.GetByIdAsync(driverId), Times.Once);
            _repository.Verify(r => r.Delete(It.IsAny<Driver>()), Times.Never);
        }
        [Fact]
        public async Task DeleteDriverAsync_ShouldReturnFailure_WhenDeleteThrows()
        {
            // Arrange
            var driverId = 1;
            var driver = new Driver { Id = driverId };

            _repository.Setup(r => r.GetByIdAsync(driverId))
                .ReturnsAsync(driver);

            _repository.Setup(r => r.Delete(driver))
                .Throws(new Exception("Delete failed"));

            // Act
            var result = await _Commands.DeleteDriverAsnc(driverId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error Deleting Driver", result.Error);
            _repository.Verify(r => r.GetByIdAsync(driverId), Times.Once);
            _repository.Verify(r => r.Delete(driver), Times.Once);
        }


    }
}
