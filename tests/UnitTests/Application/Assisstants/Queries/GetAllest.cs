

using Application.Assisstants.Dto_s;
using Application.Assisstants.Features.Queries;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Assisstants.Queries
{
    public partial  class QueriesTest
    {
        private readonly Mock<IAssisstantRepository> _repository;
        private readonly Mock<IEntityMapper<Assisstant, CreatAssisstantRequest, UpdateAssisstantRequest, AssisstantResponse>> _mapper;
        private readonly Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>> _userMapper;
        private readonly Mock<AssisstantQueries> _queriesMock;

        public QueriesTest()
        {
            _repository = new Mock<IAssisstantRepository>();
            _mapper = new Mock<IEntityMapper<Assisstant, CreatAssisstantRequest, UpdateAssisstantRequest, AssisstantResponse>>();
            _userMapper = new Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>>();

            // Partial mock to override GetById
            _queriesMock = new Mock<AssisstantQueries>(_repository.Object, _mapper.Object, _userMapper.Object) { CallBase = true };
        }

        [Fact]
        public async Task GetAllAssisstants_ReturnsSuccess_WhenAssisstantsExist()
        {
            // Arrange
            var Assisstants = new List<Assisstant>
    {
        new Assisstant { Id = 1, User = new User { Id = 10,
                                Role=UserRole.AssistantAdmin,

         FullName="john doe",
         PasswordHash ="ae342rfew",
         Phone="+213755443344",
         Username="abd33reww"}
          },
        new Assisstant { Id = 2, User = new User { Id = 20,
                                        Role=UserRole.AssistantAdmin,

         FullName="john smith",
         PasswordHash ="ae34sswrfew",
         Phone="+213755433355",
         Username="abd33rwr4ww"} }
    };

            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(Assisstants);
            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Assisstants[0]);
            _repository.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(Assisstants[1]);

            _mapper.Setup(m => m.ToResponse(Assisstants[0])).Returns(new AssisstantResponse {  });
            _mapper.Setup(m => m.ToResponse(Assisstants[1])).Returns(new AssisstantResponse {  });

            _userMapper.Setup(m => m.ToResponse(Assisstants[0].User)).Returns(new UserResponse { Id = 10 });
            _userMapper.Setup(m => m.ToResponse(Assisstants[1].User)).Returns(new UserResponse { Id = 20 });

            // Act
            var result = await _queriesMock.Object.GetAllAssisstants();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count());
           
        }


        [Fact]
        public async Task GetAllAssisstants_ReturnsFailure_WhenNoAssisstantsExist()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Assisstant>());

            // Act
            var result = await _queriesMock.Object.GetAllAssisstants();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No Assisstants Found", result.Error);
        }

        [Fact]
        public async Task GetAllAssisstants_ReturnsFailure_WhenRepositoryThrows()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _queriesMock.Object.GetAllAssisstants();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failled to fetch Assisstants", result.Error);
        }

    }

}
