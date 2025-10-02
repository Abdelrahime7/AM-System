using Application.Products.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Products.Queries;

public partial class ProductQueriesTests
{
    [Fact]
    public async Task GetByIdAsync_ShouldReturnSuccess_WhenProductIsFound()
    {
        //Arrange
        const int productId = 1;
        var product = new Product { Id = productId, Name = "Test Product" };
        var productResponse = new ProductResponse { Id = productId, Name = "Test Product" };

        _mockRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
        _mockMapper.Setup(m => m.ToResponse(product)).Returns(productResponse);

        // Act
        var result = await _queries.GetByIdAsync(productId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(productId, result.Value.Id);
        _mockRepository.Verify(r => r.GetByIdAsync(productId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(product), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnFailure_WhenProductNotFound()
    {
        //Arrange
        const int productId = 99; // Non-existent ID
        _mockRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync((Product)null!);

        //Act
        var result = await _queries.GetByIdAsync(productId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Product Found", result.Error);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int productId = 1;
        _mockRepository.Setup(r => r.GetByIdAsync(productId)).ThrowsAsync(new Exception("DB Error"));

        //Act
        var result = await _queries.GetByIdAsync(productId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch product: DB Error", result.Error);
    }
}
