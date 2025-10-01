using Application.Products.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Products.Commands;

public partial class ProductCommandsTests
{
    [Fact]
    public async Task UpdateProductAsync_ShouldReturnSuccess_WhenProductIsUpdated()
    {
        //Arrange
        var request = new UpdateAffiliateBalanceRequest
        {
            Id = 1,
            Name = "Updated Product Name"
        };
        
        var product = new Product
        {
            Id = 1,
            Name = "Original Product Name"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(product);
        
        // Act
        var result = await _commands.UpdateProductAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        _mockMapper.Verify(m => m.ToUpdateEntity(product, request), Times.Once);
        _mockRepository.Verify(r => r.Update(product), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_ShouldReturnFailure_WhenProductNotFound()
    {
        //Arrange
        var request = new UpdateAffiliateBalanceRequest
        {
            Id = 99 // Non-existent ID
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((Product)null!);

        //Act
        var result = await _commands.UpdateProductAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Product not found", result.Error);
    }
    
    [Fact]
    public async Task UpdateProductAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        var request = new UpdateAffiliateBalanceRequest
        {
            Id = 1
        };
        
        var product = new Product
        {
            Id = 1,
            Name = null!
        };

        _mockRepository.Setup(m => m.GetByIdAsync(request.Id)).ReturnsAsync(product);
        _mockRepository.Setup(r => r.Update(It.IsAny<Product>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.UpdateProductAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to update product: DB Error", result.Error);
    }
}
