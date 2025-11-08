using Application.Assisstants.Dto_s;
using Application.Assisstants.Dto_s.session;
using Application.Common.Models;

using Application.Users.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Assisstants.Commands
{
    public partial class CommandTests
    {
        

        [Fact]
        public async Task UpdateAssisstantAsync_ReturnsSuccess_WhenAllStepsSucceed()
        {
            // Arrange
            var Assisstant = new Assisstant { Id = 1 };
            var request = new UpdateAssisstantSession
            {
                AssisstantRequest = new UpdateAssisstantRequest { Id = 1 },
                UserRequest = new UpdateUserRequest()
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Assisstant);
            _userCommands.Setup(u => u.UpdateUserAsync(request.UserRequest))
     .ReturnsAsync(Result<bool>.Success(true));
            _mapper.Setup(m => m.ToUpdateEntity(Assisstant, request.AssisstantRequest));
            _repository.Setup(r => r.Update(Assisstant));

            // Act
            var result = await _Commands.UpdateAssisstantAsnc(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task UpdateAssisstantAsync_ReturnsFailure_WhenAssisstantNotFound()
        {
            // Arrange
            var request = new UpdateAssisstantSession
            {
                AssisstantRequest = new UpdateAssisstantRequest { Id = 1 },
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Assisstant)null);

            // Act
            var result = await _Commands.UpdateAssisstantAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Assisstant not found", result.Error);
        }

        [Fact]
        public async Task UpdateAssisstantAsync_ReturnsFailure_WhenUserUpdateThrows()
        {
            // Arrange
            var Assisstant = new Assisstant { Id = 1 };
            var request = new UpdateAssisstantSession
            {
                AssisstantRequest = new UpdateAssisstantRequest { Id = 1 },
                UserRequest = new UpdateUserRequest()
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Assisstant);
            _userCommands.Setup(u => u.UpdateUserAsync(request.UserRequest)).ThrowsAsync(new Exception("User update failed"));

            // Act
            var result = await _Commands.UpdateAssisstantAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Assisstant", result.Error);
        }

        [Fact]
        public async Task UpdateAssisstantAsync_ReturnsFailure_WhenMapperThrows()
        {
            // Arrange
            var Assisstant = new Assisstant { Id = 1 };
            var request = new UpdateAssisstantSession
            {
                AssisstantRequest = new UpdateAssisstantRequest { Id = 1 },
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Assisstant);
            _mapper.Setup(m => m.ToUpdateEntity(Assisstant, request.AssisstantRequest)).Throws(new Exception("Mapping failed"));

            // Act
            var result = await _Commands.UpdateAssisstantAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Assisstant", result.Error);
        }

        [Fact]
        public async Task UpdateAssisstantAsync_ReturnsFailure_WhenRepositoryUpdateThrows()
        {
            // Arrange
            var Assisstant = new Assisstant { Id = 1 };
            var request = new UpdateAssisstantSession
            {
                AssisstantRequest = new UpdateAssisstantRequest { Id = 1 },
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Assisstant);
            _mapper.Setup(m => m.ToUpdateEntity(Assisstant, request.AssisstantRequest));
            _repository.Setup(r => r.Update(Assisstant)).Throws(new Exception("DB error"));

            // Act
            var result = await _Commands.UpdateAssisstantAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Assisstant", result.Error);
        }
    }

}
