using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Application.Drivers.features.Queries;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DriverInterfaces;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;

using Domain.Entities;
using Moq;

namespace UnitTests.Application.Drivers.Queries
{
   public partial class DriverQueriesTests
    {
        private readonly Mock<IDriverRepository> _repository;
        private readonly Mock<IEntityMapper<Driver, CreateDriverRequest,
            UpdateDriverRequest, DriverResponse>> _mapper;
        private readonly Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest,
             UserResponse>> _Usermapper;
        private readonly IDriverQueries _queries;

        public DriverQueriesTests()
        {
            _repository = new Mock<IDriverRepository>();
            _mapper = new Mock<IEntityMapper<Driver, CreateDriverRequest,
                UpdateDriverRequest, DriverResponse>>();
            _Usermapper = new Mock<IEntityMapper<User, CreateUserRequest,
                UpdateUserRequest, UserResponse>>();
            _queries = new DriverQueries(_repository.Object, _mapper.Object,_Usermapper.Object);

        }
        [Fact]
        public async Task GetAllDrivers_ShouldReturnSuccess_WhenDriversExist()
        {
            // Arrange
            var drivers = new List<Driver>
    {
        new Driver { Id = 1 },
        new Driver { Id = 2 }
    };

            var sessionResponses = new List<DriverSessionResponse>
    {
        new DriverSessionResponse { },
        new DriverSessionResponse { }
    };

            _repository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(drivers);

            // Simulate GetById returning session responses
            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(drivers[0]);
            _repository.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(drivers[1]);

            _mapper.Setup(m => m.ToResponse(drivers[0]))
                .Returns(new DriverResponse
                {
                    IsAvailable = true,
                    IsLocal = true,
                    UserID = 10
                });

            _mapper.Setup(m => m.ToResponse(drivers[1]))
                .Returns(new DriverResponse
                {
                    IsAvailable = false,
                    IsLocal = false,
                    UserID = 20
                });

            _Usermapper.Setup(m => m.ToResponse(It.IsAny<User>()))
                .Returns(new UserResponse {});

            // Act
            var result = await _queries.GetAllDrivers();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count());
        }
        [Fact]
        public async Task GetAllDrivers_ShouldReturnFailure_WhenNoDriversExist()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Driver>());

            // Act
            var result = await _queries.GetAllDrivers();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No Drivers Found", result.Error);
        }
        [Fact]
        public async Task GetAllDrivers_ShouldReturnFailure_WhenRepositoryThrows()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync())
                .ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _queries.GetAllDrivers();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failled to fetch Drivers", result.Error);
        }
      
      


    }
}
