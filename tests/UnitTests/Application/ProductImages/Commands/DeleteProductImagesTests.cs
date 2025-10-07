using Domain.Entities;
using Moq;

namespace UnitTests.Application.ProductImages.Commands;

public partial class ProductImageCommandsTests
{
    [Fact]
    public async Task DeleteProductImageAsync_ShouldReturnSuccess_WhenProductImageIsDeleted()
    {
        //Arrange
        const int requestId = 1;
        var productImage = new ProductImage
        {
            Id = 1,
            ImageUrl = "https://storage.com/images/test.jpg",
            AltText = "Test image",
            IsPrimary = false,
            ProductId = 1
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync(productImage);
        _mockFileStorageService.Setup(f => f.DeleteFileAsync(productImage.ImageUrl, CancellationToken.None))
            .Returns(() => Task.FromResult(true));
        
        // Act
        var result = await _commands.DeleteProductImageAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockFileStorageService.Verify(f => f.DeleteFileAsync(productImage.ImageUrl, It.IsAny<CancellationToken>()), Times.Once);
        _mockRepository.Verify(r => r.Delete(productImage), Times.Once);
    }

    [Fact]
    public async Task DeleteProductImageAsync_ShouldReturnFailure_WhenProductImageNotFound()
    {
        //Arrange
        const int requestId = -1;

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync((ProductImage)null!);

        //Act
        var result = await _commands.DeleteProductImageAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Product image not found", result.Error);
    }
    
    [Fact]
    public async Task DeleteProductImageAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = 1;
        var productImage = new ProductImage
        {
            Id = 1,
            ImageUrl = "https://storage.com/images/test.jpg",
            AltText = "Test image",
            IsPrimary = false,
            ProductId = 1
        };

        _mockRepository.Setup(m => m.GetByIdAsync(requestId)).ReturnsAsync(productImage);
        _mockFileStorageService.Setup(f => f.DeleteFileAsync(productImage.ImageUrl, CancellationToken.None))
            .ThrowsAsync(new Exception("Storage Error"));

        //Act
        var result = await _commands.DeleteProductImageAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error deleting product image: Storage Error", result.Error);
    }
}
