
using Domain.Entities;
using Moq;


namespace UnitTests.Application.Affiliates.Commands
{
    public partial class CommandTests
    {

       
           
            [Fact]
            public async Task DeleteAffiliateAsync_ReturnsSuccess_WhenAffiliateExists()
            {
                // Arrange
                var Affiliate = new Affiliate { Id = 1 };
                _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Affiliate);
                _repository.Setup(r => r.Delete(Affiliate));

                // Act
                var result = await _Commands.DeleteAffiliateAsnc(1);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.True(result.Value);
            }

            [Fact]
            public async Task DeleteAffiliateAsync_ReturnsFailure_WhenAffiliateNotFound()
            {
                // Arrange
                _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Affiliate)null);

                // Act
                var result = await _Commands.DeleteAffiliateAsnc(1);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Affiliate Not Found", result.Error);
            }

            [Fact]
            public async Task DeleteAffiliateAsync_ReturnsFailure_WhenRepositoryThrows()
            {
                // Arrange
                var Affiliate = new Affiliate { Id = 1 };
                _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Affiliate);
                _repository.Setup(r => r.Delete(Affiliate)).Throws(new Exception("DB error"));

                // Act
                var result = await _Commands.DeleteAffiliateAsnc(1);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Contains("Error Deleting Affiliate", result.Error);
            }
        

    }
}
