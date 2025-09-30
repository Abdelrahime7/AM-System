using Application.Customers.Features.Commands;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Delivery.Commands;

public partial class  DeliveryCommandsTests
{
    private readonly Mock<IDeliveryRepository> _mockRepository;
    private readonly Mock<IEntityMapper<DeliveryIntegration, CreateDeliveryIntegrationRequest, 
        UpdateDeliveryIntegrationRequest , DeliveryIntegrationResponse>> _mockMapper;
  
    private readonly DeliveryIntgrationCommands _commands;
    
    public DeliveryCommandsTests()
    {
        _mockRepository = new Mock<IDeliveryRepository>();
        _mockMapper = new Mock<IEntityMapper<DeliveryIntegration, CreateDeliveryIntegrationRequest,
        UpdateDeliveryIntegrationRequest, DeliveryIntegrationResponse>>();
        _commands = new DeliveryIntgrationCommands(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
   
    public async Task CreateDeliveryIntegrationAsync_ShouldReturnSuccessResult_WhenRepositorySucceeds()
    {
        // Arrange
        var request = new CreateDeliveryIntegrationRequest
        {
            Name = "FastShip",
            ApiEndpoint = "https://api.fastship.com",
            ApiKey = "abc123",
            ApiSecret = "xyz789",
            IsActive = true
        };

        var deliveryEntity = new DeliveryIntegration
        {
            Id = 101,
            Name = request.Name,
            ApiEndpoint = request.ApiEndpoint,
            ApiKey = request.ApiKey,
            ApiSecret = request.ApiSecret,
            IsActive = request.IsActive
        };

       

        _mockMapper.Setup(m => m.ToEntity(request)).Returns(deliveryEntity);
        _mockRepository.Setup(r => r.AddAsync(deliveryEntity)).Returns(Task.CompletedTask);

        // Act
        var result = await _commands.CreateDeliveryIntegrationAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(101, result.Value);
    }

    [Fact]
    public async Task CreateDeliveryIntegrationAsync_ShouldReturnFailureResult_WhenRepositoryThrowsException()
    {
        // Arrange
        var request = new CreateDeliveryIntegrationRequest
        {
            Name = "FailShip",
            ApiEndpoint = "https://api.failship.com",
            ApiKey = "failkey",
            ApiSecret = "failsecret",
            IsActive = false
        };

        var deliveryEntity = new DeliveryIntegration
        {
            Name = request.Name,
            ApiEndpoint = request.ApiEndpoint,
            ApiKey = request.ApiKey,
            ApiSecret = request.ApiSecret,
            IsActive = request.IsActive
        };

       

        _mockMapper.Setup(m => m.ToEntity(request)).Returns(deliveryEntity);
       _mockRepository.Setup(r => r.AddAsync(deliveryEntity)).ThrowsAsync(new Exception("DB failure"));

        // Act
        var result = await _commands.CreateDeliveryIntegrationAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error creating Delivery Integration", result.Error);
        Assert.Contains("DB failure", result.Error);
    }


}