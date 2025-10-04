using Application.AffiliatesBalance.DTOs;
using Application.AffiliatesBalance.Features.Commands;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Moq;

namespace UnitTests.Application.AffiliateBalance.Commands;

public partial class CommandsTests
{
    private readonly Mock<IAffiliateBalanceRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Domain.Entities.AffiliateBalance, CreateAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse>> _mockMapper;
    private readonly AffiliateBalanceCommands _commands;
    
    public CommandsTests()
    {
        _mockRepository = new Mock<IAffiliateBalanceRepository>();
        _mockMapper = new Mock<IEntityMapper<Domain.Entities.AffiliateBalance, CreateAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse>>();
        _commands = new AffiliateBalanceCommands(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task CreateAffiliateBalanceAsync_ShouldReturnSuccess_WhenAffiliateBalanceIsAdded()
    {
        //Arrange
        var request = new CreateAffiliateBalanceRequest
        {
            Amount = 1500.75m,
            AffiliateId = 42
        };
        
        var existingAffiliateBalance = new Domain.Entities.AffiliateBalance
        {
            Id = 25,
            Amount = 1500.75m,
            AffiliateId = 42
        };

        _mockMapper.Setup(m => m.ToEntity(request)).Returns(existingAffiliateBalance);
        _mockRepository.Setup(r => r.AddAsync(existingAffiliateBalance));

        //Act
        var result = await _commands.CreateAffiliateBalanceAsync(request);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(25, result.Value);
        Assert.Equal(25, existingAffiliateBalance.Id);
        Assert.Equal(request.Amount, existingAffiliateBalance.Amount);
        _mockMapper.Verify(m => m.ToEntity(request), Times.Once);
        _mockRepository.Verify(r => r.AddAsync(existingAffiliateBalance), Times.Once);
    }

    [Fact]
    public async Task CreateAffiliateBalanceAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new CreateAffiliateBalanceRequest
        {
            Amount = -100m,
            AffiliateId = -1
        };

        var affiliateBalance = new Domain.Entities.AffiliateBalance
        {
            Id = -1,
            Amount = -100m,
            AffiliateId = -1
        };

        _mockMapper.Setup(m => m.ToEntity(It.IsAny<CreateAffiliateBalanceRequest>()))
            .Returns(affiliateBalance);

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.AffiliateBalance>()))
            .ThrowsAsync(new Exception("DB Error"));
        
        // Act
        var result = await _commands.CreateAffiliateBalanceAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error creating affiliate balance: DB Error", result.Error);
    }
}