using Application.Customers.DTOs;
using Application.Customers.Mapper;
using Application.Delivery.DTOs;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Delivery.Commands;

public partial class DeliveryCommandsTests
{
    [Fact]
    public async Task UpdateDeliveryIntegrationAsync_ShouldReturnSuccess_WhenEntityExists()
    {
        // Arrange
        var request = new UpdateDeliveryIntegrationRequest
        {
            Id = 1,
            Name = "UpdatedName",
            ApiEndpoint = "https://new.api.com",
            ApiKey = "newKey",
            ApiSecret = "newSecret",
            IsActive = true
        };

        var existingEntity = new DeliveryIntegration
        {
            Id = 1,
            Name = "OldName",
            ApiEndpoint = "https://old.api.com",
            ApiKey = "oldKey",
            ApiSecret = "oldSecret",
            IsActive = false
        };


        _mockRepository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(existingEntity);
        _mockMapper.Setup(m => m.ToUpdateEntity(existingEntity, request));
        _mockRepository.Setup(r => r.Update(existingEntity));

        // Act
        var result = await _commands.UpdateDeliveryIntegrationAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockMapper.Verify(m => m.ToUpdateEntity(existingEntity, request), Times.Once);
        _mockRepository.Verify(r => r.Update(existingEntity), Times.Once);
    }

    [Fact]
    public async Task UpdateDeliveryIntegrationAsync_ShouldReturnFailure_WhenEntityNotFound()
    {
        // Arrange
        var request = new UpdateDeliveryIntegrationRequest { Id = 99 };

      

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((DeliveryIntegration)null);

        // Act
        var result = await _commands.UpdateDeliveryIntegrationAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Customer Not Found", result.Error);
        _mockMapper.Verify(m => m.ToUpdateEntity(It.IsAny<DeliveryIntegration>(), It.IsAny<UpdateDeliveryIntegrationRequest>()), Times.Never);
       _mockRepository.Verify(r => r.Update(It.IsAny<DeliveryIntegration>()), Times.Never);
    }



}