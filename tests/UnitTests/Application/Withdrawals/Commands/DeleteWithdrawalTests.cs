using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Withdrawals.Commands;

public partial class WithdrawalCommandsTests
{
    [Fact]
    public async Task DeleteWithdrawalAsync_ShouldReturnSuccess_WhenWithdrawalIsDeleted()
    {
        //Arrange
        const int requestId = 10;
        var withdrawal = new Withdrawal
        {
            Id = 10,
            Amount = 500.75m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync(withdrawal);
        
        // Act
        var result = await _commands.DeleteWithdrawalAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockRepository.Verify(r => r.Delete(withdrawal), Times.Once);
    }

    [Fact]
    public async Task DeleteWithdrawalAsync_ShouldReturnFailure_WhenWithdrawalNotFound()
    {
        //Arrange
        const int requestId = -1;

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync((Withdrawal)null!);

        //Act
        var result = await _commands.DeleteWithdrawalAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Withdrawal Not Found", result.Error);
    }
    
    [Fact]
    public async Task DeleteWithdrawalAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = 10;
        var withdrawal = new Withdrawal
        {
            Id = 10,
            Amount = 500.75m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25
        };

        _mockRepository.Setup(m => m.GetByIdAsync(requestId)).ReturnsAsync(withdrawal);
        _mockRepository.Setup(r => r.Delete(It.IsAny<Withdrawal>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.DeleteWithdrawalAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to delete withdrawal: DB Error", result.Error);
    }
}