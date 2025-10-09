using Application.ProductImages.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.Application.ProductImages.Commands;

public partial class ProductImageCommandsTests
{
    [Fact]
    public async Task UpdateProductImageAsync_ShouldReturnSuccess_WhenProductImageIsUpdated()
    {
        //Arrange
        var request = new UpdateProductImageRequest
        {
            Id = 1,
            AltText = "Updated alt text",
            IsPrimary = true
        };
        
        var productImage = new ProductImage
        {
            Id = 1,
            ImageUrl = "https://storage.com/images/old.jpg",
            AltText = "Old alt text",
            IsPrimary = false,
            ProductId = 1
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(productImage);
        _mockRepository.Setup(r => r.GetByProductIdAsync(1))
            .ReturnsAsync(new List<ProductImage>());
        
        // Act
        var result = await _commands.UpdateProductImageAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockMapper.Verify(m => m.ToUpdateEntity(productImage, request), Times.Once);
        _mockRepository.Verify(r => r.Update(productImage), Times.Once);
    }

    [Fact]
    public async Task UpdateProductImageAsync_ShouldUploadNewFile_WhenImageFileProvided()
    {
        //Arrange
        var mockFile = new Mock<IFormFile>();
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });
        mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
        mockFile.Setup(f => f.FileName).Returns("new-image.jpg");
        mockFile.Setup(f => f.ContentType).Returns("image/jpeg");
        mockFile.Setup(f => f.Length).Returns(1024);

        var request = new UpdateProductImageRequest
        {
            Id = 1,
            ImageFile = mockFile.Object,
            AltText = "Updated with new image"
        };
        
        var productImage = new ProductImage
        {
            Id = 1,
            ImageUrl = "https://storage.com/images/old.jpg",
            AltText = "Old alt text",
            IsPrimary = false,
            ProductId = 1
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(productImage);
        _mockFileStorageService.Setup(f => f.UploadFileAsync(stream, It.IsAny<string>(), "image/jpeg", CancellationToken.None))
            .ReturnsAsync("https://storage.com/images/new-guid.jpg");
        _mockFileStorageService.Setup(f => f.DeleteFileAsync("https://storage.com/images/old.jpg", CancellationToken.None))
            .Returns(() => Task.FromResult(true));
        
        // Act
        var result = await _commands.UpdateProductImageAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("https://storage.com/images/new-guid.jpg", productImage.ImageUrl);
        _mockFileStorageService.Verify(f => f.UploadFileAsync(stream, It.IsAny<string>(), "image/jpeg", CancellationToken.None), Times.Once);
        _mockFileStorageService.Verify(f => f.DeleteFileAsync("https://storage.com/images/old.jpg", CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task UpdateProductImageAsync_ShouldResetPrimaryImages_WhenSettingAsPrimary()
    {
        //Arrange
        var request = new UpdateProductImageRequest
        {
            Id = 1,
            IsPrimary = true
        };
        
        var productImage = new ProductImage
        {
            Id = 1,
            ImageUrl = "https://storage.com/images/test.jpg",
            AltText = "Test image",
            IsPrimary = false,
            ProductId = 1
        };

        var existingPrimaryImage = new ProductImage
        {
            Id = 2,
            ImageUrl = "https://storage.com/images/primary.jpg",
            IsPrimary = true,
            ProductId = 1
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(productImage);
        _mockRepository.Setup(r => r.GetByProductIdAsync(1))
            .ReturnsAsync(new List<ProductImage> { existingPrimaryImage });
        
        // Act
        var result = await _commands.UpdateProductImageAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(existingPrimaryImage.IsPrimary); // Should be reset to false
        _mockRepository.Verify(r => r.Update(existingPrimaryImage), Times.Once);
    }

    [Fact]
    public async Task UpdateProductImageAsync_ShouldReturnFailure_WhenProductImageNotFound()
    {
        //Arrange
        var request = new UpdateProductImageRequest
        {
            Id = 1,
            AltText = "Updated text"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((ProductImage)null!);

        //Act
        var result = await _commands.UpdateProductImageAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Product image not found", result.Error);
    }

    [Fact]
    public async Task UpdateProductImageAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        var request = new UpdateProductImageRequest
        {
            Id = 1,
            AltText = "Updated text"
        };
        
        var productImage = new ProductImage
        {
            Id = 1,
            ImageUrl = "https://storage.com/images/test.jpg",
            AltText = "Test image",
            IsPrimary = false,
            ProductId = 1
        };

        _mockRepository.Setup(m => m.GetByIdAsync(request.Id)).ReturnsAsync(productImage);
        _mockRepository.Setup(r => r.GetByProductIdAsync(1))
            .ReturnsAsync(new List<ProductImage>());
        _mockRepository.Setup(r => r.Update(It.IsAny<ProductImage>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.UpdateProductImageAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to update product image: DB Error", result.Error);
    }
}