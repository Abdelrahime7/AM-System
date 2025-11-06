


using Application.Affiliates.DTO_s;
using Application.Users.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Affiliates.Queries
{
    public partial class QueriesTest
    {
       

        [Fact]
        public async Task GetById_ReturnsSuccess_WhenAffiliateExists()
        {
            // Arrange
            var Affiliate = new Affiliate { Id = 1, user = new User {
                Id = 10,
                FullName= "john smith",
                Phone="+213566443322",
                Username="werw34w3",
                PasswordHash="tewtr3qrwr"
            } };
            var affiliateResponse = new AffiliateResponse();
            var userResponse = new UserResponse { Id = 10 };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Affiliate);
            _mapper.Setup(m => m.ToResponse(Affiliate)).Returns(affiliateResponse);
            _userMapper.Setup(u => u.ToResponse(Affiliate.user)).Returns(userResponse);

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(10, result.Value.UserResponse.Id);
        }

        [Fact]
        public async Task GetById_ReturnsFailure_WhenAffiliateNotFound()
        {
            // Arrange
            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Affiliate)null);

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No Affiliate Found", result.Error);
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
            Assert.Contains("failed to fetch Affiliate", result.Error);
        }

        [Fact]
        public async Task GetById_ReturnsFailure_WhenMapperThrows()
        {
            // Arrange
            var Affiliate = new Affiliate { Id = 1, user = new User { Id = 10,
             FullName="john smith",
             PasswordHash="34234242w",
             Phone="+2135667788",
             Username ="wddee rr3w"}
             };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Affiliate);
            _mapper.Setup(m => m.ToResponse(Affiliate)).Throws(new Exception("Mapping error"));

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetch Affiliate", result.Error);
        }
    }

}
