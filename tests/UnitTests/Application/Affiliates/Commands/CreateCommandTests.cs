

using Application.Affiliates.DTO_s;
using Application.Affiliates.DTO_s.session;
using Application.Affiliates.Features.Commands;
using Application.Common.Models;
using Application.Interfaces.AffiliateInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Affiliates.Commands
{
    public partial  class CommandTests
    {
        private readonly Mock<IUserCommands> _userCommands;
        private readonly Mock<IAffiliateRepository> _repository;
        private readonly Mock<IEntityMapper<Affiliate, CreateAffiliateRequest, UpdateAffiliateRequest,
        AffiliateResponse>> _mapper;

        private readonly IAffiliateCommands _Commands;


        public CommandTests()
        {
            _userCommands = new Mock<IUserCommands>();
            _repository = new Mock<IAffiliateRepository>();
            _mapper = new Mock<IEntityMapper<Affiliate, CreateAffiliateRequest,
                UpdateAffiliateRequest, AffiliateResponse>>();
            _Commands = new AffiliateCommands(_repository.Object,_userCommands.Object,_mapper.Object);
        }

        

            [Fact]
            public async Task CreateAffiliateAsync_ReturnsSuccess_WhenAllStepsSucceed()
            {
                // Arrange
                var request = new CreatAffiliateSession
                {
                    UserRequest = new CreateUserRequest { FullName="john smith",
                        Role = UserRole.Affiliate,
                        PasswordHash = "erwerwerw",
                      Phone="+213566779977",
                       UserName="qf22313"
                      },
                    AffiliateRequest = new CreateAffiliateRequest()
                };

                var user = new User { Id = 1,
                    FullName= request.UserRequest.FullName,
                    Role = request.UserRequest.Role,
                    PasswordHash =request.UserRequest.PasswordHash,
                    Phone=request.UserRequest.Phone,
                    Username=request.UserRequest.UserName,
                };
                var Affiliate = new Affiliate { Id = 99 };

                _userCommands.Setup(x => x.CreatUserAsync(request.UserRequest))
                    .ReturnsAsync(Result<User>.Success(user));

                _mapper.Setup(x => x.ToEntity(request.AffiliateRequest))
                    .Returns(Affiliate);

                _repository.Setup(x => x.AddAsync(Affiliate))
                    .Returns(Task.CompletedTask);

                // Act
                var result = await _Commands.CreateAffiliateAsync(request);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(99, result.Value);
            }

            [Fact]
            public async Task CreateAffiliateAsync_ReturnsFailure_WhenUserCreationFails()
            {
            // Arrange
            var request = new CreatAffiliateSession
            {
                UserRequest = new CreateUserRequest
                { Role =UserRole.Affiliate,
                    FullName = "john smith",
                    PasswordHash = "erwerwerw",
                    Phone = "+213566779977",
                    UserName = "qf22313"
                },
                AffiliateRequest = new CreateAffiliateRequest()
            };

            _userCommands.Setup(x => x.CreatUserAsync(request.UserRequest))
                    .ReturnsAsync(Result<User>.Failure("User creation failed"));

                // Act
                var result = await _Commands.CreateAffiliateAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Failed to add Affiliate", result.Error);
            }

            [Fact]
            public async Task CreateAffiliateAsync_ReturnsFailure_WhenMapperThrows()
            {
            // Arrange
            var request = new CreatAffiliateSession
            {
                UserRequest = new CreateUserRequest
                {
                    Role=UserRole.Affiliate,
                    FullName = "john smith",
                    PasswordHash = "erwerwerw",
                    Phone = "+213566779977",
                    UserName = "qf22313"
                },
                AffiliateRequest = new CreateAffiliateRequest()
            };

            var user = new User
            {
                Role=UserRole.Affiliate,
                Id = 1,
                FullName = request.UserRequest.FullName,
                PasswordHash = request.UserRequest.PasswordHash,
                Phone = request.UserRequest.Phone,
                Username = request.UserRequest.UserName,
            };

            _userCommands.Setup(x => x.CreatUserAsync(request.UserRequest))
                    .ReturnsAsync(Result<User>.Success(user));

                _mapper.Setup(x => x.ToEntity(request.AffiliateRequest))
                    .Throws(new Exception("Mapping failed"));

                // Act
                var result = await _Commands.CreateAffiliateAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Failed to add Affiliate", result.Error);
            }

            [Fact]
            public async Task CreateAffiliateAsync_ReturnsFailure_WhenRepositoryThrows()
            {
            // Arrange
            var request = new CreatAffiliateSession
            {
                UserRequest = new CreateUserRequest
                {
                    Role = UserRole.Affiliate,
                    FullName = "john smith",
                    PasswordHash = "erwerwerw",
                    Phone = "+213566779977",
                    UserName = "qf22313"
                },
                AffiliateRequest = new CreateAffiliateRequest()
            };

            var user = new User
            {
                Id = 1,
                Role = request.UserRequest.Role,
                FullName = request.UserRequest.FullName,
                PasswordHash = request.UserRequest.PasswordHash,
                Phone = request.UserRequest.Phone,
                Username = request.UserRequest.UserName,
            };
            var Affiliate = new Affiliate { Id = 99 };

                _userCommands.Setup(x => x.CreatUserAsync(request.UserRequest))
                    .ReturnsAsync(Result<User>.Success(user));

                _mapper.Setup(x => x.ToEntity(request.AffiliateRequest))
                    .Returns(Affiliate);

                _repository.Setup(x => x.AddAsync(Affiliate))
                    .ThrowsAsync(new Exception("DB error"));

                // Act
                var result = await _Commands.CreateAffiliateAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Failed to add Affiliate", result.Error);
            }
        

    }
}
