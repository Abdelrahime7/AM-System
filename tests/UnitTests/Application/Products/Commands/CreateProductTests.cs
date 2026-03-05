using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using Application.Products.Features.Commands;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Products.Commands;

public partial class ProductCommandsTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Product, CreateProductRequest, UpdateProductRequest, ProductResponse>> _mockMapper;
    private readonly ProductCommands _commands;
    
    public ProductCommandsTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockMapper = new Mock<IEntityMapper<Product, CreateProductRequest, UpdateProductRequest, ProductResponse>>();
        _commands = new ProductCommands(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task CreateProductAsync_ShouldReturnSuccess_WhenProductIsAdded()
    {
        //Arrange
        var request = new CreateProductRequest
        {
            Name = "Test Product",
            Price = 100,
            CommissionAmount = 10,
            Status = ProductStatus.Active,
            CreatedByUserId = 1
        };
        
        var product = new Product
        {
            Id = 1,
            Name = "Test Product",
            Price = 100,
            CommissionAmount = 10,
            Status = ProductStatus.Active,
            CreatedByUserId = 1
        };

        _mockMapper.Setup(m => m.ToEntity(request)).Returns(product);
        _mockRepository.Setup(r => r.AddAsync(product));

        //Act
        var result = await _commands.CreateProductAsync(request);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
        _mockMapper.Verify(m => m.ToEntity(request), Times.Once);
        _mockRepository.Verify(r => r.AddAsync(product), Times.Once);
    }

    [Fact]
    public async Task CreateProductAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Error Product",
            Price = 50,
            CommissionAmount = 5,
            Status = ProductStatus.Inactive,
            CreatedByUserId = 2
        };

        _mockMapper.Setup(m => m.ToEntity(It.IsAny<CreateProductRequest>()))
            .Returns((Product)null!);

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ThrowsAsync(new Exception("DB Error"));
        
        // Act
        var result = await _commands.CreateProductAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error creating product: DB Error", result.Error);
    }
}
