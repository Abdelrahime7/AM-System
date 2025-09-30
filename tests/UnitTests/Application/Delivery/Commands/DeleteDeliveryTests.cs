using Application.Customers.DTOs;
using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.DataProtection;
using Moq;
using System.Xml.Linq;

namespace UnitTests.Application.Delivery.Commands;

public partial class DeliveryCommandsTests
{
    [Fact]
    public async Task DeleteDeliveryIntegrationAsync_ShouldReturnSuccess_WhenEntityExists()
    {
        // Arrange
        var id = 1;
        var existingEntity = new DeliveryIntegration {Id=1,
            
                Name = "FastShip",
                ApiEndpoint = "https://api.fastship.com/v1",
                ApiKey = "abc123xyz",
                ApiSecret = "superSecretKey!",
                IsActive = true
            };

       
        _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existingEntity);
        _mockRepository.Setup(r => r.Delete(existingEntity));

        // Act
        var result = await _commands.DeleteDeliveryIntegrationAsync(id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockRepository.Verify(r => r.Delete(existingEntity), Times.Once);
    }

    [Fact]
    public async Task DeleteDeliveryIntegrationAsync_ShouldReturnFailure_WhenExceptionThrown()
    {
        // Arrange
        var id = 1;

      
        _mockRepository.Setup(r => r.GetByIdAsync(id)).ThrowsAsync(new Exception("DB error"));

        // Act
        var result = await _commands.DeleteDeliveryIntegrationAsync(id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to delete Delivery Integration", result.Error);
        Assert.Contains("DB error", result.Error);
    }


}