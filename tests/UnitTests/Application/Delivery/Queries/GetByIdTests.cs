using Application.Customers.DTOs;
using Application.Customers.Mapper;
using Application.Delivery.DTOs;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Delivery.Queries;

public partial class DeliveryQueriesTests
{
    [Fact]
    public async Task GetByIdAsync_ShouldReturnSuccess_WhenEntityExists()
    {
        // Arrange
        var id = 1;
        var entity = new DeliveryIntegration
        {
            Id = id,
            Name = "FastShip",
            ApiEndpoint = "https://api.fastship.com",
            ApiKey = "abc123",
            ApiSecret = "secret!",
            IsActive = true
        };

        var responseDto = new DeliveryIntegrationResponse
        {
            Name = "FastShip",
            ApiEndpoint = "https://api.fastship.com",
            IsActive = true
            // other fields as needed
        };

      

        _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);
        _mockMapper.Setup(m => m.ToResponse(entity)).Returns(responseDto);

        // Act
        var result = await _queries.GetByIdAsync(id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(responseDto.Name, result.Value.Name);
        Assert.Equal(responseDto.ApiEndpoint, result.Value.ApiEndpoint);
        Assert.Equal(responseDto.IsActive, result.Value.IsActive);
    }
    [Fact]
    public async Task GetByIdAsync_ShouldReturnFailure_WhenEntityNotFound()
    {
        // Arrange
        var id = 99;

       

        _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((DeliveryIntegration)null);

        // Act
        var result = await _queries.GetByIdAsync(id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Delivery Integration Found", result.Error);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<DeliveryIntegration>()), Times.Never);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnFailure_WhenRepositoryThrowsException()
    {
        // Arrange
        var id = 1;

        
        _mockRepository.Setup(r => r.GetByIdAsync(id)).ThrowsAsync(new Exception("DB error"));

        // Act
        var result = await _queries.GetByIdAsync(id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch  Delivery Integration", result.Error);
        Assert.Contains("DB error", result.Error);
    }


}
