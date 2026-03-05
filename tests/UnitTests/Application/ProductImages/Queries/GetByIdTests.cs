using Application.ProductImages.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.ProductImages.Queries;

public partial class ProductImageQueriesTests
{
    [Fact]
    public async Task GetByIdAsync_ShouldReturnSuccess_WhenProductImageIsFound()
    {
        //Arrange
        const int requestId = 1;
        var productImage = new ProductImage
        {
            Id = 1,
            ImageUrl = "https://storage.com/images/test.jpg",
            AltText = "Test product image",
            IsPrimary = true,
            ProductId = 1,
            Product = new Product { Name = "Test Product" }
        };
        
        var response = new ProductImageResponse
        {
            Id = productImage.Id,
            ImageUrl = productImage.ImageUrl,
            AltText = productImage.AltText,
            IsPrimary = productImage.IsPrimary,
            ProductId = productImage.ProductId,
            ProductName = productImage.Product.Name
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync(productImage);

        _mockMapper.Setup(m => m.ToResponse(productImage))
            .Returns(response);

        // Act
        var result = await _queries.GetByIdAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(productImage.Id, result.Value.Id);
        Assert.Equal(productImage.ImageUrl, result.Value.ImageUrl);
        Assert.Equal(productImage.AltText, result.Value.AltText);

        _mockRepository.Verify(r => r.GetByIdAsync(requestId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(productImage), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnFailure_WhenProductImageNotFound()
    {
        //Arrange
        const int requestId = 1;
        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync((ProductImage)null!);

        //Act
        var result = await _queries.GetByIdAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Product image not found", result.Error);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = 1;

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ThrowsAsync(new Exception("DB Error"));

        //Act
        var result = await _queries.GetByIdAsync(requestId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to fetch product image: DB Error", result.Error);
    }
}