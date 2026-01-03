using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Application.Drivers.features.Commands;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DriverInterfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;


namespace UnitTests.Application.Drivers.Commands
{
   public partial class DriverCommandsTests 
    {
        private readonly Mock<IDriverRepository> _repository;
        private readonly Mock<IUserCommands> _userCommands;
        private readonly Mock<IEntityMapper<Driver, CreateDriverRequest,
       UpdateDriverRequest, DriverResponse>> _mockMapper;

        private IDriverCommands _Commands;
        public DriverCommandsTests()
        {
            _repository= new Mock<IDriverRepository>();
            _userCommands= new Mock<IUserCommands>();
            _mockMapper = new Mock<IEntityMapper<Driver, CreateDriverRequest,
                UpdateDriverRequest, DriverResponse>>();
            _Commands = new DriverCommands(_repository.Object, _userCommands.Object, _mockMapper.Object);

        }
        [Fact]
        public async Task CreateDriverAsync_ShouldReturnSuccess_WhenAllStepsSucceed()
        {
            var userRequest = new CreateUserRequest {
                Role = UserRole.Driver,
                UserName ="ahed 123",
                FullName="ahmed sobhi",
                PasswordHash="weeqweqw312wqq",
                Phone="+21356677899"
            };
            var driverRequest = new CreateDriverRequest { };
            var user = new User { Id = 1,
                Role = userRequest.Role,
                FullName = userRequest.FullName,
                PasswordHash = userRequest.PasswordHash,
                Phone= userRequest.Phone,
                Username= userRequest.UserName
            };
            var driver = new Driver { Id = 42 };

            var session = new CreatDriverSession
            {
                UserRequest = userRequest,
                DriverRequest = driverRequest
            };

            _userCommands.Setup(x => x.CreatUserAsync(userRequest))
                .ReturnsAsync(Result<User>.Success(user));

            _mockMapper.Setup(x => x.ToEntity(driverRequest))
                .Returns(driver);

            _repository.Setup(x => x.AddAsync(driver))
                .Returns(Task.CompletedTask);

            var result = await _Commands.CreateDriverAsync(session);

            Assert.True(result.IsSuccess);
            Assert.Equal(driver.Id, result.Value);
        }

        [Fact]
        public async Task CreateDriverAsync_ShouldReturnFailure_WhenUserCreationFails()
        {
            var userRequest = new CreateUserRequest
            {
                Role = UserRole.Driver,
                UserName = "ahed 123",
                FullName = "ahmed sobhi",
                PasswordHash = "weeqweqw312wqq",
                Phone = "+21356677899"
            };
            var driverRequest = new CreateDriverRequest { };

            var session = new CreatDriverSession
            {
                UserRequest = userRequest,
                DriverRequest = driverRequest
            };

            _userCommands.Setup(x => x.CreatUserAsync(userRequest))
                .ReturnsAsync(Result<User>.Failure("User creation failed"));

            var result = await _Commands.CreateDriverAsync(session);

            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to add Driver", result.Error);
        }
        [Fact]
        public async Task CreateDriverAsync_ShouldReturnFailure_WhenUserCreationThrows()
        {
            var userRequest = new CreateUserRequest
            {
                Role = UserRole.Driver,
                UserName = "ahed 123",
                FullName = "ahmed sobhi",
                PasswordHash = "weeqweqw312wqq",
                Phone = "+21356677899"
            }; var driverRequest = new CreateDriverRequest { };

            var session = new CreatDriverSession
            {
                UserRequest = userRequest,
                DriverRequest = driverRequest
            };

            _userCommands.Setup(x => x.CreatUserAsync(userRequest))
                .ThrowsAsync(new Exception("Unexpected error"));

            var result = await _Commands.CreateDriverAsync(session);

            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to add Driver", result.Error);
        }
        [Fact]
        public async Task CreateDriverAsync_ShouldReturnFailure_WhenAddAsyncThrows()
        {
            var userRequest = new CreateUserRequest
            {
                Role = UserRole.Driver,
                UserName = "ahed 123",
                FullName = "ahmed sobhi",
                PasswordHash = "weeqweqw312wqq",
                Phone = "+21356677899"
            };
            var driverRequest = new CreateDriverRequest { };
            var user = new User
            {

                Id = 1,
                Role= userRequest.Role,

                FullName = userRequest.FullName,
                PasswordHash = userRequest.PasswordHash,
                Phone = userRequest.Phone,
                Username = userRequest.UserName
            };
            var driver = new Driver { Id = 42 };

            var session = new CreatDriverSession
            {
                UserRequest = userRequest,
                DriverRequest = driverRequest
            };

            _userCommands.Setup(x => x.CreatUserAsync(userRequest))
                .ReturnsAsync(Result<User>.Success(user));

            _mockMapper.Setup(x => x.ToEntity(driverRequest))
                .Returns(driver);

            _repository.Setup(x => x.AddAsync(driver))
                .ThrowsAsync(new Exception("DB error"));

            var result = await _Commands.CreateDriverAsync(session);

            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to add Driver", result.Error);
        }




    }
}
