using Application.ProductImages.DTOs;
using Application.ProductImages.Features.Commands;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.Application.ProductImages.Commands;

public partial class ProductImageCommandsTests
{
    private readonly Mock<IProductImageRepository> _mockRepository;
    private readonly Mock<IFileStorageService> _mockFileStorageService;
    private readonly Mock<IEntityMapper<ProductImage, CreateProductImageRequest, UpdateProductImageRequest, ProductImageResponse>> _mockMapper;
    private readonly ProductImageCommands _commands;
    
    public ProductImageCommandsTests()
    {
        _mockRepository = new Mock<IProductImageRepository>();
        _mockFileStorageService = new Mock<IFileStorageService>();
        _mockMapper = new Mock<IEntityMapper<ProductImage, CreateProductImageRequest, UpdateProductImageRequest, ProductImageResponse>>();
        _commands = new ProductImageCommands(_mockRepository.Object, _mockFileStorageService.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task CreateProductImageAsync_ShouldReturnSuccess_WhenProductImageIsAdded()
    {
        //Arrange
        var mockFile = new Mock<IFormFile>();
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });
        mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
        mockFile.Setup(f => f.FileName).Returns("test.jpg");
        mockFile.Setup(f => f.ContentType).Returns("image/jpeg");

        var request = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            AltText = "Test product image",
            IsPrimary = true,
            ProductId = 1
        };
        
        var newProductImage = new ProductImage
        {
            Id = 1,
            AltText = "Test product image",
            IsPrimary = true,
            ProductId = 1,
            ImageUrl = string.Empty // Initialize with empty string instead of null
        };

        _mockFileStorageService.Setup(f => f.UploadFileAsync(stream, It.IsAny<string>(), "image/jpeg", CancellationToken.None))
            .ReturnsAsync("https://storage.com/images/test-guid.jpg");
        
        _mockRepository.Setup(r => r.GetByProductIdAsync(1))
            .ReturnsAsync(new List<ProductImage>());
        
        _mockMapper.Setup(m => m.ToEntity(request)).Returns(newProductImage);
        _mockRepository.Setup(r => r.AddAsync(newProductImage));

        //Act
        var result = await _commands.CreateProductImageAsync(request);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
        Assert.Equal("https://storage.com/images/test-guid.jpg", newProductImage.ImageUrl);
        _mockFileStorageService.Verify(f => f.UploadFileAsync(stream, It.IsAny<string>(), "image/jpeg", CancellationToken.None), Times.Once);
        _mockRepository.Verify(r => r.GetByProductIdAsync(1), Times.Once);
        _mockMapper.Verify(m => m.ToEntity(request), Times.Once);
        _mockRepository.Verify(r => r.AddAsync(newProductImage), Times.Once);
    }

    [Fact]
    public async Task CreateProductImageAsync_ShouldResetPrimaryImages_WhenSettingAsPrimaryForProduct()
    {
        //Arrange
        var mockFile = new Mock<IFormFile>();
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });
        mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
        mockFile.Setup(f => f.FileName).Returns("test.jpg");
        mockFile.Setup(f => f.ContentType).Returns("image/jpeg");

        var request = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            AltText = "Primary product image",
            IsPrimary = true,
            ProductId = 1
        };
        
        var existingPrimaryImage = new ProductImage
        {
            Id = 2,
            ImageUrl = "old-url.jpg",
            IsPrimary = true,
            ProductId = 1
        };

        var newProductImage = new ProductImage
        {
            Id = 1,
            AltText = "Primary product image",
            IsPrimary = true,
            ProductId = 1,
            ImageUrl = string.Empty // Initialize with empty string instead of null
        };

        _mockFileStorageService.Setup(f => f.UploadFileAsync(stream, It.IsAny<string>(), "image/jpeg", CancellationToken.None))
            .ReturnsAsync("https://storage.com/images/new-primary.jpg");
        
        _mockRepository.Setup(r => r.GetByProductIdAsync(1))
            .ReturnsAsync(new List<ProductImage> { existingPrimaryImage });
        
        _mockMapper.Setup(m => m.ToEntity(request)).Returns(newProductImage);
        _mockRepository.Setup(r => r.AddAsync(newProductImage));

        //Act
        var result = await _commands.CreateProductImageAsync(request);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.False(existingPrimaryImage.IsPrimary); // Should be reset to false
        _mockRepository.Verify(r => r.Update(existingPrimaryImage), Times.Once);
    }

    [Fact]
    public async Task CreateProductImageAsync_ShouldResetPrimaryImages_WhenSettingAsPrimaryForCustomizedOrder()
    {
        //Arrange
        var mockFile = new Mock<IFormFile>();
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });
        mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
        mockFile.Setup(f => f.FileName).Returns("test.jpg");
        mockFile.Setup(f => f.ContentType).Returns("image/jpeg");

        var request = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            AltText = "Primary order image",
            IsPrimary = true,
            CustomizedOrderId = 1
        };
        
        var existingPrimaryImage = new ProductImage
        {
            Id = 2,
            ImageUrl = "old-url.jpg",
            IsPrimary = true,
            CustomizedOrderId = 1
        };

        var newProductImage = new ProductImage
        {
            Id = 1,
            AltText = "Primary order image",
            IsPrimary = true,
            CustomizedOrderId = 1,
            ImageUrl = string.Empty // Initialize with empty string instead of null
        };

        _mockFileStorageService.Setup(f => f.UploadFileAsync(stream, It.IsAny<string>(), "image/jpeg", CancellationToken.None))
            .ReturnsAsync("https://storage.com/images/new-primary.jpg");
        
        _mockRepository.Setup(r => r.GetByCustomizedOrderIdAsync(1))
            .ReturnsAsync(new List<ProductImage> { existingPrimaryImage });
        
        _mockMapper.Setup(m => m.ToEntity(request)).Returns(newProductImage);
        _mockRepository.Setup(r => r.AddAsync(newProductImage));

        //Act
        var result = await _commands.CreateProductImageAsync(request);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.False(existingPrimaryImage.IsPrimary); // Should be reset to false
        _mockRepository.Verify(r => r.Update(existingPrimaryImage), Times.Once);
    }

    [Fact]
    public async Task CreateProductImageAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var mockFile = new Mock<IFormFile>();
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });
        mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
        mockFile.Setup(f => f.FileName).Returns("test.jpg");
        mockFile.Setup(f => f.ContentType).Returns("image/jpeg");

        var request = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            AltText = "Test image",
            IsPrimary = false,
            ProductId = 1
        };

        var productImage = new ProductImage
        {
            Id = -1,
            AltText = "Test image",
            IsPrimary = false,
            ProductId = 1,
            ImageUrl = string.Empty // Initialize with empty string instead of null
        };

        _mockFileStorageService.Setup(f => f.UploadFileAsync(stream, It.IsAny<string>(), "image/jpeg", CancellationToken.None))
            .ReturnsAsync("https://storage.com/images/test.jpg");
        
        _mockRepository.Setup(r => r.GetByProductIdAsync(1))
            .ReturnsAsync(new List<ProductImage>());
        
        _mockMapper.Setup(m => m.ToEntity(It.IsAny<CreateProductImageRequest>()))
            .Returns(productImage);
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<ProductImage>()))
            .ThrowsAsync(new Exception("DB Error"));
        
        // Act
        var result = await _commands.CreateProductImageAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error creating product image: DB Error", result.Error);
    }
}