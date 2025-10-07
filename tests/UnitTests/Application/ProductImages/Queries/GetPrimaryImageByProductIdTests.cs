
using Application.ProductImages.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.ProductImages.Queries;

public partial class ProductImageQueriesTests
{
    [Fact]
    public async Task GetPrimaryImageByProductIdAsync_ShouldReturnSuccess_WhenPrimaryImageFound()
    {
        //Arrange
        const int productId = 1;
        var primaryImage = new ProductImage
        {
            Id = 1,
            ImageUrl = "primary.jpg",
            AltText = "Primary Image",
            IsPrimary = true,
            ProductId = 1,
            Product = new Product { Name = "Test Product" }
        };

        _mockRepository.Setup(r => r.GetPrimaryImageByProductIdAsync(productId))
            .ReturnsAsync(primaryImage);

        _mockMapper.Setup(m => m.ToResponse(primaryImage))
            .Returns(new ProductImageResponse
            {
                Id = primaryImage.Id,
                ImageUrl = primaryImage.ImageUrl,
                AltText = primaryImage.AltText,
                IsPrimary = primaryImage.IsPrimary,
                ProductId = primaryImage.ProductId,
                ProductName = primaryImage.Product.Name
            });

        //Act
        var result = await _queries.GetPrimaryImageByProductIdAsync(productId);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(primaryImage.Id, result.Value.Id);
        Assert.True(result.Value.IsPrimary);
        _mockRepository.Verify(r => r.GetPrimaryImageByProductIdAsync(productId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(primaryImage), Times.Once);
    }

    [Fact]
    public async Task GetPrimaryImageByProductIdAsync_ShouldReturnFailure_WhenNoPrimaryImageFound()
    {
        //Arrange
        const int productId = 999;
        _mockRepository.Setup(r => r.GetPrimaryImageByProductIdAsync(productId))
            .ReturnsAsync((ProductImage)null!);

        //Act
        var result = await _queries.GetPrimaryImageByProductIdAsync(productId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No primary image found for this product", result.Error);

        _mockRepository.Verify(r => r.GetPrimaryImageByProductIdAsync(productId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<ProductImage>()), Times.Never);
    }
}