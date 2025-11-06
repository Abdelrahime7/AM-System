using Application.Affiliates.DTO_s;
using Application.Affiliates.DTO_s.session;
using Application.Common.Models;

using Application.Users.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Affiliates.Commands
{
    public partial class CommandTests
    {
        

        [Fact]
        public async Task UpdateAffiliateAsync_ReturnsSuccess_WhenAllStepsSucceed()
        {
            // Arrange
            var Affiliate = new Affiliate { Id = 1 };
            var request = new UpdateAffiliateSession
            {
                AffiliateRequest = new UpdateAffiliateRequest { Id = 1 },
                UserRequest = new UpdateUserRequest()
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Affiliate);
            _userCommands.Setup(u => u.UpdateUserAsync(request.UserRequest))
     .ReturnsAsync(Result<bool>.Success(true));
            _mapper.Setup(m => m.ToUpdateEntity(Affiliate, request.AffiliateRequest));
            _repository.Setup(r => r.Update(Affiliate));

            // Act
            var result = await _Commands.UpdateAffiliateAsnc(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task UpdateAffiliateAsync_ReturnsFailure_WhenAffiliateNotFound()
        {
            // Arrange
            var request = new UpdateAffiliateSession
            {
                AffiliateRequest = new UpdateAffiliateRequest { Id = 1 },
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Affiliate)null);

            // Act
            var result = await _Commands.UpdateAffiliateAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Affiliate not found", result.Error);
        }

        [Fact]
        public async Task UpdateAffiliateAsync_ReturnsFailure_WhenUserUpdateThrows()
        {
            // Arrange
            var Affiliate = new Affiliate { Id = 1 };
            var request = new UpdateAffiliateSession
            {
                AffiliateRequest = new UpdateAffiliateRequest { Id = 1 },
                UserRequest = new UpdateUserRequest()
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Affiliate);
            _userCommands.Setup(u => u.UpdateUserAsync(request.UserRequest)).ThrowsAsync(new Exception("User update failed"));

            // Act
            var result = await _Commands.UpdateAffiliateAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Affiliate", result.Error);
        }

        [Fact]
        public async Task UpdateAffiliateAsync_ReturnsFailure_WhenMapperThrows()
        {
            // Arrange
            var Affiliate = new Affiliate { Id = 1 };
            var request = new UpdateAffiliateSession
            {
                AffiliateRequest = new UpdateAffiliateRequest { Id = 1 },
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Affiliate);
            _mapper.Setup(m => m.ToUpdateEntity(Affiliate, request.AffiliateRequest)).Throws(new Exception("Mapping failed"));

            // Act
            var result = await _Commands.UpdateAffiliateAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Affiliate", result.Error);
        }

        [Fact]
        public async Task UpdateAffiliateAsync_ReturnsFailure_WhenRepositoryUpdateThrows()
        {
            // Arrange
            var Affiliate = new Affiliate { Id = 1 };
            var request = new UpdateAffiliateSession
            {
                AffiliateRequest = new UpdateAffiliateRequest { Id = 1 },
                UserRequest = null
            };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Affiliate);
            _mapper.Setup(m => m.ToUpdateEntity(Affiliate, request.AffiliateRequest));
            _repository.Setup(r => r.Update(Affiliate)).Throws(new Exception("DB error"));

            // Act
            var result = await _Commands.UpdateAffiliateAsnc(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to update Affiliate", result.Error);
        }
    }

}
