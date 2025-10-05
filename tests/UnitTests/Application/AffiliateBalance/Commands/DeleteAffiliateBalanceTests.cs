using Moq;

namespace UnitTests.Application.AffiliateBalance.Commands;

public partial class CommandsTests
{
    [Fact]
    public async Task DeleteAffiliateBalanceAsync_ShouldReturnSuccess_WhenAffiliateBalanceIsDeleted()
    {
        //Arrange
        const int requestId = 25;
        var affiliateBalance = new Domain.Entities.AffiliateBalance
        {
            Id = 25,
            Amount = 1500.75m,
            AffiliateId = 42
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync(affiliateBalance);
        
        // Act
        var result = await _commands.DeleteAffiliateBalanceAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockRepository.Verify(r => r.Delete(affiliateBalance), Times.Once);
    }

    [Fact]
    public async Task DeleteAffiliateBalanceAsync_ShouldReturnFailure_WhenAffiliateBalanceNotFound()
    {
        //Arrange
        const int requestId = -1;

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync((Domain.Entities.AffiliateBalance)null!);

        //Act
        var result = await _commands.DeleteAffiliateBalanceAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Affiliate Balance Not Found", result.Error);
    }
    
    [Fact]
    public async Task DeleteAffiliateBalanceAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = 25;
        var affiliateBalance = new Domain.Entities.AffiliateBalance
        {
            Id = 25,
            Amount = 1500.75m,
            AffiliateId = 42
        };

        _mockRepository.Setup(m => m.GetByIdAsync(requestId)).ReturnsAsync(affiliateBalance);
        _mockRepository.Setup(r => r.Delete(It.IsAny<Domain.Entities.AffiliateBalance>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.DeleteAffiliateBalanceAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to delete affiliate balance: DB Error", result.Error);
    }
}