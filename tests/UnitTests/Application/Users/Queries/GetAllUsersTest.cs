using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Application.Users.Features.Queries;
using Domain.Entities;

using Moq;


namespace UnitTests.Application.Users.Queries
{
    public partial  class UserQueriesTests 
    {

        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>> _mapperMock;
        private readonly UsersQueries _usersQueries;

        public UserQueriesTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>>();
            _usersQueries = new UsersQueries(_userRepoMock.Object, _mapperMock.Object);
        }


      
            [Fact]
            public async Task GetAllUsersAsync_ShouldReturnSuccess_WhenUsersExist()
            {
                // Arrange
                var users = new List<User>
                   {
                     new User { Id = 1,
                         Username = "user123doe",
                           Email = "old@example.com",
                           FullName = "john doe",
                         Phone = "0611223344",


                         PasswordHash="1w132w12",
                        
                              },
                     new User { Id = 2, Username = "user1234doee",

                           Email = "old2@example.com",
                           FullName = "johne",
                         Phone = "0611223364",

                         PasswordHash="1wfsfwfs",
                        }
                   };

                var responses = new List<UserResponse>
                     {
                       new UserResponse{ Username = "smith121doe",
                        },
                      new UserResponse {  Username = "smith3435doe",
                          }

                      };

                _userRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);
                _mapperMock.Setup(m => m.ToResponse(users[0])).Returns(responses[0]);
                _mapperMock.Setup(m => m.ToResponse(users[1])).Returns(responses[1]);

                // Act
                var result = await _usersQueries.GetAllUsersAsync();

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(2, result.Value.Count());
              
            }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnFailure_WhenNoUsersExist()
        {
            // Arrange
            _userRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>());

            // Act
            var result = await _usersQueries.GetAllUsersAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No users found.", result.Error);
        }


        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            _userRepoMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _usersQueries.GetAllUsersAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetche users", result.Error);
            Assert.Contains("DB error", result.Error);
        }
    }


}

