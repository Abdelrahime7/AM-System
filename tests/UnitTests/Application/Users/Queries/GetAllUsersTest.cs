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
                     new User { Id = 1, FullName = "Johen doe",
                         Email = "user1@example.com" ,
                         PasswordHash="1w132w12",
                         Phone="0122334455",
                              },
                     new User { Id = 2, FullName = "smith doe",
                         Email = "user2@example.com" ,
                         PasswordHash="1wfsfwfs",
                         Phone="0122355455", }
                   };

                var responses = new List<UserResponse>
                     {
                       new UserResponse{ FullName = "smith doe",
                         Email = "user1@example.com" ,
                         Phone="0122355455", },
                      new UserResponse {  FullName = "smith doe",
                         Email = "user2@example.com" ,  
                         Phone="0122355455", }

                      };

                _userRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);
                _mapperMock.Setup(m => m.ToResponse(users[0])).Returns(responses[0]);
                _mapperMock.Setup(m => m.ToResponse(users[1])).Returns(responses[1]);

                // Act
                var result = await _usersQueries.GetAllUsersAsync();

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(2, result.Value.Count());
                Assert.Contains(result.Value, r => r.Email == "user1@example.com");
                Assert.Contains(result.Value, r => r.Email == "user2@example.com");
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

