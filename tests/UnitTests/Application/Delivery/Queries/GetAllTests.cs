using Application.Customers.Features.Queries;
using Application.Customers.Mapper;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Delivery.Queries;

public partial class DeliveryQueriesTests
{
    private readonly Mock<IDeliveryRepository> _mockRepository;

    private readonly Mock<IEntityMapper<DeliveryIntegration, CreateDeliveryIntegrationRequest,
        UpdateDeliveryIntegrationRequest, DeliveryIntegrationResponse>> _mockMapper;
    private readonly DeliveryIntegrationQueries _queries;
    
    public DeliveryQueriesTests()
    {
        _mockRepository = new Mock<IDeliveryRepository>();
        _mockMapper = new Mock<IEntityMapper<DeliveryIntegration, CreateDeliveryIntegrationRequest,
        UpdateDeliveryIntegrationRequest, DeliveryIntegrationResponse>>();
        _queries = new DeliveryIntegrationQueries(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnSuccess_WhenEntitiesExist()
    {
        // Arrange
        var entities = new List<DeliveryIntegration>
    {
        new DeliveryIntegration { Id = 1, Name = "FastShip", IsActive = true },
        new DeliveryIntegration { Id = 2, Name = "QuickDrop", IsActive = false }
    };

        var responses = new List<DeliveryIntegrationResponse>
    {
        new DeliveryIntegrationResponse { Name = "FastShip", IsActive = true },
        new DeliveryIntegrationResponse { Name = "QuickDrop", IsActive = false }
    };

       
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
        _mockMapper.Setup(m => m.ToResponse(entities[0])).Returns(responses[0]);
        _mockMapper.Setup(m => m.ToResponse(entities[1])).Returns(responses[1]);

        // Act
        var result = await _queries.GetAllAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count());
        Assert.Contains(result.Value, r => r.Name == "FastShip");
        Assert.Contains(result.Value, r => r.Name == "QuickDrop");
    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnFailure_WhenNoEntitiesExist()
    {
        // Arrange
       
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<DeliveryIntegration>());

        // Act
        var result = await _queries.GetAllAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Dilveries integration Found", result.Error);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<DeliveryIntegration>()), Times.Never);
    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnFailure_WhenRepositoryThrowsException()
    {
        // Arrange
      
        _mockRepository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("DB error"));

        // Act
        var result = await _queries.GetAllAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch Dilveries integration", result.Error);
        Assert.Contains("DB error", result.Error);
    }



}
