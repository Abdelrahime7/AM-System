

using Application.Drivers.DTO_s;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Drivers.Queries
{
    partial class DriverQueriesTests
    {
        [Fact]
        public async Task GetById_ShouldReturnSuccess_WhenDriverExists()
        {
            // Arrange
            var driverId = 1;
            var driver = new Driver
            {
                Id = driverId,
                User = new User { Id = 10,
                Username="ahmed123",
                Role=UserRole.Driver,
                PasswordHash="wdwerrqw32",
                Phone="+213755006655",
                FullName="ahmad hadi"
                }
            };

            var driverResponse = new DriverResponse
            {
                IsAvailable = true,
                IsLocal = true,
                UserID = 10
            };

            var userResponse = new UserResponse
            {
               
                FullName = "Test User"
            };

            _repository.Setup(r => r.GetByIdAsync(driverId))
                .ReturnsAsync(driver);

            _mapper.Setup(m => m.ToResponse(driver))
                .Returns(driverResponse);

            _Usermapper.Setup(m => m.ToResponse(driver.User))
                .Returns(userResponse);

            // Act
            var result = await _queries.GetById(driverId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(driverResponse, result.Value.DriverResponse);
            Assert.Equal(userResponse, result.Value.UserResponse);
        }
        [Fact]
        public async Task GetById_ShouldReturnFailure_WhenDriverNotFound()
        {
            // Arrange
            var driverId = 99;

            _repository.Setup(r => r.GetByIdAsync(driverId))
                .ReturnsAsync((Driver)null);

            // Act
            var result = await _queries.GetById(driverId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No driver Found", result.Error);
        }
        [Fact]
        public async Task GetById_ShouldReturnFailure_WhenRepositoryThrows()
        {
            // Arrange
            var driverId = 2;

            _repository.Setup(r => r.GetByIdAsync(driverId))
                .ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _queries.GetById(driverId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetch driver", result.Error);
        }



    }
}
