using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Withdrawals.DTOs;
using Application.Withdrawals.Features.Queries;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Withdrawals.Queries;

public partial class WithdrawalQueriesTests
{
    private readonly Mock<IWithdrawalRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Withdrawal, CreateWithdrawalRequest, UpdateWithdrawalRequest, WithdrawalResponse>> _mockMapper;
    private readonly WithdrawalQueries _queries;
    
    public WithdrawalQueriesTests()
    {
        _mockRepository = new Mock<IWithdrawalRepository>();
        _mockMapper = new Mock<IEntityMapper<Withdrawal, CreateWithdrawalRequest, UpdateWithdrawalRequest, WithdrawalResponse>>();
        _queries = new WithdrawalQueries(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task GetAllWithdrawalsAsync_ShouldReturnSuccess_WhenWithdrawalsAreFound()
    {
        // Arrange
        var withdrawals = Enumerable.Range(1, 3).Select(i => new Withdrawal
        {
            Id = i,
            Amount = 100m * i,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 40 + i,
            AffiliateBalanceId = 20 + i,
            ProcessedBy = i == 3 ? 1 : (int?)null
        }).ToList();

        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(withdrawals);

        _mockMapper.Setup(m => m.ToResponse(It.IsAny<Withdrawal>()))
                   .Returns<Withdrawal>(w => new WithdrawalResponse
                   {
                       Id = w.Id,
                       Amount = w.Amount,
                       Status = w.Status.ToString(),
                       AffiliateId = w.AffiliateId,
                       AffiliateName = $"Affiliate {w.AffiliateId}",
                       AffiliateBalanceId = w.AffiliateBalanceId,
                       CurrentBalance = 1000m,
                       ProcessedBy = w.ProcessedBy,
                       ProcessedByName = w.ProcessedBy.HasValue ? $"Admin {w.ProcessedBy}" : null
                   });

        // Act
        var result = await _queries.GetAllWithdrawalsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(withdrawals.Count, result.Value!.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Withdrawal>()), Times.Exactly(withdrawals.Count));
    }

    [Fact]
    public async Task GetAllWithdrawalsAsync_ShouldReturnFailure_WhenWithdrawalsNotFound()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(new List<Withdrawal>());

        // Act
        var result = await _queries.GetAllWithdrawalsAsync();
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Withdrawals Found", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Withdrawal>()), Times.Never);
    }
    
    [Fact]
    public async Task GetAllWithdrawalsAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ThrowsAsync(new Exception("DB Error"));

        // Act
        var result = await _queries.GetAllWithdrawalsAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch withdrawals: DB Error", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
