

using Application.Admins.Dto_s;
using Application.Admins.DTO_s.session;
using Application.Admins.Features.Commands;
using Application.Common.Models;
using Application.Interfaces.AdminInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Application.Users.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Admins.Commands
{
    public partial  class CommandTests
    {
        private readonly Mock<IUserCommands> _userCommands;
        private readonly Mock<IAdminRepository> _repository;
        private readonly Mock<IEntityMapper<Admin, CreateAdminRequest, UpdateAdminRequest,
        AdminResponse>> _mapper;

        private readonly IAdminCommands _Commands;


        public CommandTests()
        {
            _userCommands = new Mock<IUserCommands>();
            _repository = new Mock<IAdminRepository>();
            _mapper = new Mock<IEntityMapper<Admin, CreateAdminRequest,
                UpdateAdminRequest, AdminResponse>>();
            _Commands = new AdminCommands(_repository.Object,_userCommands.Object,_mapper.Object);
        }

        

            [Fact]
            public async Task CreateAdminAsync_ReturnsSuccess_WhenAllStepsSucceed()
            {
                // Arrange
                var request = new CreatAdminSession
                {
                    UserRequest = new CreateUserRequest { FullName="john smith",
                     PasswordHash= "erwerwerw",
                      Phone="+213566779977",
                       UserName="qf22313"
                      },
                    AdminRequest = new CreateAdminRequest()
                };

                var user = new User { Id = 1,
                    FullName= request.UserRequest.FullName,
                    PasswordHash=request.UserRequest.PasswordHash,
                    Phone=request.UserRequest.Phone,
                    Username=request.UserRequest.UserName,
                };
                var admin = new Admin { Id = 99 };

                _userCommands.Setup(x => x.CreatUserAsync(request.UserRequest))
                    .ReturnsAsync(Result<User>.Success(user));

                _mapper.Setup(x => x.ToEntity(request.AdminRequest))
                    .Returns(admin);

                _repository.Setup(x => x.AddAsync(admin))
                    .Returns(Task.CompletedTask);

                // Act
                var result = await _Commands.CreateAdminAsync(request);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(99, result.Value);
            }

            [Fact]
            public async Task CreateAdminAsync_ReturnsFailure_WhenUserCreationFails()
            {
            // Arrange
            var request = new CreatAdminSession
            {
                UserRequest = new CreateUserRequest
                {
                    FullName = "john smith",
                    PasswordHash = "erwerwerw",
                    Phone = "+213566779977",
                    UserName = "qf22313"
                },
                AdminRequest = new CreateAdminRequest()
            };

            _userCommands.Setup(x => x.CreatUserAsync(request.UserRequest))
                    .ReturnsAsync(Result<User>.Failure("User creation failed"));

                // Act
                var result = await _Commands.CreateAdminAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Failed to add Admin", result.Error);
            }

            [Fact]
            public async Task CreateAdminAsync_ReturnsFailure_WhenMapperThrows()
            {
            // Arrange
            var request = new CreatAdminSession
            {
                UserRequest = new CreateUserRequest
                {
                    FullName = "john smith",
                    PasswordHash = "erwerwerw",
                    Phone = "+213566779977",
                    UserName = "qf22313"
                },
                AdminRequest = new CreateAdminRequest()
            };

            var user = new User
            {
                Id = 1,
                FullName = request.UserRequest.FullName,
                PasswordHash = request.UserRequest.PasswordHash,
                Phone = request.UserRequest.Phone,
                Username = request.UserRequest.UserName,
            };

            _userCommands.Setup(x => x.CreatUserAsync(request.UserRequest))
                    .ReturnsAsync(Result<User>.Success(user));

                _mapper.Setup(x => x.ToEntity(request.AdminRequest))
                    .Throws(new Exception("Mapping failed"));

                // Act
                var result = await _Commands.CreateAdminAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Failed to add Admin", result.Error);
            }

            [Fact]
            public async Task CreateAdminAsync_ReturnsFailure_WhenRepositoryThrows()
            {
            // Arrange
            var request = new CreatAdminSession
            {
                UserRequest = new CreateUserRequest
                {
                    FullName = "john smith",
                    PasswordHash = "erwerwerw",
                    Phone = "+213566779977",
                    UserName = "qf22313"
                },
                AdminRequest = new CreateAdminRequest()
            };

            var user = new User
            {
                Id = 1,
                FullName = request.UserRequest.FullName,
                PasswordHash = request.UserRequest.PasswordHash,
                Phone = request.UserRequest.Phone,
                Username = request.UserRequest.UserName,
            };
            var admin = new Admin { Id = 99 };

                _userCommands.Setup(x => x.CreatUserAsync(request.UserRequest))
                    .ReturnsAsync(Result<User>.Success(user));

                _mapper.Setup(x => x.ToEntity(request.AdminRequest))
                    .Returns(admin);

                _repository.Setup(x => x.AddAsync(admin))
                    .ThrowsAsync(new Exception("DB error"));

                // Act
                var result = await _Commands.CreateAdminAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Failed to add Admin", result.Error);
            }
        

    }
}
