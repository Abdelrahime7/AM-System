using Application.ProductImages.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.ProductImages.Queries;

public partial class ProductImageQueriesTests
{
    [Fact]
    public async Task GetByCustomizedOrderIdAsync_ShouldReturnSuccess_WhenProductImagesAreFound()
    {
        //Arrange
        const int orderId = 1;
        var productImages = new List<ProductImage>
        {
            new() { Id = 1, ImageUrl = "image1.jpg", AltText = "Order Image 1", IsPrimary = true, CustomizedOrderId = 1 },
            new() { Id = 2, ImageUrl = "image2.jpg", AltText = "Order Image 2", IsPrimary = false, CustomizedOrderId = 1 }
        };

        _mockRepository.Setup(r => r.GetByCustomizedOrderIdAsync(orderId))
            .ReturnsAsync(productImages);

        _mockMapper.Setup(m => m.ToResponse(It.IsAny<ProductImage>()))
            .Returns<ProductImage>(pi => new ProductImageResponse
            {
                Id = pi.Id,
                ImageUrl = pi.ImageUrl,
                AltText = pi.AltText,
                IsPrimary = pi.IsPrimary,
                CustomizedOrderId = pi.CustomizedOrderId
            });

        //Act
        var result = await _queries.GetByCustomizedOrderIdAsync(orderId);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value!.Count());
        _mockRepository.Verify(r => r.GetByCustomizedOrderIdAsync(orderId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<ProductImage>()), Times.Exactly(2));
    }
    
    [Fact]
    public async Task GetByCustomizedOrderIdAsync_ShouldReturnFailure_WhenNoProductImagesFound()
    {
        //Arrange
        const int orderId = 999;
        
        _mockRepository.Setup(r => r.GetByCustomizedOrderIdAsync(orderId))
            .ReturnsAsync(new List<ProductImage>()); 

        //Act
        var result = await _queries.GetByCustomizedOrderIdAsync(orderId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No product images found for this customized order", result.Error);
        _mockRepository.Verify(r => r.GetByCustomizedOrderIdAsync(orderId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<ProductImage>()), Times.Never);
    }
}