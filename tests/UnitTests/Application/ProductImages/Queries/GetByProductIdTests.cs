using Application.ProductImages.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.ProductImages.Queries;

public partial class ProductImageQueriesTests
{
    [Fact]
    public async Task GetByProductIdAsync_ShouldReturnSuccess_WhenProductImagesAreFound()
    {
        //Arrange
        const int productId = 1;
        var productImages = new List<ProductImage>
        {
            new() { Id = 1, ImageUrl = "image1.jpg", AltText = "Image 1", IsPrimary = true, ProductId = 1 },
            new() { Id = 2, ImageUrl = "image2.jpg", AltText = "Image 2", IsPrimary = false, ProductId = 1 }
        };

        _mockRepository.Setup(r => r.GetByProductIdAsync(productId))
            .ReturnsAsync(productImages);

        _mockMapper.Setup(m => m.ToResponse(It.IsAny<ProductImage>()))
            .Returns<ProductImage>(pi => new ProductImageResponse
            {
                Id = pi.Id,
                ImageUrl = pi.ImageUrl,
                AltText = pi.AltText,
                IsPrimary = pi.IsPrimary,
                ProductId = pi.ProductId
            });

        //Act
        var result = await _queries.GetByProductIdAsync(productId);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value!.Count());
        _mockRepository.Verify(r => r.GetByProductIdAsync(productId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<ProductImage>()), Times.Exactly(2));
    }

    [Fact]
    public async Task GetByProductIdAsync_ShouldReturnFailure_WhenNoProductImagesFound()
    {
        //Arrange
        const int productId = 999;
        _mockRepository.Setup(r => r.GetByProductIdAsync(productId))
            .ReturnsAsync(new List<ProductImage>());

        //Act
        var result = await _queries.GetByProductIdAsync(productId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No product images found for this product", result.Error);

        _mockRepository.Verify(r => r.GetByProductIdAsync(productId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<ProductImage>()), Times.Never);
    }
}

