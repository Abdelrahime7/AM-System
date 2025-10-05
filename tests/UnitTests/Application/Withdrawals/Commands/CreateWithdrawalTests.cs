using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Withdrawals.DTOs;
using Application.Withdrawals.Features.Commands;
using Domain.Enums;
using Domain.Entities;
using Moq;

namespace WithdrawalRequestValidatorTests.Commands;

public partial class WithdrawalCommandsTests
{
     private readonly Mock<IWithdrawalRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Withdrawal, CreateWithdrawalRequest, UpdateWithdrawalRequest, WithdrawalResponse>> _mockMapper;
    private readonly WithdrawalCommands _commands;
    
    public WithdrawalCommandsTests()
    {
        _mockRepository = new Mock<IWithdrawalRepository>();
        _mockMapper = new Mock<IEntityMapper<Withdrawal, CreateWithdrawalRequest, UpdateWithdrawalRequest, WithdrawalResponse>>();
        _commands = new WithdrawalCommands(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task CreateWithdrawalAsync_ShouldReturnSuccess_WhenWithdrawalIsAdded()
    {
        //Arrange
        var request = new CreateWithdrawalRequest
        {
            Amount = 500.75m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25,
            ProcessedBy = 1
        };
        
        var existingWithdrawal = new Withdrawal
        {
            Id = 10,
            Amount = 500.75m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25,
            ProcessedBy = 1
        };

        _mockMapper.Setup(m => m.ToEntity(request)).Returns(existingWithdrawal);
        _mockRepository.Setup(r => r.AddAsync(existingWithdrawal));

        //Act
        var result = await _commands.CreateWithdrawalAsync(request);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(10, result.Value);
        Assert.Equal(10, existingWithdrawal.Id);
        Assert.Equal(request.Amount, existingWithdrawal.Amount);
        Assert.Equal(request.Status, existingWithdrawal.Status);
        _mockMapper.Verify(m => m.ToEntity(request), Times.Once);
        _mockRepository.Verify(r => r.AddAsync(existingWithdrawal), Times.Once);
    }

    [Fact]
    public async Task CreateWithdrawalAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new CreateWithdrawalRequest
        {
            Amount = -100m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = -1,
            AffiliateBalanceId = -1
        };

        var withdrawal = new Withdrawal
        {
            Id = -1,
            Amount = -100m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = -1,
            AffiliateBalanceId = -1
        };

        _mockMapper.Setup(m => m.ToEntity(It.IsAny<CreateWithdrawalRequest>()))
            .Returns(withdrawal);

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Withdrawal>()))
            .ThrowsAsync(new Exception("DB Error"));
        
        // Act
        var result = await _commands.CreateWithdrawalAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error creating withdrawal: DB Error", result.Error);
    }
}