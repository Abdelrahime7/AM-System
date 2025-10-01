using Domain.Entities;
using Moq;

namespace UnitTests.Application.Products.Commands;

public partial class ProductCommandsTests
{
    [Fact]
    public async Task DeleteProductAsync_ShouldReturnSuccess_WhenProductIsDeleted()
    {
        //Arrange
        const int productId = 1;
        var product = new Product
        {
            Id = productId,
            Name = null!
        };

        _mockRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
        
        // Act
        var result = await _commands.DeleteProductAsync(productId);

        // Assert
        Assert.True(result.IsSuccess);
        _mockRepository.Verify(r => r.Delete(product), Times.Once);
    }

    [Fact]
    public async Task DeleteProductAsync_ShouldReturnFailure_WhenProductNotFound()
    {
        //Arrange
        const int productId = 99; // Non-existent ID

        _mockRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync((Product)null!);

        //Act
        var result = await _commands.DeleteProductAsync(productId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Product Not Found", result.Error);
    }
    
    [Fact]
    public async Task DeleteProductAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int productId = 1;
        var product = new Product
        {
            Id = productId,
            Name = null!
        };

        _mockRepository.Setup(m => m.GetByIdAsync(productId)).ReturnsAsync(product);
        _mockRepository.Setup(r => r.Delete(It.IsAny<Product>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.DeleteProductAsync(productId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error updating product: DB Error", result.Error);
    }
}
