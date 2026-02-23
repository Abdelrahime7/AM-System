

using Application.Assisstants.Dto_s;

using Application.Users.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Assisstants.Queries
{
    public partial class QueriesTest
    {
       

        [Fact]
        public async Task GetById_ReturnsSuccess_WhenAssisstantExists()
        {
            // Arrange
            var Assisstant = new Assisstant { Id = 1, User = new User {
                Role = Domain.Enums.UserRole.Assistant,

                Id = 10,
                FullName= "john smith",
                Phone="+213566443322",
                Username="werw34w3",
                PasswordHash="tewtr3qrwr"
            } };
            var AssisstantResponse = new AssisstantResponse {};
            var userResponse = new UserResponse {  };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Assisstant);
            _mapper.Setup(m => m.ToResponse(Assisstant)).Returns(AssisstantResponse);
            _userMapper.Setup(u => u.ToResponse(Assisstant.User)).Returns(userResponse);

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
          //Assert.Equal(10, result.Value.UserResponse.Id);
        }

        [Fact]
        public async Task GetById_ReturnsFailure_WhenAssisstantNotFound()
        {
            // Arrange
            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Assisstant)null);

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No Assisstant Found", result.Error);
        }

        [Fact]
        public async Task GetById_ReturnsFailure_WhenRepositoryThrows()
        {
            // Arrange
            _repository.Setup(r => r.GetByIdAsync(1)).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetch Assisstant", result.Error);
        }

        [Fact]
        public async Task GetById_ReturnsFailure_WhenMapperThrows()
        {
            // Arrange
            var Assisstant = new Assisstant { Id = 1, User = new User { Id = 10,
                Role = Domain.Enums.UserRole.Assistant,

                FullName = "john smith",
             PasswordHash="34234242w",
             Phone="+2135667788",
             Username ="wddee rr3w"}
             };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Assisstant);
            _mapper.Setup(m => m.ToResponse(Assisstant)).Throws(new Exception("Mapping error"));

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetch Assisstant", result.Error);
        }
    }

}
