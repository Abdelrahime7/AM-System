using Application.ProductImages.DTOs;
using Application.ProductImages.Features.Queries;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.ProductImages.Queries;

public partial class ProductImageQueriesTests
{
    private readonly Mock<IProductImageRepository> _mockRepository;
    private readonly Mock<IEntityMapper<ProductImage, CreateProductImageRequest, UpdateProductImageRequest, ProductImageResponse>> _mockMapper;
    private readonly ProductImageQueries _queries;
    
    public ProductImageQueriesTests()
    {
        _mockRepository = new Mock<IProductImageRepository>();
        _mockMapper = new Mock<IEntityMapper<ProductImage, CreateProductImageRequest, UpdateProductImageRequest, ProductImageResponse>>();
        _queries = new ProductImageQueries(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnSuccess_WhenProductImagesAreFound()
    {
        // Arrange
        var productImages = new List<ProductImage>
        {
            new() { Id = 1, ImageUrl = "image1.jpg", AltText = "Image 1", IsPrimary = true, ProductId = 1 },
            new() { Id = 2, ImageUrl = "image2.jpg", AltText = "Image 2", IsPrimary = false, ProductId = 1 },
            new() { Id = 3, ImageUrl = "image3.jpg", AltText = "Image 3", IsPrimary = true, CustomizedOrderId = 1 }
        };

        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(productImages);

        _mockMapper.Setup(m => m.ToResponse(It.IsAny<ProductImage>()))
                   .Returns<ProductImage>(pi => new ProductImageResponse
                   {
                       Id = pi.Id,
                       ImageUrl = pi.ImageUrl,
                       AltText = pi.AltText,
                       IsPrimary = pi.IsPrimary,
                       ProductId = pi.ProductId,
                       CustomizedOrderId = pi.CustomizedOrderId
                   });

        // Act
        var result = await _queries.GetAllAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(productImages.Count, result.Value!.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<ProductImage>()), Times.Exactly(productImages.Count));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnFailure_WhenProductImagesNotFound()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(new List<ProductImage>());

        // Act
        var result = await _queries.GetAllAsync();
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No product images found", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<ProductImage>()), Times.Never);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ThrowsAsync(new Exception("DB Error"));

        // Act
        var result = await _queries.GetAllAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to fetch product images: DB Error", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}




