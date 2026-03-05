using Application.AffiliatesBalance.DTOs;
using Application.AffiliatesBalance.Features.Queries;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Moq;

namespace UnitTests.Application.AffiliateBalance.Queries;

public partial class QueriesTests
{
    private readonly Mock<IAffiliateBalanceRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Domain.Entities.AffiliateBalance, CreateAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse>> _mockMapper;
    private readonly AffiliateBalanceQueries _queries;
    
    public QueriesTests()
    {
        _mockRepository = new Mock<IAffiliateBalanceRepository>();
        _mockMapper = new Mock<IEntityMapper<Domain.Entities.AffiliateBalance, CreateAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse>>();
        _queries = new AffiliateBalanceQueries(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task GetAllAffiliateBalancesAsync_ShouldReturnSuccess_WhenAffiliateBalancesAreFound()
    {
        // Arrange
        var affiliateBalances = Enumerable.Range(1, 3).Select(i => new Domain.Entities.AffiliateBalance
        {
            Id = i,
            Amount = 1000m * i,
            AffiliateId = 40 + i
        }).ToList();

        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(affiliateBalances);

        _mockMapper.Setup(m => m.ToResponse(It.IsAny<Domain.Entities.AffiliateBalance>()))
                   .Returns<Domain.Entities.AffiliateBalance>(ab => new AffiliateBalanceResponse
                   {
                       Id = ab.Id,
                       Amount = ab.Amount,
                       AffiliateId = ab.AffiliateId,
                       AffiliateName = $"Affiliate {ab.AffiliateId}"
                   });

        // Act
        var result = await _queries.GetAllAffiliateBalancesAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(affiliateBalances.Count, result.Value!.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Domain.Entities.AffiliateBalance>()), Times.Exactly(affiliateBalances.Count));
    }

    [Fact]
    public async Task GetAllAffiliateBalancesAsync_ShouldReturnFailure_WhenAffiliateBalancesNotFound()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(new List<Domain.Entities.AffiliateBalance>());

        // Act
        var result = await _queries.GetAllAffiliateBalancesAsync();
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Affiliate Balances Found", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Domain.Entities.AffiliateBalance>()), Times.Never);
    }
    
    [Fact]
    public async Task GetAllAffiliateBalancesAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ThrowsAsync(new Exception("DB Error"));

        // Act
        var result = await _queries.GetAllAffiliateBalancesAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch Affiliate Balances: DB Error", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
