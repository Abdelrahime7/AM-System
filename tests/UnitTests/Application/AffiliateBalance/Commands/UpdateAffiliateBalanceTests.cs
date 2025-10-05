using Application.AffiliatesBalance.DTOs;
using Moq;

namespace UnitTests.Application.AffiliateBalance.Commands;

public partial class CommandsTests
{
    [Fact]
    public async Task UpdateAffiliateBalanceAsync_ShouldReturnSuccess_WhenAffiliateBalanceIsUpdated()
    {
        //Arrange
        var request = new UpdateAffiliateBalanceRequest
        {
            Id = 25,
            Amount = 2000.50m
        };
        
        var affiliateBalance = new Domain.Entities.AffiliateBalance
        {
            Id = 25,
            Amount = 1500.75m,
            AffiliateId = 42
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(affiliateBalance);
        
        // Act
        var result = await _commands.UpdateAffiliateBalanceAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockMapper.Verify(m => m.ToUpdateEntity(affiliateBalance, request), Times.Once);
        _mockRepository.Verify(r => r.Update(affiliateBalance), Times.Once);
    }

    [Fact]
    public async Task UpdateAffiliateBalanceAsync_ShouldReturnFailure_WhenAffiliateBalanceNotFound()
    {
        //Arrange
        var request = new UpdateAffiliateBalanceRequest
        {
            Id = 25,
            Amount = 2000.50m
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((Domain.Entities.AffiliateBalance)null!);

        //Act
        var result = await _commands.UpdateAffiliateBalanceAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Affiliate Balance Not Found", result.Error);
    }
    
    [Fact]
    public async Task UpdateAffiliateBalanceAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        var request = new UpdateAffiliateBalanceRequest
        {
            Id = 25,
            Amount = 2000.50m
        };
        
        var affiliateBalance = new Domain.Entities.AffiliateBalance
        {
            Id = 25,
            Amount = 1500.75m,
            AffiliateId = 42
        };

        _mockRepository.Setup(m => m.GetByIdAsync(request.Id)).ReturnsAsync(affiliateBalance);
        _mockRepository.Setup(r => r.Update(It.IsAny<Domain.Entities.AffiliateBalance>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.UpdateAffiliateBalanceAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to update affiliate balance: DB Error", result.Error);
    }
}