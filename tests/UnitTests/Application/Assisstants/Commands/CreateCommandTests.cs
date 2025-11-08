

using Application.Assisstants.Dto_s;
using Application.Assisstants.Dto_s.session;
using Application.Assisstants.Features.Commands;
using Application.Common.Models;
using Application.Interfaces.AssisstantInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Application.Users.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Assisstants.Commands
{
    public partial  class CommandTests
    {
        private readonly Mock<IUserCommands> _userCommands;
        private readonly Mock<IAssisstantRepository> _repository;
        private readonly Mock<IEntityMapper<Assisstant, CreatAssisstantRequest, UpdateAssisstantRequest,
        AssisstantResponse>> _mapper;

        private readonly IAssisstantCommands _Commands;


        public CommandTests()
        {
            _userCommands = new Mock<IUserCommands>();
            _repository = new Mock<IAssisstantRepository>();
            _mapper = new Mock<IEntityMapper<Assisstant, CreatAssisstantRequest,
                UpdateAssisstantRequest, AssisstantResponse>>();
            _Commands = new AssisstantCommands(_repository.Object,_userCommands.Object,_mapper.Object);
        }

        

            [Fact]
            public async Task CreateAssisstantAsync_ReturnsSuccess_WhenAllStepsSucceed()
            {
                // Arrange
                var request = new CreatAssisstantSession
                {
                    userRequest = new CreateUserRequest { FullName="john smith",
                     PasswordHash= "erwerwerw",
                      Phone="+213566779977",
                       UserName="qf22313"
                      },
                    assisstantRequest = new CreatAssisstantRequest()
                };

                var user = new User { Id = 1,
                    FullName= request.userRequest.FullName,
                    PasswordHash=request.userRequest.PasswordHash,
                    Phone=request.userRequest.Phone,
                    Username=request.userRequest.UserName,
                };
                var Assisstant = new Assisstant { Id = 99 };

                _userCommands.Setup(x => x.CreatUserAsync(request.userRequest))
                    .ReturnsAsync(Result<User>.Success(user));

                _mapper.Setup(x => x.ToEntity(request.assisstantRequest))
                    .Returns(Assisstant);

                _repository.Setup(x => x.AddAsync(Assisstant))
                    .Returns(Task.CompletedTask);

                // Act
                var result = await _Commands.CreateAssisstantAsync(request);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(99, result.Value);
            }

            [Fact]
            public async Task CreateAssisstantAsync_ReturnsFailure_WhenUserCreationFails()
            {
            // Arrange
            var request = new CreatAssisstantSession
            {
                userRequest = new CreateUserRequest
                {
                    FullName = "john smith",
                    PasswordHash = "erwerwerw",
                    Phone = "+213566779977",
                    UserName = "qf22313"
                },
                assisstantRequest = new CreatAssisstantRequest()
            };

            _userCommands.Setup(x => x.CreatUserAsync(request.userRequest))
                    .ReturnsAsync(Result<User>.Failure("User creation failed"));

                // Act
                var result = await _Commands.CreateAssisstantAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Failed to add Assisstant", result.Error);
            }

            [Fact]
            public async Task CreateAssisstantAsync_ReturnsFailure_WhenMapperThrows()
            {
            // Arrange
            var request = new CreatAssisstantSession
            {
                userRequest = new CreateUserRequest
                {
                    FullName = "john smith",
                    PasswordHash = "erwerwerw",
                    Phone = "+213566779977",
                    UserName = "qf22313"
                },
                assisstantRequest = new CreatAssisstantRequest()
            };

            var user = new User
            {
                Id = 1,
                FullName = request.userRequest.FullName,
                PasswordHash = request.userRequest.PasswordHash,
                Phone = request.userRequest.Phone,
                Username = request.userRequest.UserName,
            };

            _userCommands.Setup(x => x.CreatUserAsync(request.userRequest))
                    .ReturnsAsync(Result<User>.Success(user));

                _mapper.Setup(x => x.ToEntity(request.assisstantRequest))
                    .Throws(new Exception("Mapping failed"));

                // Act
                var result = await _Commands.CreateAssisstantAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Failed to add Assisstant", result.Error);
            }

            [Fact]
            public async Task CreateAssisstantAsync_ReturnsFailure_WhenRepositoryThrows()
            {
            // Arrange
            var request = new CreatAssisstantSession
            {
                userRequest = new CreateUserRequest
                {
                    FullName = "john smith",
                    PasswordHash = "erwerwerw",
                    Phone = "+213566779977",
                    UserName = "qf22313"
                },
                assisstantRequest = new CreatAssisstantRequest()
            };

            var user = new User
            {
                Id = 1,
                FullName = request.userRequest.FullName,
                PasswordHash = request.userRequest.PasswordHash,
                Phone = request.userRequest.Phone,
                Username = request.userRequest.UserName,
            };
            var Assisstant = new Assisstant { Id = 99 };

                _userCommands.Setup(x => x.CreatUserAsync(request.userRequest))
                    .ReturnsAsync(Result<User>.Success(user));

                _mapper.Setup(x => x.ToEntity(request.assisstantRequest))
                    .Returns(Assisstant);

                _repository.Setup(x => x.AddAsync(Assisstant))
                    .ThrowsAsync(new Exception("DB error"));

                // Act
                var result = await _Commands.CreateAssisstantAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Failed to add Assisstant", result.Error);
            }
        

    }
}
