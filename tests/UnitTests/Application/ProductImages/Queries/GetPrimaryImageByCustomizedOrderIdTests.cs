using Application.ProductImages.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.ProductImages.Queries;

public partial class ProductImageQueriesTests
{
    [Fact]
    public async Task GetPrimaryImageByCustomizedOrderIdAsync_ShouldReturnSuccess_WhenPrimaryImageFound()
    {
        //Arrange
        const int orderId = 1;
        var primaryImage = new ProductImage
        {
            Id = 1,
            ImageUrl = "primary-order.jpg",
            AltText = "Primary Order Image",
            IsPrimary = true,
            CustomizedOrderId = 1,
            CustomizedOrder = new CustomizedOrder
            {
                Name = "Custom Order",
                Dimensions = null!
            }
        };

        _mockRepository.Setup(r => r.GetPrimaryImageByCustomizedOrderIdAsync(orderId))
            .ReturnsAsync(primaryImage);

        _mockMapper.Setup(m => m.ToResponse(primaryImage))
            .Returns(new ProductImageResponse
            {
                Id = primaryImage.Id,
                ImageUrl = primaryImage.ImageUrl,
                AltText = primaryImage.AltText,
                IsPrimary = primaryImage.IsPrimary,
                CustomizedOrderId = primaryImage.CustomizedOrderId,
                CustomizedOrderName = primaryImage.CustomizedOrder.Name
            });

        //Act
        var result = await _queries.GetPrimaryImageByCustomizedOrderIdAsync(orderId);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(primaryImage.Id, result.Value.Id);
        Assert.True(result.Value.IsPrimary);
        _mockRepository.Verify(r => r.GetPrimaryImageByCustomizedOrderIdAsync(orderId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(primaryImage), Times.Once);
    }
    
    [Fact]
    public async Task GetPrimaryImageByCustomizedOrderIdAsync_ShouldReturnFailure_WhenNoPrimaryImageFound()
    {
        //Arrange
        const int orderId = 999; // Non-existent order ID
    
        _mockRepository.Setup(r => r.GetPrimaryImageByCustomizedOrderIdAsync(orderId))
            .ReturnsAsync((ProductImage)null!); // Return null when no primary image found

        //Act
        var result = await _queries.GetPrimaryImageByCustomizedOrderIdAsync(orderId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No primary image found for this customized order", result.Error);
        _mockRepository.Verify(r => r.GetPrimaryImageByCustomizedOrderIdAsync(orderId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<ProductImage>()), Times.Never); // Should not call mapper
    }
}