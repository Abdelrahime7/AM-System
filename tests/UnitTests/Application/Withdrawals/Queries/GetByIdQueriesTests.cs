using Application.Withdrawals.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Withdrawals.Queries;

public partial class WithdrawalQueriesTests
{
    [Fact]
    public async Task GetWithdrawalByIdAsync_ShouldReturnSuccess_WhenWithdrawalIsFound()
    {
        //Arrange
        const int requestId = 10;
        var user = new User
        {
            Role =UserRole.Admin,
            Username = "user123doe",
            Email = "old@example.com",
            FullName = "john doe",
            Phone = "0611223344",
            PasswordHash = null!
        };

        var affiliateBalance = new Domain.Entities.AffiliateBalance { Amount = 1500.75m };

        var processedByUser = new User
        {
            Role = UserRole.Admin,
            Username = "user123doe",
            Email = "old@example.com",
            FullName = "john doe",
            Phone = "0611223344",
            PasswordHash = null!
        };
        
        var withdrawal = new Withdrawal
        {
            Id = 10,
            Amount = 500.75m,
            Status = WithdrawalStatus.Approved,
            ProcessedAt = DateTime.UtcNow,
            AffiliateId = 42,
            AffiliateBalanceId = 25,
            ProcessedBy = 1,
            Affiliate = user,
            AffiliateBalance = affiliateBalance,
            ProcessedByUser = processedByUser
        };
        
        var response = new WithdrawalResponse
        {
            Id = withdrawal.Id,
            Amount = withdrawal.Amount,
            Status = withdrawal.Status.ToString(),
            ProcessedAt = withdrawal.ProcessedAt,
            AffiliateId = withdrawal.AffiliateId,
          
            AffiliateBalanceId = withdrawal.AffiliateBalanceId,
            CurrentBalance = withdrawal.AffiliateBalance.Amount,
            ProcessedBy = withdrawal.ProcessedBy,
            
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync(withdrawal);

        _mockMapper.Setup(m => m.ToResponse(withdrawal))
            .Returns(response);

        // Act
        var result = await _queries.GetWithdrawalByIdAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(withdrawal.Id, result.Value.Id);
        Assert.Equal(withdrawal.Amount, result.Value.Amount);
        Assert.Equal(withdrawal.Status.ToString(), result.Value.Status);
        Assert.Equal(withdrawal.AffiliateId, result.Value.AffiliateId);
        Assert.Equal(withdrawal.AffiliateBalanceId, result.Value.AffiliateBalanceId);

        _mockRepository.Verify(r => r.GetByIdAsync(requestId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(withdrawal), Times.Once);
    }

    [Fact]
    public async Task GetWithdrawalByIdAsync_ShouldReturnFailure_WhenWithdrawalNotFound()
    {
        //Arrange
        const int requestId = 10;
        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync((Withdrawal)null!);

        //Act
        var result = await _queries.GetWithdrawalByIdAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Withdrawal Found", result.Error);
    }
    
    [Fact]
    public async Task GetWithdrawalByIdAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = 10;

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ThrowsAsync(new Exception("DB Error"));

        //Act
        var result = await _queries.GetWithdrawalByIdAsync(requestId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch withdrawal: DB Error", result.Error);
    }
}