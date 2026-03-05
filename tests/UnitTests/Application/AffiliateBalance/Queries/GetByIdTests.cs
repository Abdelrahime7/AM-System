using Application.AffiliatesBalance.DTOs;
using Moq;

namespace UnitTests.Application.AffiliateBalance.Queries;

public partial class QueriesTests
{
    [Fact]
    public async Task GetAffiliateBalanceByIdAsync_ShouldReturnSuccess_WhenAffiliateBalanceIsFound()
    {
        //Arrange
        const int requestId = 25;
        var affiliateBalance = new Domain.Entities.AffiliateBalance
        {
            Id = 25,
            Amount = 1500.75m,
            AffiliateId = 42
        };
        
        var response = new AffiliateBalanceResponse
        {
            Id = affiliateBalance.Id,
            Amount = affiliateBalance.Amount,
            AffiliateId = affiliateBalance.AffiliateId,
            AffiliateName = "John Doe"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync(affiliateBalance);

        _mockMapper.Setup(m => m.ToResponse(affiliateBalance))
            .Returns(response);

        // Act
        var result = await _queries.GetAffiliateBalanceByIdAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(affiliateBalance.Id, result.Value.Id);
        Assert.Equal(affiliateBalance.Amount, result.Value.Amount);
        Assert.Equal(affiliateBalance.AffiliateId, result.Value.AffiliateId);

        _mockRepository.Verify(r => r.GetByIdAsync(requestId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(affiliateBalance), Times.Once);
    }

    [Fact]
    public async Task GetAffiliateBalanceByIdAsync_ShouldReturnFailure_WhenAffiliateBalanceNotFound()
    {
        //Arrange
        const int requestId = 25;
        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync((Domain.Entities.AffiliateBalance)null!);

        //Act
        var result = await _queries.GetAffiliateBalanceByIdAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Affiliate Balance Found", result.Error);
    }
    
    [Fact]
    public async Task GetAffiliateBalanceByIdAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = 25;

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ThrowsAsync(new Exception("DB Error"));

        //Act
        var result = await _queries.GetAffiliateBalanceByIdAsync(requestId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch Affiliate Balance: DB Error", result.Error);
    }
    
}