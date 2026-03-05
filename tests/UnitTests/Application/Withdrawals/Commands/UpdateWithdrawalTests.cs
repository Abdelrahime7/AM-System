using Application.Withdrawals.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Withdrawals.Commands;

public partial class WithdrawalCommandsTests
{
    [Fact]
    public async Task UpdateWithdrawalAsync_ShouldReturnSuccess_WhenWithdrawalIsUpdated()
    {
        //Arrange
        var request = new UpdateWithdrawalRequest
        {
            Id = 10,
            Amount = 750.50m,
            Status = WithdrawalStatus.Approved,
            ProcessedAt = DateTime.UtcNow
        };
        
        var withdrawal = new Withdrawal
        {
            Id = 10,
            Amount = 500.75m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(withdrawal);
        
        // Act
        var result = await _commands.UpdateWithdrawalAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockMapper.Verify(m => m.ToUpdateEntity(withdrawal, request), Times.Once);
        _mockRepository.Verify(r => r.Update(withdrawal), Times.Once);
    }

    [Fact]
    public async Task UpdateWithdrawalAsync_ShouldReturnFailure_WhenWithdrawalNotFound()
    {
        //Arrange
        var request = new UpdateWithdrawalRequest
        {
            Id = 10,
            Amount = 750.50m,
            Status = WithdrawalStatus.Approved
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((Withdrawal)null!);

        //Act
        var result = await _commands.UpdateWithdrawalAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Withdrawal Not Found", result.Error);
    }
    
    [Fact]
    public async Task UpdateWithdrawalAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        var request = new UpdateWithdrawalRequest
        {
            Id = 10,
            Amount = 750.50m,
            Status = WithdrawalStatus.Approved
        };
        
        var withdrawal = new Withdrawal
        {
            Id = 10,
            Amount = 500.75m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25
        };

        _mockRepository.Setup(m => m.GetByIdAsync(request.Id)).ReturnsAsync(withdrawal);
        _mockRepository.Setup(r => r.Update(It.IsAny<Withdrawal>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.UpdateWithdrawalAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to update withdrawal: DB Error", result.Error);
    }
}